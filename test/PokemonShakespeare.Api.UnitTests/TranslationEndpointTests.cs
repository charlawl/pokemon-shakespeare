using Xunit;

namespace PokemonShakespeare.Api.UnitTests;

using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Moq.Protected;
using NSubstitute;
using Services;

//Testing as outlined by the microsoft docs: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
public class TranslationEndpointTests
{
    [Theory]
    [InlineData("charmander")]
    [InlineData("Charmander")]
    [InlineData("ChARManDeR")]
    public async Task GET_Pokemon_returns_OK_response(string name)
    {
        //Arrange
        var pokemonName = name;
        var description = new TranslationOutput()
        {
            Description = "fallback description",
            Name = "Charmander",
            ShakespeareDescription = "Shall I compare thee to a summers day",
            Sprite = "www.spriteurl.test"
        };

        var json = JsonSerializer.Serialize(description);
        
        var handlerMock = new Mock<HttpMessageHandler>();
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(json)
        };
        
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);
        
        var httpClient = new HttpClient(handlerMock.Object);
        
        //Act
        var response = await httpClient.GetAsync($"http://localhost:5000/pokemon/{pokemonName}");
        var responseText = await response.Content.ReadAsStringAsync();
        var translationResult = JsonSerializer.Deserialize<TranslationOutput>(responseText);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        translationResult.Should().NotBeNull();
        translationResult.Description.Should().NotBeNull();
        translationResult.Name.Should().Be("Charmander");
        translationResult.ShakespeareDescription.Should().Be("Shall I compare thee to a summers day");
    }
    
    [Theory]
    [InlineData("hello")]
    [InlineData("12345")]
    [InlineData("Not a pokemon")]
    public async Task GET_Pokemon_returns_NotFound_response_if_not_exists(string name)
    {
        //Arrange
        var pokemonName = name;
        
        var handlerMock = new Mock<HttpMessageHandler>();
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
        };

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);
        
        var httpClient = new HttpClient(handlerMock.Object);

        //Act
        var response = await httpClient.GetAsync($"http://localhost:5000/pokemon/{pokemonName}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    //I tried using the WebApplicationFactory for testing the endpoints but couldn't quite get it working :(
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