FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS build
WORKDIR /
COPY ./src/ ./src
COPY ./nuget.config .

RUN dotnet restore ./src/PokemonShakespear.Api/PokemonShakespear.Api.csproj --configfile ./nuget.config

COPY . .
WORKDIR "/src/PokemonShakespear.Api"
RUN dotnet build "PokemonShakespear.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonShakespear.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

RUN apk add --update \
    curl \
    && rm -rf /var/cache/apk/*
    
WORKDIR /app
COPY --from=publish /app/publish .
RUN curl https://s3.amazonaws.com/rds-downloads/rds-ca-2019-eu-west-1.pem --output ./rds-ca-2019-eu-west-1.pem 

EXPOSE 5000
HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 CMD curl --fail http://localhost:5000/_system/health || exit 1
ENTRYPOINT ["dotnet", "PokemonShakespear.Api.dll"]