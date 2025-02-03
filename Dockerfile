# Base para execução em produção (Alpine)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

# Base para build do projeto (Alpine)
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Instalar dependências do Alpine, incluindo icu-libs para suporte à globalização
RUN apk add --no-cache icu-libs

# Copia a solução e os projetos para otimizar o cache de dependências
COPY ["PrestacaoNuvem.Api/PrestacaoNuvem.Api.csproj", "PrestacaoNuvem.Api/"]

# Restaura as dependências primeiro para cache otimizado
WORKDIR /src/PrestacaoNuvem.Api
RUN dotnet restore

# Copia todo o código fonte restante
COPY . .

# Compila o projeto e publica os binários
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final minimalista para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Copia apenas os arquivos publicados
COPY --from=build /app/publish .

# Instala icu-libs para suporte à globalização no runtime
RUN apk add --no-cache icu-libs

# Entrada do container
ENTRYPOINT ["dotnet", "PrestacaoNuvem.Api.dll", "--urls", "http://0.0.0.0:80"]
