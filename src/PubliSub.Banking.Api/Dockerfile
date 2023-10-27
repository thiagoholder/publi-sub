#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PubliSub.Banking.Api/PubliSub.Banking.Api.csproj", "PubliSub.Banking.Api/"]
RUN dotnet restore "PubliSub.Banking.Api/PubliSub.Banking.Api.csproj"
COPY . .
WORKDIR "/src/PubliSub.Banking.Api"
RUN dotnet build "PubliSub.Banking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PubliSub.Banking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:9870 \
    ASPNETCORE_ENVIROMENT=Docker
EXPOSE 9870
ENTRYPOINT ["dotnet", "PubliSub.Banking.Api.dll"]