# Microservices with Publisher-Subscriber Pattern using RabbitMQ

This C# project is an example of a microservices architecture that implements the Publisher-Subscriber (Pub-Sub) pattern using RabbitMQ. The structure of the solution is organized as follows:

## Domain (Project: PubSub.Domain.Core)

Here, we define the core of the application domain related to the Event Bus architecture. This domain provides a solid foundation for creating resilient and extensible systems. It includes the following classes and interfaces:

### Event Bus

The Event Bus is the heart of the Publisher-Subscriber architecture. It is responsible for managing event publishing and subscription throughout the system. The key parts of this domain are:

- **IEventBus**: This is an interface that defines the main operations for handling events. It allows you to publish events, subscribe to them, and manage communication between different parts of the system.

- **IEventHandler**: This interface is used to define event handlers. Handlers implement specific logic for handling events when they are consumed. Having this abstraction makes the application more flexible and extensible, as you can easily add new handlers for different event types.

### Commands

Commands represent actions that need to be executed within the system. In this context, you have an abstract class called `Command` that can be extended to represent specific commands for your application. This helps keep the code clean and organized, making it easy to add new commands in the future.

### Events

Events represent occurrences that happen within the system and are relevant to other components. This section includes the following abstract classes:

- **Event**: This abstract class can be extended to create different types of events that are published and consumed by the system. Events allow different microservices to communicate asynchronously, making the application more scalable and resilient.

- **Message**: This abstract class is used to define different types of messages that are exchanged within the system. It can contain event-specific information or other data relevant to communication between services.

## Infrastructure (Project: PubSub.Infrastructure)

In the Infrastructure layer, we handle the lower-level implementation details necessary to support the Event Bus and the RabbitMQ integration. This layer includes:

### Infra Bus

- **RabbitMQBus**: This class contains the implementation of methods required for sending commands, publishing events, and subscribing to events using RabbitMQ as the underlying message broker. It serves as the bridge between the application and the messaging infrastructure, providing reliable communication between microservices.

### Infra.IoC

This part of the Infrastructure layer focuses on Inversion of Control (IoC) for managing dependencies within the application. It ensures that the necessary components, such as the Event Bus and other infrastructure-related services, are registered and resolved appropriately. IoC helps maintain a clean and modular codebase, making it easier to manage and extend the application.

## Advantages of Publisher-Subscriber Architecture with RabbitMQ

The Publisher-Subscriber architecture, when implemented with RabbitMQ, offers several advantages for building resilient and extensible systems, as mentioned in the previous explanation. The addition of the Infrastructure layer further enhances these benefits by providing a robust foundation for reliable event-based communication and effective dependency management.

![Message Bus](docs/Publish%20and%20Subscriber.png)

# Microservices - Example

In this example, we have two microservices: the **Banking API** and the **TransferAPI**, which demonstrate the use of the Publisher-Subscriber architecture with RabbitMQ. The focus of this solution is to illustrate the advantages of this architecture, and it does not emphasize business rules.

## Banking API

The **Banking API** serves as the main API that receives commands from various applications. One of the commands it handles is the "Transfer." When a transfer command is created, a transfer message is sent to the RabbitMQ queue. This message waits for a subscriber event of the "TransferEvent" type to consume it. The key components in this microservice include:

- **Command Handling**: When a transfer command is received, it is translated into a transfer message and sent to the RabbitMQ queue for asynchronous processing.

- **Event Bus Integration**: The Banking API is responsible for integrating with the event bus to publish messages to the RabbitMQ queue. It utilizes the `RabbitMQBus` from the Infrastructure layer to achieve this.

- **Transfer Message Queue**: The RabbitMQ queue acts as a temporary storage for transfer messages, ensuring reliable message delivery to the TransferAPI. The Banking API enqueues messages to this queue and awaits the processing by the TransferAPI.

## TransferAPI

The **TransferAPI** microservice is responsible for processing transfer events. It subscribes to the RabbitMQ queue to consume transfer messages, process the transfer, and persist the transaction. Key components in this microservice include:

- **Event Subscription**: The TransferAPI registers as a subscriber for "TransferEvent" messages in the RabbitMQ queue. When a transfer message is enqueued, this microservice is notified and processes the transfer.

- **Transfer Event Handling**: Upon receiving a transfer event, the TransferAPI performs the required processing. In a real-world scenario, this could include validating the transaction, updating account balances, and persisting the transaction data.

- **Database Interaction**: The TransferAPI is responsible for interacting with a database to persist transfer-related information. In this example, the focus is on demonstrating the flow of events and the Pub-Sub architecture, so the database interactions are simplified.

## Advantages of the Architecture

The architecture employed in this example, which is based on the Publisher-Subscriber pattern with RabbitMQ, offers several advantages:

1. **Asynchronous Communication**: The use of RabbitMQ allows for asynchronous communication between the Banking API and TransferAPI, enhancing system responsiveness and scalability.

2. **Resilience**: Even if the TransferAPI is temporarily unavailable, the Transfer messages are safely stored in the RabbitMQ queue until processing can occur. This ensures that no transactions are lost.

3. **Scalability**: Both the Banking API and TransferAPI can be scaled independently to handle increased load as the application's demands grow.

4. **Clean Separation of Concerns**: This architecture separates concerns by having the Banking API focus on command handling and message publishing, while the TransferAPI focuses on event subscription and processing. This makes the codebase modular and easier to maintain.

5. **Illustration of Pub-Sub**: The example effectively demonstrates how the Publisher-Subscriber architecture can be used to facilitate message-based communication between microservices.

In summary, this example highlights the power of the Publisher-Subscriber pattern with RabbitMQ in creating distributed, scalable, and responsive microservices architecture. The primary goal is to showcase the architectural advantages without delving into complex business logic.

# Running the Microservices Application Example

To run the microservices application example, you can follow these steps:

**Note**: Make sure you have Docker installed on your machine before proceeding.

1. **Start Infrastructure with Docker Compose**:
   - Open a terminal or command prompt.
   - Navigate to the directory containing the `docker-compose.yaml` file.
   - Run the following command to start the infrastructure services (SQL Server 2019 and RabbitMQ):

   ```bash
   docker-compose up sqlserver rabbitmq --build
   ```

   This command will create containers for SQL Server and RabbitMQ with the specified configuration. It may take a moment to download the necessary Docker images and start the containers.

2. **Run the Banking API and TransferAPI**:
   - Open a terminal or command prompt.
   - Navigate to the directory containing the `docker-compose.yaml` file.
   - Build the Docker image for the Banking API using:

   ```bash
   docker-compose up bankingapi transferapi --build
   ```

   This will start the Banking API on port 8000 and Transfer APi on port 8001.

3. **Access Swagger for Banking API**:
   - Open a web browser.
   - Access the Swagger documentation for the Banking API by navigating to:

   ```
   http://localhost:8000/swagger
   ```

   Here, you can interact with the API. Use the Swagger interface to post a transfer.

5. **Access Swagger for TransferAPI**:
   - Open a web browser.
   - Access the Swagger documentation for the TransferAPI by navigating to:

   ```
   http://localhost:8001/swagger
   ```

   Here, you can interact with the TransferAPI and view the results of processed transfers.

6. **View RabbitMQ Admin**:
   - Open a web browser.
   - Access the RabbitMQ admin panel by navigating to:

   ```
   http://localhost:15672/
   ```

   Use the credentials defined in the Docker Compose file to log in. From the admin panel, you can monitor and manage the RabbitMQ message queue, allowing you to see the messages being processed.

This example demonstrates the architecture's functionality, allowing you to create transfer commands, process transfers, and visualize the flow of messages using RabbitMQ.