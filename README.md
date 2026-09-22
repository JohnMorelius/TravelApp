# TravelApp

Individuellt projekt i kursen Dataåtkomster i .NET

**Namn:** John Morelius

## Förutsättningar

- .NET 10 SDK
- Docker Desktop

## Kom igång

### 1. Klona repot
git clone https://github.com/JohnMorelius/TravelApp.git
cd TravelApp 


### 2. Lägg in user-secrets

Zip-filen som lämnades in tillsammans med detta repo innehåller en `secrets.json`-fil.
Lägg den filen på följande plats (skapa mapparna om de inte finns): %APPDATA%\Microsoft\UserSecrets\31a61ea2-578d-478d-baf7-af9db4d9d186\secrets.json 


### 3. Starta SQL Server i Docker 

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=skYhgS@83#aQ" -p 14333:1433 --name sql2022container -d mcr.microsoft.com/mssql/server:2022-latest


(Om en container med samma namn redan finns, kör istället `docker start sql2022container`.)

### 4. Skapa databasen (kör EF Core-migrationer)

dotnet ef database update --project Models --startup-project MyWebApi


### 5. Starta WebApi

dotnet run --project MyWebApi 


### 6. Öppna Swagger

Gå till `http://localhost:5193/swagger` i webbläsaren.

### 7. Seeda testdata

Kör `POST /api/Seed` i Swagger för att fylla databasen med testdata (minst 50 användare, 100+ orter över 4+ länder, 1000+ sevärdheter).

## Öppna i VS Code

Dubbelklicka på `TravelApp.code-workspace` för att öppna hela projektet direkt.