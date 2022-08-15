FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY *.sln .
COPY src/Oneonones/*.csproj ./src/Oneonones/
RUN dotnet restore
COPY src/Oneonones/. ./src/Oneonones/
WORKDIR /app/
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Oneonones.dll"]