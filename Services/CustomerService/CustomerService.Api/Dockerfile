# Stage 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV NUGET_FALLBACK_PACKAGES=""

# Copy NuGet config first
COPY NuGet.config .

# Copy project files (layer caching)
COPY Logistics.slnx .
COPY Services/CustomerService/CustomerService.Api/CustomerService.Api.csproj Services/CustomerService/CustomerService.Api/
COPY Services/CustomerService/CustomerService.Domain/CustomerService.Domain.csproj Services/CustomerService/CustomerService.Domain/
COPY Services/CustomerService/CustomerService.Infrastructure/CustomerService.Infrastructure.csproj Services/CustomerService/CustomerService.Infrastructure/
COPY Shared/Shared.Kernel/Shared.Kernel.csproj Shared/Shared.Kernel/

# Restore
RUN dotnet restore Services/CustomerService/CustomerService.Api/CustomerService.Api.csproj

# Copy everything else
COPY . .

# Publish (restore + build + publish in one step)
RUN dotnet publish Services/CustomerService/CustomerService.Api/CustomerService.Api.csproj \
    -c Release \
    -o /app/publish

# Stage 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "CustomerService.Api.dll"]