# ===== STAGE 1: build =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copia csproj e restaura dependências
COPY ordem-servico-backend.csproj ./
RUN dotnet restore

# copia o resto do código
COPY . ./

# publica em Release
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ===== STAGE 2: runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# copia arquivos publicados
COPY --from=build /app/publish ./

# porta em que o Kestrel vai escutar
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

ENTRYPOINT ["dotnet", "ordem-servico-backend.dll"]
