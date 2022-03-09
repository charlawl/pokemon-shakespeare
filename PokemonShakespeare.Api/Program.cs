using PokemonShakespeare.Api.Endpoints;

var builder = WebApplication.CreateBuilder();

builder.Services.AddTranslationService();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapTranslationEndpoints();
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program { }

