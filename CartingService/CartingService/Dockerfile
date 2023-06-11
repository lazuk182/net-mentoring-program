#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CartingService/CartingService.API.csproj", "CartingService/"]
COPY ["CartingService.BL/CartingService.BLL.csproj", "CartingService.BL/"]
COPY ["CartingService.DAL/CartingService.DAL.csproj", "CartingService.DAL/"]
RUN dotnet restore "CartingService/CartingService.API.csproj"
COPY . .
WORKDIR "/src/CartingService"
RUN dotnet build "CartingService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CartingService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartingService.API.dll"]