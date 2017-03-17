FROM ubuntu:16.04

# system update 50MB程度
RUN apt-get -y update

# set locale
RUN unlink /etc/localtime
RUN ln -s /usr/share/zoneinfo/Japan /etc/localtime

#RUN apt-get reinstall -y glibc-common
#RUN apt-get reinstall -y glibc

#RUN yum group install -y "Japanese Support"
RUN localedef -f UTF-8 -i ja_JP ja_JP

ENV LANG ja_JP.UTF-8
ENV LC_CTYPE ja_JP.UTF-8
ENV LC_NUMERIC ja_JP.UTF-8
ENV LC_TIME ja_JP.UTF-8
ENV LC_COLLATE ja_JP.UTF-8
ENV LC_MONETARY ja_JP.UTF-8
ENV LC_MESSAGES ja_JP.UTF-8
ENV LC_PAPER ja_JP.UTF-8
ENV LC_NAME ja_JP.UTF-8
ENV LC_ADDRESS ja_JP.UTF-8
ENV LC_TELEPHONE ja_JP.UTF-8
ENV LC_MEASUREMENT ja_JP.UTF-8
ENV LC_IDENTIFICATION ja_JP.UTF-8
ENV LC_ALL ja_JP.UTF-8
ENV LANGUAGE ja_JP:ja

RUN apt-get -y update
RUN apt-get -y install kbd

# editor install 191kB程度
RUN apt-get -y install nano

## dotnet core
RUN apt-get -y install apt-transport-https
## https://github.com/dotnet/core/blob/master/release-notes/download-archive.md
RUN sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 417A0893
RUN apt-get update
RUN apt-get -y install dotnet-dev-1.0.1

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE true
ENV UGET_XMLDOC_MODE skip
#ENV ASPNETCORE_ENVIROMENT=Development,Staging,Production

#How to Use
#docker build -t <tag> .
#docker run -it -p 5000:5000 <tag> /bin/bash
#dotnet restore
#dotnet build
#ASPNETCORE_URLS="http://*:5000" dotnet run
#curl http://192.168.99.100:5000
