using Xunit;

namespace PokemonShakespeare.Api.UnitTests;

using System;
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using NSubstitute;
using Services;

//Testing as outlined by the microsoft docs: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
public class TranslationEndpointTests
{
    private readonly ITranslator _translator = Substitute.For<ITranslator>();
        
    
    [Fact]
    public async Task GET_Pokemon_returns_OK_response()
    {
        //Arrange
        var pokemonName = "charmander";
        var description = new TranslationModel()
        {
            Description = "fallback description",
            Name = "charmander",
            ShakespeareDescription = "Shall I compare thee to a summers day",
            Sprite = "www.spriteurl.test"
        };

        _translator.GetTranslation(Arg.Is(pokemonName)).Returns(description);
        
        using var app = new TranslationEndpointsTestsApp(x =>
        {
            x.AddSingleton(_translator);
        });

        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"pokemon/{pokemonName}");
        var responseText = await response.Content.ReadAsStringAsync();
        var translationResult = JsonSerializer.Deserialize<TranslationModel>(responseText);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        //translationResult.Should().BeEquivalentTo(description);

    }
    
    [Fact]
    public async Task GET_Pokemon_returns_NotFound_response_if_not_exists()
    {
        //Arrange
        var pokemonName = "hello";

        using var app = new TranslationEndpointsTestsApp(x =>
        {
            x.AddSingleton(_translator);
        });

        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"pokemon/{pokemonName}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    internal class TranslationEndpointsTestsApp : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _serviceOverride;

        public TranslationEndpointsTestsApp(Action<IServiceCollection> serviceOverride)
        {
            _serviceOverride = serviceOverride;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(_serviceOverride);

            return base.CreateHost(builder);
        }
    }
}