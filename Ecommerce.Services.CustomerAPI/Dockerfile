FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ecommerce.Services.CustomerAPI.csproj", "./"]
RUN dotnet restore "Ecommerce.Services.CustomerAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Ecommerce.Services.CustomerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Services.CustomerAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Services.CustomerAPI.dll"]
