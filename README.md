# 🚀 .NET Core Microservices with Clean Architecture

[![.NET](https://img.shields.io/badge/.NET%20Core-8.0-blue)]()
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED)]()
[![Kubernetes](https://img.shields.io/badge/Kubernetes-Orchestration-326CE5)]()
[![License](https://img.shields.io/badge/License-MIT-green)]()

> Clean Architecture + Microservices với **.NET Core**, **Kubernetes**, **RabbitMQ**, **Redis**, **gRPC** và **Azure**.

![Overview](./images/overview.png)

---

## 📑 Mục lục

- [🎯 Features](#-features)
- [🛠 Tech Stack](#-tech-stack)
- [🏗 Architecture Overview](#-architecture-overview)
- [👨‍💻 Target Audience](#-target-audience)
- [📦 Prerequisites](#-prerequisites)
- [🚀 Getting Started](#-getting-started)

---

## 🎯 Features

- ✅ Secure Microservices with Azure AD
- ✅ Cross-cutting concerns (logging, monitoring, caching)
- ✅ API Gateway với **Ocelot** & **Nginx**
- ✅ Messaging với **RabbitMQ + gRPC**
- ✅ Triển khai bằng **Docker + Kubernetes (AKS)**
- ✅ Istio Service Mesh cho traffic management & observability
- ✅ Auto-scaling cho High Availability
- ✅ Frontend với **Next.js**

---

## 🛠 Tech Stack

**Backend**

- .NET Core, gRPC, RabbitMQ, MassTransit, Dapr, Polly

**Frontend**

- React, Next.js

**Databases**

- SQL Server, MongoDB, PostgreSQL, Redis

**Infrastructure**

- Docker, Kubernetes, Istio, Helm, Ocelot, Nginx, Azure

**Observability**

- Elasticsearch, Logstash, Kibana (ELK Stack)

**Validation**

- FluentValidation

![Tech Stack](./images/techstack.png)

---

## 🏗 Architecture Overview

Microservices là một design pattern trong đó ứng dụng được chia thành nhiều module độc lập, giao tiếp qua những boundary rõ ràng. Điều này giúp dễ dàng **develop, test, deploy, và scale** từng phần của hệ thống.

Dự án này minh họa cách:

- Build & deploy microservices với **.NET Core**
- Dùng **Docker & Kubernetes** để container hóa và orchestrate
- Tích hợp **Azure AD** cho auth
- Messaging với **RabbitMQ**
- Inter-service communication với **gRPC**
- Observability & traffic management với **Istio**
- Multi-database support (**SQL Server, MongoDB, PostgreSQL, Redis**)
- API Gateway bằng **Ocelot & Nginx**
- CI/CD với **Helm Charts** & auto-scaling

---

## 👨‍💻 Target Audience

Repo này hữu ích cho:

- Developers muốn học Microservices kiến trúc chuẩn
- Architects thiết kế hệ thống **scalable** & **fault-tolerant**
- Teams tìm kiếm reference implementation cho .NET Microservices hiện đại

---

## 📦 Prerequisites

- Kiến thức cơ bản về **C#**, **Docker**, **Next.js**
- Hiểu biết cơ bản về **Distributed Systems**

---

## 🚀 Getting Started

Clone repo:

```bash
git clone https://github.com/your-repo.git
cd your-repo
```
