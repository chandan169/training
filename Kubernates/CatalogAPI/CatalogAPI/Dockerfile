#stage 1 Build phase

FROM mcr.microsoft.com/dotnet/core/sdk:2.2.402-bionic AS build
WORKDIR /src
COPY ["CatalogAPI.csproj", "./"]
RUN dotnet restore "./CatalogAPI.csproj"
COPY . .
WORKDIR /src
RUN dotnet build -c Release -o /bin
RUN dotnet publish -c Release -o /dist

#Stage 2

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.7-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV MongoSettings:ConnectionString=mongodb://chandan-cosmos-mongo:06bbBSJ2aRVx5Gr7tz2GlN84gV60sEkOEGrnaiEfGF1caEEX64A9PIzKvZDijOG8gMU6ZRQMBWr5oFh2sPXZ0w==@chandan-cosmos-mongo.documents.azure.com:10255/?ssl=true&replicaSet=globaldb&retryWrites=false
ENV MongoSettings:Database=CatalogDb

COPY --from=build /dist .
ENTRYPOINT ["dotnet", "CatalogAPI.dll"]
