# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY cicdemo/*.csproj ./cicdemo/
COPY CiCDemo.Tests/*.csproj ./CiCDemo.Tests/
RUN dotnet restore

# copy everything else and build app
COPY cicdemo/. ./cicdemo/  
COPY CiCDemo.Tests/. ./CiCDemo.Tests/  
RUN dotnet publish -c release -o /app
COPY cicdemo/azurecontainer.io.crt /app  
COPY cicdemo/azurecontainer.io.key /app  
# --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "cicdemo.dll"]