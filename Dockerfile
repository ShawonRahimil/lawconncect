# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY lawconncect/lawconncect.csproj ./lawconncect/
RUN dotnet restore ./lawconncect/lawconncect.csproj

# Copy all files and publish
COPY . ./
WORKDIR /app/lawconncect
RUN dotnet publish -c Release -o /app/publish

# Run stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "lawconncect.dll"]
