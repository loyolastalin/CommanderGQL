docker-compose up -d
docker-compose stop
dotnet-ef
dotnet ef migrations add AddPlatformToDB
dotnet ef database update
