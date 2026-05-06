# Stage 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV NUGET_FALLBACK_PACKAGES=""

COPY NuGet.config .
COPY Logistics.slnx .
COPY Services/ReportService/ReportService.Api/ReportService.Api.csproj Services/ReportService/ReportService.Api/
COPY Services/ReportService/ReportService.Domain/ReportService.Domain.csproj Services/ReportService/ReportService.Domain/
COPY Services/ReportService/ReportService.Infrastructure/ReportService.Infrastructure.csproj Services/ReportService/ReportService.Infrastructure/
COPY Shared/Shared.Kernel/Shared.Kernel.csproj Shared/Shared.Kernel/

RUN dotnet restore Services/ReportService/ReportService.Api/ReportService.Api.csproj

COPY . .

RUN dotnet publish Services/ReportService/ReportService.Api/ReportService.Api.csproj \
    -c Release \
    -o /app/publish

# Stage 2 - Runtime          ← blank line before this FROM
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN apt-get update && apt-get install -y \
    libgssapi-krb5-2 \
    libfontconfig1 \
    libfreetype6 \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ReportService.Api.dll"]