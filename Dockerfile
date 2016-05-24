FROM microsoft/dotnet:latest

MAINTAINER f799 <kokudou330@gmail.com>

ENV PROJECT /project

RUN apt-get -y update
#RUN apt-get -y install npm
#RUN npm update -g
#RUN npm install -g n
#RUN n latest
#RUN apt-get -y install node
#RUN npm install -g npm-check-updates
#RUN npm install -g npm
#RUN npm install -g yo
#RUN npm install -g bower
#RUN npm install -g gulp
#RUN npm install -g grunt-cli
#RUN npm install -g generator-aspnet

RUN mkdir $PROJECT
WORKDIR $PROJECT

#RUN dnvm use 1.0.0-rc1-update1 -r coreclr -a x64
