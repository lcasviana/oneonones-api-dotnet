FROM mcr.microsoft.com/dotnet/core/sdk:5.0 AS build
WORKDIR /app
#
COPY *.sln .
COPY Oneonones/Oneonones/*.csproj ./Oneonones/Oneonones/
COPY Oneonones/Oneonones.Domain/*.csproj ./Oneonones/Oneonones.Domain/
COPY Oneonones/Oneonones.Infrastructure/*.csproj ./Oneonones/Oneonones.Infrastructure/
COPY Oneonones/Oneonones.Persistence/*.csproj ./Oneonones/Oneonones.Persistence/
COPY Oneonones/Oneonones.Service/*.csproj ./Oneonones/Oneonones.Service/
COPY Oneonones/Oneonones.Test.Unit/*.csproj ./Oneonones/Oneonones.Test.Unit/
#
RUN dotnet restore
#
# copy everything else and build app
COPY Oneonones/Oneonones/. ./Oneonones/Oneonones/
COPY Oneonones/Oneonones.Domain/. ./Oneonones/Oneonones.Domain/
COPY Oneonones/Oneonones.Infrastructure/. ./Oneonones/Oneonones.Infrastructure/
COPY Oneonones/Oneonones.Persistence/. ./Oneonones/Oneonones.Persistence/
COPY Oneonones/Oneonones.Service/. ./Oneonones/Oneonones.Service/
COPY Oneonones/Oneonones.Test.Unit/. ./Oneonones/Oneonones.Test.Unit/
#
WORKDIR /app/
RUN dotnet publish -c Release -o out
#
FROM mcr.microsoft.com/dotnet/core/aspnet:5.0 AS runtime
WORKDIR /app
#
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Oneonones.dll"]