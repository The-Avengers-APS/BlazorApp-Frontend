# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# GitHub Packages authentication for NuGet
ARG GITHUB_TOKEN
ARG GITHUB_USERNAME

# Copy nuget.config and project files
COPY ["BlazorApp/nuget.config", "BlazorApp/"]
COPY ["BlazorApp/BlazorApp.csproj", "BlazorApp/"]

# Configure NuGet with GitHub Packages credentials and restore
RUN dotnet nuget update source github \
    --source "https://nuget.pkg.github.com/The-Avengers-APS/index.json" \
    --username ${GITHUB_USERNAME} \
    --password ${GITHUB_TOKEN} \
    --store-password-in-clear-text \
    --configfile "BlazorApp/nuget.config" || \
    dotnet nuget add source "https://nuget.pkg.github.com/The-Avengers-APS/index.json" \
    --name github \
    --username ${GITHUB_USERNAME} \
    --password ${GITHUB_TOKEN} \
    --store-password-in-clear-text \
    --configfile "BlazorApp/nuget.config"

RUN dotnet restore "BlazorApp/BlazorApp.csproj" --configfile "BlazorApp/nuget.config"

# Copy source code and build
COPY BlazorApp/ BlazorApp/
RUN dotnet build "BlazorApp/BlazorApp.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "BlazorApp/BlazorApp.csproj" -c Release -o /app/publish

# Runtime stage - nginx to serve static Blazor WASM files
FROM nginx:alpine AS final

COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf

RUN chmod -R 755 /usr/share/nginx/html

EXPOSE 8080

CMD ["nginx", "-g", "daemon off;"]
