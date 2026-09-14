#!/bin/bash

# azure-sql-edge regenerates a self-signed certificate on every start, and roughly
# half of them carry a negative serial number, which Go's x509 parser rejects
# outright. go-sqlcmd is a Go binary, so -C is not enough: -N disable skips the
# TLS handshake entirely.

# Wait for SQL Server to be ready
echo "Waiting for SQL Server to be ready..."
for i in {1..50};
do
    sqlcmd -N disable -C -S database -U sa -P Dometrain#123 -Q "SELECT 1" > /dev/null 2>&1
    if [ $? -eq 0 ]
    then
        echo "SQL Server is ready."
        break
    else
        echo "Not ready yet..."
        sleep 1
    fi
done

# Run the SQL script
sqlcmd -N disable -C -S database -U sa -P Dometrain#123 -d master -i /init.sql
