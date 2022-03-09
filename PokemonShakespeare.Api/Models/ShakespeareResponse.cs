namespace PokemonShakespeare.Api.Models;

using System.Text.Json.Serialization;

public class ShakespeareModel
{
    [JsonPropertyName("contents")]
    public Content Contents { get; set; }
}

public class Content
{
    [JsonPropertyName("translated")]
    public string? Translated { get; set; }
    
    [JsonPropertyName("text")]
    public string? Text { get; set; }
    
    [JsonPropertyName("translation")]
    public string? Translation { get; set; }
}