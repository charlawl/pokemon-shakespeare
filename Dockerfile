﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY "PokemonShakespeare.Api/PokemonShakespeare.Api.csproj" "PokemonShakespeare.Api/"
RUN dotnet restore "PokemonShakespeare.Api/PokemonShakespeare.Api.csproj"
COPY . .
WORKDIR "/src/PokemonShakespeare.Api"
RUN dotnet build "PokemonShakespeare.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN npm cache clean --force
RUN npm cache verify
RUN npm i npm@latest -g

RUN dotnet publish "PokemonShakespeare.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokemonShakespeare.Api.dll"]
