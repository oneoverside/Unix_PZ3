# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy the project files and restore as distinct layers
COPY src/Dictionary.API/*.csproj ./src/Dictionary.API/
COPY src/Dictionary.Application/*.csproj ./src/Dictionary.Application/
COPY src/Dictionary.Infrastructure/*.csproj ./src/Dictionary.Infrastructure/

RUN dotnet restore ./src/Dictionary.API/Dictionary.API.csproj

# Copy the entire project directories
COPY src/Dictionary.API/ ./src/Dictionary.API/
COPY src/Dictionary.Application/ ./src/Dictionary.Application/
COPY src/Dictionary.Infrastructure/ ./src/Dictionary.Infrastructure/

# Build the project
RUN dotnet publish -c Release -o out ./src/Dictionary.API/Dictionary.API.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory.
WORKDIR /app

# Set environment variable
ENV ConnectionStrings__PostgresConnectionString="Server=postgres;Database=myDataBase;User Id=myUsername;Password=myPassword;"
ENV ConnectionStrings__RedisConnection="127.0.0.1:6379"

# Copy the published output from the build stage
COPY --from=build-env /app/out .

# Expose port 5000 for the application.
EXPOSE 5000

# Start the application
CMD ["dotnet", "Dictionary.API.dll"]