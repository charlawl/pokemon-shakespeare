namespace PokemonShakespeare.Api.Services;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Models;

public interface ITranslator
{
    public Task<TranslationModel> GetTranslation(string pokemonName);
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
    
    public async Task<TranslationModel> GetTranslation(string pokemonName)
    {
      
        var pokemonDetails = await GetPokemon(pokemonName); //API returns 404 with caps

        if (string.IsNullOrEmpty(pokemonDetails))
        {
            _logger.LogError("No description returned from Pokemon API");
            return null;
        }

        var cleanedPokemonDetails = StripFormatting(pokemonDetails);
        var translatedDetails = await GetShakespeare(cleanedPokemonDetails);
        ;
        if (string.IsNullOrEmpty(translatedDetails))
        {
            _logger.LogError("No translation returned from Shakespeare translation API");
            return null;
        }

        return new TranslationModel()
        {
            Name = pokemonName,
            Sprite = "",
            Translation = translatedDetails
        };
    }

    private async Task<string> GetPokemon (string name)
    {
        var uri = $"{BasePokemonUrl}/pokemon-species/{name}";

        try
        {
            var request = await _client.GetAsync(uri);
            request.EnsureSuccessStatusCode();
            
            var response = await request.Content.ReadAsStringAsync();
            var description = JsonSerializer.Deserialize<PokemonResponse>(response);
            ;
            return description.FlavorTextEntries.Where(x => x.Language?.Name == "en")?.FirstOrDefault()
                ?.FlavorText;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("No data returned from Pokemon API. Exception {@ex}", ex);
            throw;
        }
    }
    
    private async Task<string> GetShakespeare (string? text)
    {
        var uri = $"{BaseShakespeareUrl}?text={text}";
        
        try
        {
            var request = await _client.GetAsync(uri);
            request.EnsureSuccessStatusCode();
            
            var response = await request.Content.ReadAsStringAsync();
            var description = JsonSerializer.Deserialize<ShakespeareModel>(response);
            
            return description.Contents.Translated;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("No data returned from Shakespeare API. Exception {@ex}", ex);
            throw;
        }
    }

    private static string StripFormatting(string input)
    {
        return Regex.Replace(input, @"\s+", " ");
    }
}