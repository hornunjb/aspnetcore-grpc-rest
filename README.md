# Hybrid REST and gRPC service with ASP.NET Core 3.0

This documents outlines how to get started with a hybrid REST and gRPC service using ASP.NET Core 3.0. 

## Motivation

While looking at migrating existing APIs from REST to gRPC, I struggled to find a working C# example, where I could run a hybrid between the two. I did not want to convert the existing service strictly to gRPC and throw away the REST implementation since legacy services may still depend on it. Instead I wondered if it would be possible add the gRPC component on top of an existing REST service and expose separate ports to run HTTP/1.x and HTTP/2 connections. For a relatively simple idea, I had hoped there would be a various documentation for how to achieve this. But after a bit of researching, I decided to create my own example.

## Build and run the sample with Docker

You can build and run the sample in Docker using the following commands. The instructions assume that you are in the root of the repository. Otherwise, navigate to the folder where your Dockerfile lies.

```console
docker build- t aspnetapp-k8s .
docker run -it --rm -p 9000:4999 -p 9001:5000 --name aspnetcore-sample aspnetapp-k8s
```

You should see the following console output as the application starts.

After the application starts, navigate to `http://localhost:9000/swagger` in your web browser. On Windows, you may need to navigate to the container via IP address. See [ASP.NET Core apps in Windows Containers](aspnetcore-docker-windows.md) for instructions on determining the IP address, using the value of `--name` that you used in `docker run`.

> Note: The run command `-p` argument maps ports 9000 and 9001 on the local machine to ports 4999 and 5000 in the container (the form of the port mapping is `host:container`). See the [Docker run reference](https://docs.docker.com/engine/reference/commandline/run/) for more information on commandline parameters. In some cases, you might see an error because the host port you select is already in use. Choose a different port in that case.

## Build and run the sample on minikube

If you want to run k8s locally you can spin up a whole cluster manually. Another solution is to use minikube. 

You can build and run the sample locally with [minikube](https://kubernetes.io/docs/setup/minikube/). This post won't show you how to install [minikube](https://kubernetes.io/docs/setup/minikube/) or the command line tool [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/).

Build your image:
```console
docker build -t aspnetapp-k8s .
cd aspnetapp
kubectl create -f service.yaml
kubectl create -f deployment.yaml
minikube service aspneta---k8s --url
```

After the application starts, visit `http://localhost:9000/swagger` in your web browser to test the REST component.

For the gRPC piece, you can use a gRPC client to connect to `http://localhost:9001`.
