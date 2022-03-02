namespace PokemonShakespeare.Api.Models;

using System.Text.Json.Serialization;

public class PokemonResponse
{
    public PokemonResponse()
    {
    }

    [JsonPropertyName("flavor_text_entries")]
    public List<FlavorTextEntry>? FlavorTextEntries { get; set; }
}

public class FlavorTextEntry
{
    [JsonPropertyName("flavor_text")]
    public string? FlavorText { get; set; }
    
    [JsonPropertyName("language")]
    public Language? Language { get; set; }
}

public class Language
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
