using Xunit;

namespace PokemonShakespeare.Api.UnitTests;

using System;
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
        
    [Theory]
    [InlineData("charmander")]
    [InlineData("Charmander")]
    [InlineData("ChARManDeR")]
    public async Task GET_Pokemon_returns_OK_response(string name)
    {
        //Arrange
        var pokemonName = name;
        
        await using var app = new TranslationEndpointsTestsApp(x =>
        {
            x.AddSingleton(_translator);
        });

        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"pokemon/{pokemonName}");
        var responseText = await response.Content.ReadAsStringAsync();
        var translationResult = JsonSerializer.Deserialize<TranslationOutput>(responseText);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        translationResult.Should().NotBeNull();
        translationResult.Description.Should().NotBeNull();
        translationResult.Name.Should().Be("Charmander");
    }
    
    [Theory]
    [InlineData("hello")]
    [InlineData("12345")]
    [InlineData("Not a pokemon")]
    public async Task GET_Pokemon_returns_NotFound_response_if_not_exists(string name)
    {
        //Arrange
        var pokemonName = name;

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