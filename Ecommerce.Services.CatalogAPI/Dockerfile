FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ecommerce.Services.CatalogAPI.csproj", "./"]
RUN dotnet restore "Ecommerce.Services.CatalogAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Ecommerce.Services.CatalogAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Services.CatalogAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Services.CatalogAPI.dll"]