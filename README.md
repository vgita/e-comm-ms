# e-comm-ms

Microservices implementation practice

#### Catalog Service _(manage products)_

-   Vertical slice architecture
-   Minimal APIs definitions (**Carter**)
-   CQRS (**MediatR**)
-   Pipeline Behaviors for Validation (**MediatR, Fluent Validation**)
-   Transactional Document DB with Postgres (**Marten**)
-   other: Pipeline Behaviors for Logging, Global exception handling, health checks

#### Basket Service _(manage basket)_

-   Vertical slice architecture
-   Minimal APIs definitions (**Carter**)
-   CQRS (**MediatR**)
-   Pipeline Behaviors for Validation (**MediatR, Fluent Validation**)
-   Transactional Document DB with Postgres (**Marten**)
-   Distributed Cache (**Redis**)
-   Proxy, Decorator (**Scrutor**) and Cache-aside patterns
-   **gRPC** (Client) communication with Discount Service
-   Publishing events (**MassTransit and RabbitMQ**) (BasketCheckoutEvent)
-   other: Pipeline Behaviors for Logging, Global exception handling, health checks

#### Discount gRPC Service

-   **gRPC** Server, exposing services with Protobuf messages (communication with Basket Service)
-   N-Layer architecture (Data access, Business, Presentation)
-   **_EF Core, SQLite, migrations_**

#### Ordering Service

-   DDD and Clean Architecture
-   Domain Layer Patterns:
    -   tactical DDD tactical
    -   Domain Entity pattern, Entity Base classes
    -   Rich-domain model
    -   Value Object pattern
    -   The Aggregate Root/Root Entity pattern
    -   Strong Type IDs Pattern
    -   Domain Events and Integration Events
-   Infrastructure Data Layer Patterns
    -   Repository patterns
    -   EF Core ORM - Code First - Migrations - DB Seeding
    -   Value Object Complex Types, Ef Aggregate Root Entities
    -   Entity Configuration with ModelBuilder - DDD to EF Core Entity Objects
    -   Dispatch Domain Events with EF Core and **MediatR**
-   Application Use Case Layer Patterns
    -   CQRS Pattern
    -   Command and Command Handler patterns
    -   Mediator Pattern + Pipeline Behaviors
    -   Fluent Validation, Logging Cross-cutting, Global Error handling
    -   Handle Domain (**MediatR**) and Integration Events (**MassTransit and RabbitMQ**)
-   Presentation API Layer Patterns
    -   Minimal APIs (**Carter**)

#### Api Gateway

-   YARP Reverse proxy
-   Configure: Routes, CLusters, Paths, Transforms, Destination
-   Rate Limiting with FixedWindowLimiter

#### Web Client UI

-   .NET Core Web Application with Razor and Bootstrap 4
-   Call YARP endpoints via Refit HttpClientFactory

#### Communication

-   **Sync** inter-service **gRPC** communication
-   **Async** communication with **RabbitMQ**
    -   RabbitMQ Publish/Subscribe Topic Exchange Model
    -   Abstraction over RabbitMQ with **MassTransit**

#### Docker Compose configuration for all services

-   Containerization of microservices
-   Containerization of backing services
-   Override Environment variables

#### Backing Services

-   catalogdb: postgres
-   basketdb: postgres
-   distributedcache: redis
-   orderdb: mssql server
-   messagebroker: rabbitmq
-   yarpapigateway
