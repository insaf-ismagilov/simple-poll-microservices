FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ./src/SimplePoll.Common ./SimplePoll.Common
COPY ./src/SimplePoll.Common.DataAccess ./SimplePoll.Common.DataAccess
COPY ./src/SimplePoll.Identity ./SimplePoll.Identity

WORKDIR /src/SimplePoll.Identity
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /src/SimplePoll.Identity/out ./

ENTRYPOINT ["dotnet", "SimplePoll.Identity.dll"]