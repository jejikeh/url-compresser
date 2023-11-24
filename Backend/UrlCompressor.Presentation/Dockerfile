FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Backend/UrlCompressor.Presentation/UrlCompressor.Presentation.csproj", "Backend/UrlCompressor.Presentation/"]
RUN dotnet restore "Backend/UrlCompressor.Presentation/UrlCompressor.Presentation.csproj"
COPY . .
WORKDIR "/src/Backend/UrlCompressor.Presentation"
RUN dotnet build "UrlCompressor.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlCompressor.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlCompressor.Presentation.dll"]
