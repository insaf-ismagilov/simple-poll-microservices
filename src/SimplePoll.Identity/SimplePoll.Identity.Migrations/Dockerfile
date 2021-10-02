FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ./src/SimplePoll.Identity.Migrations ./SimplePoll.Identity.Migrations
COPY ./src/SimplePoll.Common.Migrations ./SimplePoll.Common.Migrations

WORKDIR /src/SimplePoll.Identity.Migrations
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /src/SimplePoll.Identity.Migrations/out ./

ENTRYPOINT ["dotnet", "SimplePoll.Identity.Migrations.dll"]