# Hybrid REST and gRPC services with ASP.NET Core 3.0

This documents outlines how to get started with a hybrid REST and gRPC service using ASP.NET Core 3.0. 

## Motivation

While looking at migrating existing APIs from REST to gRPC, I struggled to find a working C# example, where I could run a hybrid between the two. I did not want to convert the service strictly to gRPC and throw away the existing REST implmentation, since a lot legacy services may still depend on it. Instead I wondered if it would be possible add the gRPC component on top on an existing REST service and expose separate ports to run HTTP/1.x and HTTP/2 connections. For such a relatively simple idea, I had hoped there would be a various documentation for how to achieve this. But after a bit of researching, I decided to create my own example.

## Build and run the sample with Docker

You can build and run the sample in Docker using the following commands. The instructions assume that you are in the root of the repository. Otherwise, navigate to the folder where your Dockerfile lies.

```console
docker build --pull -t aspnetapp-k8s .
docker run -it --rm -p 9000:4999 -p 9001:5000 --name aspnetcore-sample aspnetapp-k8s
```

You should see the following console output as the application starts. The run command maps port 9000 and 9001 on the local machine to port 4999 and 5000 in the container.

```console
C:\git\dotnet-docker\samples\aspnetapp>docker run --name aspnetcore_sample --rm -it -p 8000:80 aspnetapp
Hosting environment: Production
Content root path: /app
Now listening on: http://[::]:80
Application started. Press Ctrl+C to shut down.
```

After the application starts, navigate to `http://localhost:8000` in your web browser. On Windows, you may need to navigate to the container via IP address. See [ASP.NET Core apps in Windows Containers](aspnetcore-docker-windows.md) for instructions on determining the IP address, using the value of `--name` that you used in `docker run`.

> Note: The `-p` argument maps port 8000 on your local machine to port 80 in the container (the form of the port mapping is `host:container`). See the [Docker run reference](https://docs.docker.com/engine/reference/commandline/run/) for more information on commandline parameters. In some cases, you might see an error because the host port you select is already in use. Choose a different port in that case.

## Build and run the sample locally

You can build and run the sample locally with the [.NET Core 2.2 SDK](https://www.microsoft.com/net/download/core) using the following commands. The commands assume that you are in the root of the repository.

```console
cd samples
cd aspnetapp
dotnet run
```

After the application starts, visit `http://localhost:5000` in your web browser.

You can produce an application that is ready to deploy to production locally using the following command.

```console
dotnet publish -c Release -o out
```

You can run the application using the following commands.

```console
cd out
dotnet aspnetapp.dll
```

Note: The `-c Release` argument builds the application in release mode (the default is debug mode). See the [dotnet publish reference](https://docs.microsoft.com/dotnet/core/tools/dotnet-publish) for more information on commandline parameters.