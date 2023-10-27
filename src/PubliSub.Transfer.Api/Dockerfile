#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PubliSub.Transfer.Api/PubliSub.Transfer.Api.csproj", "PubliSub.Transfer.Api/"]
RUN dotnet restore "PubliSub.Transfer.Api/PubliSub.Transfer.Api.csproj"
COPY . .
WORKDIR "/src/PubliSub.Transfer.Api"
RUN dotnet build "PubliSub.Transfer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PubliSub.Transfer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:9871 \
    ASPNETCORE_ENVIROMENT=Docker
EXPOSE 9871
ENTRYPOINT ["dotnet", "PubliSub.Transfer.Api.dll"]