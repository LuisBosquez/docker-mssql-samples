FROM ubuntu:16.04

MAINTAINER Dockerfiles

RUN apt-get update && apt-get install -y \
	python \
	python-dev \
	python-pip \
	python-setuptools \
	curl \
	apt-transport-https && sudo rm -rf /var/lib/apt/lists/*

RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
RUN curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | tee /etc/apt/sources.list.d/msprod.list
RUN echo "msodbcsql     msodbcsql/accept_eula boolean true" | debconf-set-selections
RUN apt-get update && apt-get install -y msodbcsql unixodbc-dev-utf16 -y

RUN pip install --upgrade pip
RUN pip install pyodbc