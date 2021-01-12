FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
#
COPY *.sln .
COPY Meetings/*.csproj ./Meetings/
COPY Meetings.Domain/*.csproj ./Meetings.Domain/
COPY Meetings.Infrastructure/*.csproj ./Meetings.Infrastructure/
COPY Meetings.Persistence/*.csproj ./Meetings.Persistence/
COPY Meetings.Service/*.csproj ./Meetings.Service/
#
RUN dotnet restore
#
# copy everything else and build app
COPY Meetings/. ./Meetings/
COPY Meetings.Domain/. ./Meetings.Domain/
COPY Meetings.Infrastructure/. ./Meetings.Infrastructure/
COPY Meetings.Persistence/. ./Meetings.Persistence/
COPY Meetings.Service/. ./Meetings.Service/
#
WORKDIR /app/Meetings
RUN dotnet publish -c Release -o out
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
#
COPY --from=build /app/Meetings/out ./
ENTRYPOINT ["dotnet", "Meetings.dll"]