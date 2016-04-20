# 共通

Dockerfile作成
`docker build -t <tag> .`

# Windows

Toolbox for Docker

chocoratey
choco install docker
choco install docker-machine

```
c:/Users > docker run -v /${PWD}:/project -it user/tag bash
```

# Linux (Redhat CentOS)

```
yum -y install docker.io
```

# Linux (Ubuntu-Debian)

```
apt-get install -y docker.io
```

```
apt-get install -y lxc-docker
```

ローカルファイルを使用したテストコマンド実行  
`docker run -v ${PWD}:/project -it <image> bash -c 'command.sh'`

# Mac OS

pending


