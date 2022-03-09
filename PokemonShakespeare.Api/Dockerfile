FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet run --project PokemonShakespeare.Api.csproj"]

# RUN apt-get update
# RUN apt-get install -y curl
# RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
# RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
# RUN apt-get install -y nodejs



# RUN apt-get update
# RUN apt-get install -y curl
# RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
# RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
# RUN apt-get install -y nodejs


# WORKDIR /PokemonShakespeare.Api
# COPY ["PokemonShakespeare.Api.csproj", "PokemonShakespeare.Api.build/"]
# RUN dotnet restore "PokemonShakespeare.Api.build/PokemonShakespeare.Api.csproj"
# COPY . .
# RUN dotnet build "PokemonShakespeare.Api.csproj" -c Release -o /app/build

# FROM build AS publish
# # COPY --from=client-build ./ClientApp/build /app/publish/ClientApp/build
# RUN dotnet publish "PokemonShakespeare.Api.csproj" -c Release -o /app/publish 

# FROM build AS certs
# RUN dotnet dev-certs https -ep /certs/aspnetapp.pfx --trust

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# COPY --from=certs /certs/* /root/.aspnet/https/
# # ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/root/.aspnet/https/aspnetapp.pfx"
# ENV ASPNETCORE_URLS="http://*:80"
# ENTRYPOINT ["dotnet", "PokemonShakespeare.Api.dll"]
