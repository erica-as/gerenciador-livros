# Build (Compilação)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copia os arquivos de projeto (.csproj) e restaura as dependências
COPY ["src/GerenciadorLivros.API/GerenciadorLivros.API.csproj", "src/GerenciadorLivros.API/"]
COPY ["src/GerenciadorLivros.Domain/GerenciadorLivros.Domain.csproj", "src/GerenciadorLivros.Domain/"]
COPY ["src/GerenciadorLivros.Infrastructure/GerenciadorLivros.Infrastructure.csproj", "src/GerenciadorLivros.Infrastructure/"]
COPY ["src/GerenciadorLivros.Service/GerenciadorLivros.Service.csproj", "src/GerenciadorLivros.Service/"]

RUN dotnet restore "src/GerenciadorLivros.API/GerenciadorLivros.API.csproj"

# Copia o resto do código e compila
COPY . .
WORKDIR "/app/src/GerenciadorLivros.API"
RUN dotnet publish -c Release -o /app/publish

# Runtime (Execução)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GerenciadorLivros.API.dll"]