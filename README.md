# Build guide

## Requirements

- docker (with docker compose) [link](https://docs.docker.com/engine/install/ubuntu/)
- .net 9 sdk [link](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-install?tabs=dotnet9&pivots=os-linux-ubuntu-2404)
- entity framework .net tool (for DB migrations and updates) [link](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- node.js (for frontend app) [link](https://nodejs.org/en/download)
- angular cli [link](https://angular.dev/installation)

This [script](./install-dependencies-dnet.sh) may be used on Ubuntu 24.04 to install .NET 9 SDK 
This [script](./install-dependencies-dnet-tools.sh) may be used on Ubuntu 24.04 to install .NET EntityFramework tool
THis [script](./install-dependencies-docker.sh) may be used on Ubuntu 24.04 to install docker

**NOTE:** it is recommended to add active dev user to docker group via command:

```bash
sudo usermod -aG docker $USER
```

### Node.js installation

1. Install dependency:

```bash
sudo apt install unzip
```

2. Download fnm (described in details [here](https://nodejs.org/en/download):

```bash
curl -o- https://fnm.vercel.app/install | bash
```

3. Restart terminal (e.g. logout and reloging via ssh)

4. install node.js and npm via fnm:

```bash
fnm install 22

```

### Angular CLI installation (requires node.js with NPM)

```bash
npm install -g @angular/cli
```


## Release build (dockerized)

### 0 Clone this repo

**NOTE:** configured SSH-key authentication is required for submodules to work properly

```bash
git clone --recursive git@git.ooplabs.ru:trspo/course-projects/blogator/blogatorbuild.git
```

### 1 Build frontend app (required node.js)

```bash
./build-admin-panel
```

### 2 Bootstrap containers:

```bash
docker compose pull
docker compose build
docker compose up -d
```

### 3 Check that the containers are up and ready:

```bash
docker compose ps
```

Should yeild something like this:

```
NAME                       IMAGE                    COMMAND                  SERVICE    CREATED          STATUS         PORTS
blogatorbuild-blogator-1   blogatorbuild-blogator   "dotnet BlogAtor.Run…"   blogator   2 seconds ago    Up 2 seconds   0.0.0.0:8080->8080/tcp, :::8080->8080/tcp
blogatorbuild-db-1         postgres                 "docker-entrypoint.s…"   db         24 minutes ago   Up 2 seconds   0.0.0.0:5432->5432/tcp, :::5432->5432/tcp
blogatorbuild-web-1        nginx                    "/docker-entrypoint.…"   web        24 minutes ago   Up 2 seconds   0.0.0.0:80->80/tcp, :::80->80/tcp
```

### 4 Setup database:

0. Add local BlogAtor config to /etc/

- create folder /etc/blogator

```bash
sudo mkdir /etc/blogator/
```

- create local BlogAtor config in '/etc/blogator/StartupConfig.json' via yout text editor of choice. Paste the following text into the config:

```json
{
	"DbConnection" : "Host=localhost; Database=blogator; Username=dbuser; Password=dbpwd"
}
```

1. Add initial migration:

```bash
./add-migration init
```

2. Apply DB schema to PostgreSQL container (db container should be running):

```bash
./setup-db
```

### 5 Check that the site is live:

Go to [localhost](http://localhost) or your PC's IP-address via HTTP (:80) in the browser

**NOTE**:
To drop database use delete DB volumes:

```bash
docker compose down -v
```
