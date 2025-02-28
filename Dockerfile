FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY AnimesLand.Api/*.csproj AnimesLand.Api/
COPY AnimesLand.Application/*.csproj AnimesLand.Application/
COPY AnimesLand.Domain/*.csproj AnimesLand.Domain/
COPY AnimesLand.Infrastructure/*.csproj AnimesLand.Infrastructure/

RUN dotnet restore

COPY . .
WORKDIR /src/AnimesLand.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "AnimesLand.Api.dll"]