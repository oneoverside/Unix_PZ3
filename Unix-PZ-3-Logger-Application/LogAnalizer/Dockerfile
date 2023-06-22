# Base SDK Image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./LogAnalizer.csproj", "."]
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Base Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

# Define command to run the application
ENTRYPOINT ["dotnet", "LogAnalizer.dll"]