# PartyGuide

## Infrastructure

Build the Docker Image
```shell
docker build -t party-guide .
```

Save the docker image to a tarball
```shell
docker save -o party-guide.tar party-guide
```

SCP the tarball to the Raspberry Pi
```shell
scp party-guide.tar pi@192.168.1.209:/home/pi/party-guide/images/
```

On the Raspberry Pi:
```shell
docker load --input party-guide.tar
```