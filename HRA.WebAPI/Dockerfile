#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HRA.WebAPI/HRA.WebAPI.csproj", "HRA.WebAPI/"]
RUN dotnet restore "HRA.WebAPI/HRA.WebAPI.csproj"
COPY . .
WORKDIR "/src/HRA.WebAPI"
RUN dotnet build "HRA.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRA.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRA.WebAPI.dll"]