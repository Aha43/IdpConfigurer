#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/IdpConfigurer.Application.WebApi/IdpConfigurer.Application.WebApi.csproj", "src/IdpConfigurer.Application.WebApi/"]
COPY ["src/IdpConfigurer.Domain/IdpConfigurer.Domain.csproj", "src/IdpConfigurer.Domain/"]
COPY ["src/IdpConfigurer.Infrastructure.Db/IdpConfigurer.Infrastructure.Db.csproj", "src/IdpConfigurer.Infrastructure.Db/"]
COPY ["src/IdpConfigurer.Specification/IdpConfigurer.Specification.csproj", "src/IdpConfigurer.Specification/"]
COPY ["src/IdpConfigurer.Infrastructure.Memory/IdpConfigurer.Infrastructure.Memory.csproj", "src/IdpConfigurer.Infrastructure.Memory/"]
RUN dotnet restore "src/IdpConfigurer.Application.WebApi/IdpConfigurer.Application.WebApi.csproj"
COPY . .
WORKDIR "/src/src/IdpConfigurer.Application.WebApi"
RUN dotnet build "IdpConfigurer.Application.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdpConfigurer.Application.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdpConfigurer.Application.WebApi.dll"]