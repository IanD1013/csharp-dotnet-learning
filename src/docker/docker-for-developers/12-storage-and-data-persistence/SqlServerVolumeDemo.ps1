# Lesson 3: Mounting volumes in containers.
#
# Deviations from the course script:
#   - azure-sql-edge instead of mssql/server, which publishes no arm64 image.
#   - the course pauses on Read-Host while you seed the database by hand from a
#     SQL client. This seeds it from a one-off container instead, so the script
#     runs start to finish. azure-sql-edge ships no sqlcmd, so the helper image
#     is the chapter 9 seeder (demo-app/db) rebuilt under a local tag; it already
#     carries go-sqlcmd and /init.sql.

"Building the sqlcmd helper image..."
docker build -q -t ch12-sqlcmd ../demo-app/db

"Ensure container doesn't exist from previous run of this script..."
docker rm -f sqlserver-withvol

"Ensuring our volume doesn't exist..."
docker volume rm sqldb-data

"Creating SQL Server container using volume..."
docker run `
  --name sqlserver-withvol `
  -e "ACCEPT_EULA=1" `
  -e "MSSQL_SA_PASSWORD=Dometrain#123" `
  -p 1433:1433 `
  -d `
  -v sqldb-data:/var/opt/mssql `
  mcr.microsoft.com/azure-sql-edge:latest

# Note that --mount syntax looks like this:
# --mount "type=volume,source=sqldb-data,target=/var/opt/mssql"

"Seeding the database..."
docker run --rm --network container:sqlserver-withvol ch12-sqlcmd sh -c `
  "until sqlcmd -N disable -C -S localhost -U sa -P 'Dometrain#123' -Q 'SELECT 1' > /dev/null 2>&1; do echo 'Not ready yet...'; sleep 2; done; echo 'SQL Server is ready.'; sqlcmd -N disable -C -S localhost -U sa -P 'Dometrain#123' -i /init.sql"

"Deleting SQL Server container..."
docker rm -f sqlserver-withvol

"Listing all containers..."
docker ps -a

"Listing all volumes..."
docker volume ls

"Creating another SQL Server container using the same volume..."
docker run `
  --name sqlserver-withvol `
  -e "ACCEPT_EULA=1" `
  -e "MSSQL_SA_PASSWORD=Dometrain#123" `
  -p 1433:1433 `
  -d `
  -v sqldb-data:/var/opt/mssql `
  mcr.microsoft.com/azure-sql-edge:latest

"Querying the brand new container - the seeded rows should still be there..."
docker run --rm --network container:sqlserver-withvol ch12-sqlcmd sh -c `
  "until sqlcmd -N disable -C -S localhost -U sa -P 'Dometrain#123' -Q 'SELECT 1' > /dev/null 2>&1; do echo 'Not ready yet...'; sleep 2; done; sqlcmd -N disable -C -S localhost -U sa -P 'Dometrain#123' -d podcasts -Q 'SELECT COUNT(*) AS Total FROM Podcasts'"
