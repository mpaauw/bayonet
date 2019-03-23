FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /app

# copy bayonet.Core library and restore as distinct layers
COPY src/bayonet.Api/*.csproj ./src/bayonet.Api/
COPY src/bayonet.Core/*.csproj ./src/bayonet.Core/
WORKDIR /app/src/bayonet.Api
RUN dotnet restore

# copy bayonet.Data library and restore as distinct layers
COPY src/bayonet.Api/*.csproj ./src/bayonet.Api/
COPY src/bayonet.Data/*.csproj ./src/bayonet.Data/
WORKDIR /app/src/bayonet.Api
RUN dotnet restore

# copy and publish bayonet.Api webapp and libraries
WORKDIR /app/
COPY src/bayonet.Api/. ./src/bayonet.Api/
COPY src/bayonet.Core/. ./src/bayonet.Core/
COPY src/bayonet.Data/. ./src/bayonet.Data/
WORKDIR /app/src/bayonet.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:2.1 AS runtime
WORKDIR /app
COPY --from=build /app/src/bayonet.Api/out ./
ENTRYPOINT ["dotnet", "bayonet.Api.dll"]