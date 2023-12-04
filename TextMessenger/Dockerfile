#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TextMessenger/TextMessenger.csproj", "TextMessenger/"]
COPY ["TextMessenger.DataAccessLayer/TextMessenger.DataAccessLayer.csproj", "TextMessenger.DataAccessLayer/"]
COPY ["TextMessenger.DataLayer/TextMessenger.DataLayer.csproj", "TextMessenger.DataLayer/"]
COPY ["TextMessenger.Services/TextMessenger.Services.csproj", "TextMessenger.Services/"]
RUN dotnet restore "TextMessenger/TextMessenger.csproj"
COPY . .
WORKDIR "/src/TextMessenger"
RUN dotnet build "TextMessenger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TextMessenger.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TextMessenger.dll"]