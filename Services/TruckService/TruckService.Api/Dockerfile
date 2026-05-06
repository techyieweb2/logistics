# Stage 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV NUGET_FALLBACK_PACKAGES=""

# Copy NuGet config first
COPY NuGet.config .

# Copy project files (layer caching)
COPY Logistics.slnx .
COPY Services/TruckService/TruckService.Api/TruckService.Api.csproj Services/TruckService/TruckService.Api/
COPY Services/TruckService/TruckService.Domain/TruckService.Domain.csproj Services/TruckService/TruckService.Domain/
COPY Services/TruckService/TruckService.Infrastructure/TruckService.Infrastructure.csproj Services/TruckService/TruckService.Infrastructure/
COPY Shared/Shared.Kernel/Shared.Kernel.csproj Shared/Shared.Kernel/

# Restore
RUN dotnet restore Services/TruckService/TruckService.Api/TruckService.Api.csproj

# Copy everything else
COPY . .

# Publish (restore + build + publish in one step)
RUN dotnet publish Services/TruckService/TruckService.Api/TruckService.Api.csproj \
    -c Release \
    -o /app/publish

# Stage 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "TruckService.Api.dll"]