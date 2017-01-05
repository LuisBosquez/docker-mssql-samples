# Quickstart Compose and ASP.NET Core with SQL Server
This quick-start guide demonstrates how to use Docker Compose to set up and run a simple Django/PostgreSQL app. Before starting, you’ll need to have Compose installed.

# Define the project
For this project, you will need a Dockerfile, a few code files and a docker-compose.yml YAML file.
1. Create an empty project directory.
1. Create a new Dockerfile in your project directory. 
1. Add the following content to the Dockerfile
    
    > FROM ubuntu:16.04
    >
    > RUN apt-get update && apt-get install -y \
            apt-transport-https
    > 
    > RUN sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
    >
    > RUN apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893
    >
    > RUN apt-get update && apt-get install -y \\\
    >       dotnet-dev-1.0.0-preview2-003131
    >
    > RUN mkdir ~/aspnetcore
    > WORKDIR ~/aspnetcore
    > 
    > ADD ./code/ ./
    > RUN dotnet restore
