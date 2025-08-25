# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY ["CartaoVacina.sln", "."]

# Copy project files
COPY ["src/CartaoVacina.API/CartaoVacina.API.csproj", "src/CartaoVacina.API/"]
COPY ["src/CartaoVacina.Application/CartaoVacina.Application.csproj", "src/CartaoVacina.Application/"]
COPY ["src/CartaoVacina.Domain/CartaoVacina.Domain.csproj", "src/CartaoVacina.Domain/"]
COPY ["src/CartaoVacina.Infrastructure/CartaoVacina.Infrastructure.csproj", "src/CartaoVacina.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "src/CartaoVacina.API/CartaoVacina.API.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/src/CartaoVacina.API"
RUN dotnet build "CartaoVacina.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "CartaoVacina.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8 runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published application
COPY --from=publish /app/publish .

# Expose the port the app runs on
EXPOSE 8080
EXPOSE 8081

# Set the entry point
ENTRYPOINT ["dotnet", "CartaoVacina.API.dll"]
