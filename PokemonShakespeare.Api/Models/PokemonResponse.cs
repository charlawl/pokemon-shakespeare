namespace PokemonShakespeare.Api.Models;

using System.Text.Json.Serialization;

public class PokemonResponse
{
    [JsonPropertyName("flavor_text_entries")]
    public List<FlavorTextEntry>? FlavorTextEntries { get; set; }
    [JsonPropertyName("names")]
    public List<PokemonName>? Names { get; set; }

    public string? GetDescription()
    {
        return FlavorTextEntries?
            .Where(x => x.Language?.Name == "en")?
            .FirstOrDefault()?.FlavorText;
    }

    public string? GetName()
    {
        return Names?.Where(x => x.Language.Name == "en").Select(x => x.Name).FirstOrDefault();
    }
}

public class FlavorTextEntry
{
    [JsonPropertyName("flavor_text")]
    public string? FlavorText { get; set; }
    
    [JsonPropertyName("language")]
    public Language? Language { get; set; }
}


public class PokemonName
{
    [JsonPropertyName("language")]
    public Language Language { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
public class Language
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}



