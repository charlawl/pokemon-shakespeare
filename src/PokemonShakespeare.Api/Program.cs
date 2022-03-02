using PokemonShakespeare.Api.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<Translator>();

var app = builder.Build();

app.MapGet("/pokemon/{name}", ([FromServices] Translator translator, [FromRoute] string name) 
    => translator.GetTranslation(name));

app.Run();