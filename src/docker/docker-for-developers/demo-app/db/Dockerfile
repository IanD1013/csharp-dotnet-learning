# The course bases the seeder on the SQL Server image because that image already
# ships sqlcmd. azure-sql-edge does not, so the seeder is a small Debian image
# with the native go-sqlcmd release dropped in instead.
FROM debian:bookworm-slim

ARG TARGETARCH
ARG SQLCMD_VERSION=1.10.0
RUN apt-get update \
 && apt-get install -y --no-install-recommends curl ca-certificates bzip2 \
 && curl -sSL "https://github.com/microsoft/go-sqlcmd/releases/download/v${SQLCMD_VERSION}/sqlcmd-linux-${TARGETARCH}.tar.bz2" \
    | tar -xj -C /usr/local/bin sqlcmd \
 && rm -rf /var/lib/apt/lists/*

COPY ./wait-and-run.sh /wait-and-run.sh
COPY ./init.sql /init.sql
RUN chmod +x /wait-and-run.sh

CMD ["/wait-and-run.sh"]
