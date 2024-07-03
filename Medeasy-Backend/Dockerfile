FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5281

ENV ASPNETCORE_URLS=http://+:5281

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src 

COPY ["Medeasy-Backend.csproj", "Medeasy-Backend/"]

RUN dotnet restore "Medeasy-Backend/Medeasy-Backend.csproj"

COPY . .
WORKDIR "/src/Medeasy-Backend"
RUN dotnet build "Medeasy-Backend.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Medeasy-Backend.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medeasy-Backend.dll"]
