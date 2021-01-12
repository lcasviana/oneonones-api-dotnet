FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
#
COPY *.sln .
COPY Meetings/Meetings/*.csproj ./Meetings/Meetings/
COPY Meetings/Meetings.Domain/*.csproj ./Meetings/Meetings.Domain/
COPY Meetings/Meetings.Infrastructure/*.csproj ./Meetings/Meetings.Infrastructure/
COPY Meetings/Meetings.Persistence/*.csproj ./Meetings/Meetings.Persistence/
COPY Meetings/Meetings.Service/*.csproj ./Meetings/Meetings.Service/
COPY Meetings/Meetings.Test.Unit/*.csproj ./Meetings/Meetings.Test.Unit/
#
RUN dotnet restore
#
# copy everything else and build app
COPY Meetings/Meetings/. ./Meetings/Meetings/
COPY Meetings/Meetings.Domain/. ./Meetings/Meetings.Domain/
COPY Meetings/Meetings.Infrastructure/. ./Meetings/Meetings.Infrastructure/
COPY Meetings/Meetings.Persistence/. ./Meetings/Meetings.Persistence/
COPY Meetings/Meetings.Service/. ./Meetings/Meetings.Service/
COPY Meetings/Meetings.Test.Unit/. ./Meetings/Meetings.Test.Unit/
#
WORKDIR /app/
RUN dotnet publish -c Release -o out
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
#
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Meetings.dll"]