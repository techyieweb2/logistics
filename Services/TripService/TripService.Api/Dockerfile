# Stage 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV NUGET_FALLBACK_PACKAGES=""

# Copy NuGet config first
COPY NuGet.config .

# Copy project files (layer caching)
COPY Logistics.slnx .
COPY Services/TripService/TripService.Api/TripService.Api.csproj Services/TripService/TripService.Api/
COPY Services/TripService/TripService.Domain/TripService.Domain.csproj Services/TripService/TripService.Domain/
COPY Services/TripService/TripService.Infrastructure/TripService.Infrastructure.csproj Services/TripService/TripService.Infrastructure/
COPY Shared/Shared.Kernel/Shared.Kernel.csproj Shared/Shared.Kernel/

# Restore
RUN dotnet restore Services/TripService/TripService.Api/TripService.Api.csproj

# Copy everything else
COPY . .

# Publish (restore + build + publish in one step)
RUN dotnet publish Services/TripService/TripService.Api/TripService.Api.csproj \
    -c Release \
    -o /app/publish

# Stage 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "TripService.Api.dll"]