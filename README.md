[![Build Status](https://travis-ci.org/mpaauw/bayonet.svg?branch=master)](https://travis-ci.org/mpaauw/bayonet)

# bayonet :hocho:
A restful API wrapper for the official Hacker News API, written in C#.

## Docker Setup
Clone this repository, navigate to newly-created root directory on your machine. Also ensure that you have Docker installed on your machine.

From root, run the following command to build local project files:
```
dotnet clean; dotnet restore --no-cache; dotnet build
```
From root, run the following commands to build and run a docker container with access the api:
```
docker build -t bayonet .
docker run --rm -p 8080:80 --name bayonet-api bayonet bayonet.Api from Docker
```

Once the docker commands have executed successfully, run `docker ps -a` and you should see the following within your list of running containers:

![alt text](https://user-images.githubusercontent.com/4207462/54870047-aad71e00-4d5e-11e9-982a-6caae41e56ae.png)

Finally, you can go ahead and navigate to `http://localhost:8080/swagger`, where a list of available APIs should be shown, ready to run. And that's it!

NOTE: If you kill the running `bayonet-api` docker container, it will be removed from your container registry per the `--rm` tag in the `run` command; if you wish to retain the container within your registry, simply omit this tag from the `run` command.
