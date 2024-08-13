FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY ReservationApp/*.csproj ./ReservationApp/
RUN dotnet restore ReservationApp/ReservationApp.csproj

COPY ReservationApp/. ./ReservationApp/
WORKDIR /app/ReservationApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/ReservationApp/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "ReservationApp.dll"]