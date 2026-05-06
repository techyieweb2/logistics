# Stage 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV NUGET_FALLBACK_PACKAGES=""

# Copy NuGet config first
COPY NuGet.config .

# Copy project files (layer caching)
COPY Logistics.slnx .
COPY Services/DispatcherService/DispatcherService.Api/DispatcherService.Api.csproj Services/DispatcherService/DispatcherService.Api/
COPY Services/DispatcherService/DispatcherService.Domain/DispatcherService.Domain.csproj Services/DispatcherService/DispatcherService.Domain/
COPY Services/DispatcherService/DispatcherService.Infrastructure/DispatcherService.Infrastructure.csproj Services/DispatcherService/DispatcherService.Infrastructure/
COPY Shared/Shared.Kernel/Shared.Kernel.csproj Shared/Shared.Kernel/

# Restore
RUN dotnet restore Services/DispatcherService/DispatcherService.Api/DispatcherService.Api.csproj

# Copy everything else
COPY . .

# Publish (restore + build + publish in one step)
RUN dotnet publish Services/DispatcherService/DispatcherService.Api/DispatcherService.Api.csproj \
    -c Release \
    -o /app/publish

# Stage 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DispatcherService.Api.dll"]