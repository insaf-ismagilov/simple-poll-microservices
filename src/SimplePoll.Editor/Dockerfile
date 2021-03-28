FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ./src/SimplePoll.Common ./SimplePoll.Common
COPY ./src/SimplePoll.Common.DataAccess ./SimplePoll.Common.DataAccess
COPY ./src/SimplePoll.Common.Authentication ./SimplePoll.Common.Authentication
COPY ./src/SimplePoll.Editor ./SimplePoll.Editor

WORKDIR /src/SimplePoll.Editor
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /src/SimplePoll.Editor/out ./

ENTRYPOINT ["dotnet", "SimplePoll.Editor.dll"]