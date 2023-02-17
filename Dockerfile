FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Clean.Site/Clean.Site.csproj", "Clean.Site/"]
RUN dotnet restore "Clean.Site/Clean.Site.csproj"
COPY . .
WORKDIR "/src/Clean.Site"
RUN dotnet build "Clean.Site.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clean.Site.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clean.Site.dll"]