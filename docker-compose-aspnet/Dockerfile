FROM ubuntu:16.04

RUN apt-get update && apt-get install -y \
        apt-transport-https

RUN sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
RUN apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893
RUN apt-get update && apt-get install -y \
        dotnet-dev-1.0.0-preview2-003131

RUN mkdir ~/aspnetcore
WORKDIR ~/aspnetcore

ADD ./code/ ./
RUN dotnet restore
