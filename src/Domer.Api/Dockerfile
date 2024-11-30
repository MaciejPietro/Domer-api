##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080

# Install cultures (same approach as Alpine SDK image)
RUN apk add --no-cache icu-libs

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src
COPY ["./Directory.Packages.props", "./"]
COPY ["src/Domer.Api/Domer.Api.csproj", "src/Domer.Api/"]
COPY ["src/Domer.Application/Domer.Application.csproj", "src/Domer.Application/"]
COPY ["src/Domer.Domain/Domer.Domain.csproj", "src/Domer.Domain/"]
COPY ["src/Domer.Infrastructure/Domer.Infrastructure.csproj", "src/Domer.Infrastructure/"]
COPY . .
WORKDIR "/src/src/Domer.Api"

FROM build AS publish
RUN dotnet publish "Domer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID 
ENTRYPOINT ["dotnet", "Domer.Api.dll"]