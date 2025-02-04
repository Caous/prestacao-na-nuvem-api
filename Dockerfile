# Base para execução em produção (alpine)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

# Base para build do projeto (alpine)
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Instalar as dependências do Alpine, incluindo icu-libs e tzdata para suporte à globalização
RUN apk add --no-cache icu-libs tzdata

# Definir variáveis de ambiente para habilitar a globalização
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LC_ALL=en_US.UTF-8
ENV LANG=en_US.UTF-8

# Copia apenas os arquivos de projeto para otimizar o cache de build
COPY PrestacaoNuvem.Api/PrestacaoNuvem.Api.csproj PrestacaoNuvem.Api/

# Restaura as dependências
RUN dotnet restore PrestacaoNuvem.Api/PrestacaoNuvem.Api.csproj

# Copia o restante do código-fonte
COPY PrestacaoNuvem.Api/ .

# Compila o projeto e publica os binários
WORKDIR /src/PrestacaoNuvem.Api
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final minimalista
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Instalar icu-libs e tzdata novamente para garantir suporte à globalização
RUN apk add --no-cache icu-libs tzdata

# Definir variáveis de ambiente na imagem final
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LC_ALL=en_US.UTF-8
ENV LANG=en_US.UTF-8

# Copia somente os binários publicados da etapa anterior
COPY --from=build /app/publish .

# Limpeza de cache do Alpine para reduzir ainda mais o tamanho
RUN rm -rf /var/cache/apk/*

ENTRYPOINT ["dotnet", "PrestacaoNuvem.Api.dll", "--urls", "http://0.0.0.0:80"]
