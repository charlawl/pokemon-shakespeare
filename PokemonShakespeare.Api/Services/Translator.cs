namespace PokemonShakespeare.Api.Services;

using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using Models;

public interface ITranslator
{
    public Task<TranslationModel?> GetTranslation(string pokemonName);
}
public class Translator : ITranslator
{
    private const string BasePokemonUrl = "https://pokeapi.co/api/v2";
    private readonly string BaseShakespeareUrl = "https://api.funtranslations.com/translate/shakespeare.json";
    private readonly HttpClient _client;
    private readonly ILogger<Translator> _logger;
    
    public Translator(HttpClient client, ILogger<Translator> logger)
    {
        _client = client;
        _logger = logger;
    }
    
    public async Task<TranslationModel?> GetTranslation(string pokemonName)
    {
        
        var pokemonResponse = await GetPokemonDescription(pokemonName.ToLower()); //API returns 404 with caps
        if (pokemonResponse is null)
        {
            _logger.LogError("No description returned from Pokemon API");
            return null;
        }

        var spriteImage = await GetPokemonSprite(pokemonName.ToLower());

        if (string.IsNullOrEmpty(spriteImage))
        {
            _logger.LogError("No link for sprite returned from API");
            return null;
        }
        
        var translatedDetails = await GetShakespeareDescription(pokemonResponse);
        
        return new TranslationModel
        {
            Name = pokemonResponse.GetName(),
            Description = pokemonResponse.GetDescription(),
            ShakespeareDescription = translatedDetails,
            Sprite = spriteImage
        };
    }

    internal async Task<PokemonResponse?> GetPokemonDescription(string name)
    {
        var uri = $"{BasePokemonUrl}/pokemon-species/{name}";

        try
        {
            var request = await _client.GetAsync(uri);
            request.EnsureSuccessStatusCode();

            var response = await request.Content.ReadAsStringAsync();
            var details = JsonSerializer.Deserialize<PokemonResponse>(response);

            return details;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("No data returned from Pokemon API. Exception {@ex}", ex);
            return null;
        }
    }

    internal async Task<string?> GetPokemonSprite(string name)
    {
        var uri = $"{BasePokemonUrl}/pokemon/{name}";

        try
        {
            var request = await _client.GetAsync(uri);
            request.EnsureSuccessStatusCode();

            var response = await request.Content.ReadAsStringAsync();
            var sprite = JsonSerializer.Deserialize<SpriteResponse>(response);

            var result = sprite?.SpriteLink?.FrontDefault;
            
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("No sprite data returned. Exception: {@ex}", ex);
            return null;
        }
    }
    
    internal async Task<string?> GetShakespeareDescription(PokemonResponse? details)
    {
        var strippedInput = StripFormatting(details?.GetDescription());
        var uri = $"{BaseShakespeareUrl}?text={strippedInput}";
        
        try
        {
            var request = await _client.GetAsync(uri);
            request.EnsureSuccessStatusCode();
            
            var response = await request.Content.ReadAsStringAsync();
            var description = JsonSerializer.Deserialize<ShakespeareModel>(response);
            
            return description?.Contents.Translated;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("No data returned from Shakespeare API. Exception {@ex}", ex);
            return null;
        }
    }

    internal static string StripFormatting(string input)
    {
        return Regex.Replace(input, @"\s+", " ");
    }
}