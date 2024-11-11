# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FastFoodManagement.Web/FastFoodManagement.Web.csproj", "FastFoodManagement.Web/"]
COPY ["FastFoodManagement.Common/FastFoodManagement.Common.csproj", "FastFoodManagement.Common/"]
COPY ["FastFoodManagement.Data/FastFoodManagement.Data.csproj", "FastFoodManagement.Data/"]
COPY ["FastFoodManagement.Model/FastFoodManagement.Model.csproj", "FastFoodManagement.Model/"]
COPY ["FastFoodManagement.Service/FastFoodManagement.Service.csproj", "FastFoodManagement.Service/"]
RUN dotnet restore "./FastFoodManagement.Web/FastFoodManagement.Web.csproj"
COPY . .
WORKDIR "/src/FastFoodManagement.Web"
RUN dotnet build "./FastFoodManagement.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FastFoodManagement.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastFoodManagement.Web.dll"]