# Docker Proof of Concept

A proof of concept application that combines multiple tools in a single docker compose and kubernetes compatible project.

## Technologies used
- .NET 5.0 web app and hosted services
- SQL Server using EF Core
- RabbitMQ using Masstransit
- Recurring jobs using Quartz.NET

## Setup for docker compose and Visual Studio
Generate a self-signed certificate using `config/request-cert.ps1` or `config/request-cert.sh`

## Setup for kubernetes
- Install [helm](https://helm.sh/docs/intro/install/)
- Install [skaffold](https://skaffold.dev/docs/install/)
- Deploy [nginx ingress controller](https://kubernetes.github.io/ingress-nginx/deploy/)

### Dashboard
Deploy [kubernetes dashboard](https://artifacthub.io/packages/helm/k8s-dashboard/kubernetes-dashboard) without token login

`helm install dashboard kubernetes-dashboard/kubernetes-dashboard --set extraArgs="{--enable-insecure-login=true,--enable-skip-login=true,--disable-settings-authorizer=true}"`