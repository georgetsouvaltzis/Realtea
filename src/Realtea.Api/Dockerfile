# TO RUN THIS CONTAINER FROM REALTEA.API DIRECTORY:
# RUN COMMAND: 'docker build -t realtea -f . ../'

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /src

COPY ../Realtea.sln .
COPY ../Realtea.Core/ Realtea.Core/
COPY ../Realtea.Infrastructure/ Realtea.Infrastructure/
COPY ../Realtea.API/ Realtea.Api/
RUN dotnet restore Realtea.sln

RUN dotnet publish -c Release -o realtea

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /src
COPY --from=build-env /src/realtea .

EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "Realtea.Api.dll"]