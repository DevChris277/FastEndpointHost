# TAG.Api

## docker image for SQL:
docker run -e 'HOMEBREW_NO_ENV_FILTERING=1' -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=199815Chris' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest