# Stage 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV NUGET_FALLBACK_PACKAGES=""

# Copy NuGet config first
COPY NuGet.config .

# Copy project files (layer caching)
COPY Logistics.slnx .
COPY Services/DriverService/DriverService.Api/DriverService.Api.csproj Services/DriverService/DriverService.Api/
COPY Services/DriverService/DriverService.Domain/DriverService.Domain.csproj Services/DriverService/DriverService.Domain/
COPY Services/DriverService/DriverService.Infrastructure/DriverService.Infrastructure.csproj Services/DriverService/DriverService.Infrastructure/
COPY Shared/Shared.Kernel/Shared.Kernel.csproj Shared/Shared.Kernel/

# Restore
RUN dotnet restore Services/DriverService/DriverService.Api/DriverService.Api.csproj

# Copy everything else
COPY . .

# Publish (restore + build + publish in one step)
RUN dotnet publish Services/DriverService/DriverService.Api/DriverService.Api.csproj \
    -c Release \
    -o /app/publish

# Stage 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DriverService.Api.dll"]