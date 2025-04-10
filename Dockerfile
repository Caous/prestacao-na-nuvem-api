# Base para execução em produção (Alpine)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

# Base para build do projeto (Alpine)
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo de projeto e restaura as dependências
COPY ["PrestacaoNuvem.Api/PrestacaoNuvem.Api.csproj", "PrestacaoNuvem.Api/"]
WORKDIR /src/PrestacaoNuvem.Api
RUN dotnet restore

# Copia todo o código fonte e compila o projeto
COPY ["PrestacaoNuvem.Api/", "PrestacaoNuvem.Api/"]
RUN dotnet publish PrestacaoNuvem.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final minimalista para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Copia apenas os arquivos publicados
COPY --from=build /app/publish .

# Instala icu-libs para suporte à globalização no runtime
RUN apk add --no-cache icu-libs

# Definir variáveis de ambiente para habilitar a globalização
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Entrada do container
ENTRYPOINT ["dotnet", "PrestacaoNuvem.Api.dll", "--urls", "http://0.0.0.0:80"]
