FROM node:22 AS frontend
WORKDIR /app
COPY frontend/package*.json ./
RUN npm ci
COPY frontend/ .
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["backend/RestaurantHR.Api/RestaurantHR.Api.csproj", "RestaurantHR.Api/"]
COPY ["backend/RestaurantHR.Application/RestaurantHR.Application.csproj", "RestaurantHR.Application/"]
COPY ["backend/RestaurantHR.Domain/RestaurantHR.Domain.csproj", "RestaurantHR.Domain/"]
COPY ["backend/RestaurantHR.Infrastructure/RestaurantHR.Infrastructure.csproj", "RestaurantHR.Infrastructure/"]
RUN dotnet restore "RestaurantHR.Api/RestaurantHR.Api.csproj"
COPY backend/ .
COPY --from=frontend /app/dist wwwroot/
WORKDIR "/src/RestaurantHR.Api"
RUN dotnet build "RestaurantHR.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestaurantHR.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "RestaurantHR.Api.dll"]
