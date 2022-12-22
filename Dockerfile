FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app


COPY *.sln .
COPY LibraryApp.API/*.csproj ./LibraryApp.API
COPY LibraryApp.Data/*.csproj ./LibraryApp.Data
RUN dotnet restore

COPY LibraryApp.API/. ./LibraryApp.API
COPY LibraryApp.Data/. ./LibraryApp.Data
WORKDIR /app/LibraryApp.API
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/LibraryApp.API/out .
EXPOSE 80
CMD ["dotnet", "LibraryApp.API.dll"]