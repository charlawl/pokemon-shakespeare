namespace PokemonShakespeare.Api.Endpoints;

using Microsoft.AspNetCore.Mvc;
using Services;

public static class TranslationEndpoints 
{
    public static void MapTranslationEndpoints(this WebApplication app)
    {
        app.MapGet("/pokemon/{name}", GetTranslation);
    }
    
    public static void AddTranslationService(this IServiceCollection services)
    {
        services.AddHttpClient<Translator>();
    }
    internal static async Task<IResult> GetTranslation(Translator translator, [FromRoute] string name)
    {
        var translation = await translator.GetTranslation(name);
        return translation is not null ? Results.Ok(translation) : Results.NotFound() ;
    }
}