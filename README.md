# Ecommerce-Microservices-.Net

## Prerequisites

1. Install git - [https://git-scm.com/downloads](https://git-scm.com/downloads).
2. Install .NET Core 6.0 - [https://dotnet.microsoft.com/download/dotnet/6.0](https://dotnet.microsoft.com/download/dotnet/6.0).
3. Install Visual Studio 2022, Rider or VSCode.
4. Install docker - [https://docs.docker.com/docker-for-windows/install/](https://docs.docker.com/docker-for-windows/install/).
5. Make sure that you have ~10GB disk space.
6. Clone Project [https://github.com/kaankvrck/Ecommerce-Microservices-.Net](https://github.com/kaankvrck/Ecommerce-Microservices-.Net), make sure that's compiling
7. Run the [docker-compose.yml](./docker-compose.yml) file, for running prerequisites infrastructures with `docker-compose up --build` command.
8. Open [EcommerceMicroservices.sln](./EcommerceMicroservices.sln) solution.

## How to run

### Using Docker-Compose

1. Go to main path ...\Ecommerce-Microservices-.Net and run: `docker-compose up`.
2. Wait until all dockers got are downloaded and running.
3. You should automatically get:
    - Postgres running
      - Customers Database, Available at: [http://localhost:5433](http://localhost:5433)
      - Catalog Database, Available at: [http://localhost:5434](http://localhost:5434)
      - Order Database, Available at: [http://localhost:5435](http://localhost:5435)
    - Microservies running and accessible:
      - Api Gateway, Available at: [http://localhost:7000](http://localhost:7000) (Not Yet)
      - Customers Service, Available at: [http://localhost:7001](http://localhost:7001)
      - Catalogs Service, Available at: [http://localhost:7002](http://localhost:7002)
      - Order Service, Available at: [http://localhost:7003](http://localhost:7003)

Some useful docker commands:

``` powershell
// start dockers
docker-compose -f .\docker-compose.yml up

// build without caching
docker-compose -f .\docker-compose.yml build --no-cache

// to stop running dockers
docker-compose kill

// to clean stopped dockers
docker-compose down -v

// showing running dockers
docker ps

// to show all dockers (also stopped)
docker ps -a
```
