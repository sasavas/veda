FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Veda.Api/Veda.Api.csproj", "Veda.Api/"]
RUN dotnet restore "Veda.Api/Veda.Api.csproj"
COPY . .
WORKDIR "/src/Veda.Api"
RUN dotnet build "Veda.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Veda.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Veda.Api.dll"]
