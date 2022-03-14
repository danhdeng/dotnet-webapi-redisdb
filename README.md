# dotnet-webapi-redisdb

# start a container in background detached mode

docker compose up -d

# docker compose down to remove the container

docker compose down

# stop the container

docker compose stop

# access the docker container from the local desktop remotely

docker exec -it {container_id} /bin/bash

docker exec -it 87675c89ba0a /bin/bash

# REDIS-CLI

# To start REDIS-CLI shell

redis-cli

# test REDIS server running properly with redis-cli

redis-cli ping

# set data in redis with redis-cli

# set objectName:objectKey objectValue

set platform:10010 Docker

# get data from redis with redis-cli

# get objectName:objectKey

get platform:10010

# delete object from redis with redis-cli

del platform:10010

# redis commands reference

https://redis.io/commands

# packages required for this Project

dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
