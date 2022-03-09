namespace PokemonShakespeare.Api.Models;

using System.Text.Json.Serialization;

public class TranslationOutput
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("shakespeare_description")]
    public string? ShakespeareDescription { get; set; }
    [JsonPropertyName("sprite")]
    public string? Sprite { get; set; }
}



