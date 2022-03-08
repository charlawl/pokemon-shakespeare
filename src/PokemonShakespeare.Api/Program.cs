using PokemonShakespeare.Api.Endpoints;

var builder = WebApplication.CreateBuilder();

builder.Services.AddTranslationService();

var app = builder.Build();

app.MapTranslationEndpoints();

app.Run();

public partial class Program { }

