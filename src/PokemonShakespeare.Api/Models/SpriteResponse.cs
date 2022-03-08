namespace PokemonShakespeare.Api.Models;

using System.Text.Json.Serialization;

public class SpriteResponse
{
    [JsonPropertyName("sprites")]
    public Sprite? SpriteLink { get; set; }
}

public class Sprite
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
}