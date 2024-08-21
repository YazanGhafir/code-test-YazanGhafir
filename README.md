# Project Documentation

## Overview

This project adopts a modular monolith architecture, leveraging the principles of clean architecture and model-driven design. It is structured into domain, application, infrastructure, and integration layers within each module.

#### Please do note that I could make the project as a single module with clean architecture which is actually more suitable architecture for the size of the project but i wanted to emphisize the ability for scalability in architecture and to show how easily and effectivly the project can be extended with more clean modules, each of witch having its own bounded context, configuration and startup instantiation.

## Self-made Assessment of the code quality, automation and performance of the Solution
[Assessment](https://docs.google.com/spreadsheets/d/1He5BKqCYXbLL8ms3XECTlkfk_YESlFtIHzlVyyIwLVY/edit?usp=sharing)  

## Solution Structure
![Solution Structure](https://github.com/hedin-it/code-test-YazanGhafir/blob/main/docs/Solution%20Structure.png)

## Features

### Configurable and Scalable Architecture

- **Modular Design**: Emphasizes scalability and ease of extension with self-contained modules, each possessing distinct bounded contexts.
- **Clean Architecture**: Ensures separation of concerns, promoting maintainability and testability.

### Configuration and Logging

- **Auto-instantiation and Validation of Configuration**: Implements a simple yet helpful new approach for automatic configuration setup and validation, enhancing the robustness of application settings management.
- **Serilog Logging**: Utilizes Serilog for comprehensive logging capabilities, supporting both global and module-specific configurations.
- **Logs**: Added logs mostly to the API controllers while utilizing scopes and abstracting where the logs are persisted in program.cs.

### Integration and Service Communication

- **Module Initializer**: Facilitates the registration of services for each module. Each module has its own Startup. All services are instantiated in the global IoC container
- **Simple EventBus Implementation**: Supports inter-module communication, demonstrating a foundational event bus mechanism for event-driven architectures.

### Development Principles and Patterns

- **Adheres to SOLID principles**: by encapsulating internal classes within modules, promoting extensibility without modification, abstracting with interfaces and abstract methods, using IOC etc. 
- **CQRS with MediatR**: I prepared for using the Command Query Responsibility Segregation pattern, leveraging MediatR for separating read and write operations, thereby simplifying the codebase and enhancing maintainability, but did not have time to complete the implementation.

### Asynchronous Programming

- **Async/Await**: Ensures responsive and scalable applications by implementing asynchronous programming patterns in controllers and services.

### API Enhancements

- **Refined API Parameters**: Transitions API parameters to JSON for increased clarity and consistency.
- **Dynamic Pricing Configuration**: Introduces a dynamic method for pricing configuration, with the potential for external API integration.

### Testing
- **Unit tests**: Added test project in each module and in the API folder. Added more than 10 unit tests to test the functionalities of booking in different days and times and also for email validations. Please see [BookingRequestTests.cs](https://github.com/hedin-it/code-test-YazanGhafir/blob/main/src/API/Tests/Requests/BookingRequestTests.cs).

### Documentation

- **Swagger**: Incorporates Swagger for API documentation, facilitating easier testing and interaction with the API endpoints.

### Final Thoughts
- I could add a Dockerfile and docker-compose file to deploy the application in a containerized environment. 
- I could also create a github action pipeline with build, test and release steps. 
- I could also make authentication and autorisation using Azure AD app registration utilizing the OAuth2.0 protocol, openid connect and claims in jwt tokens.  
- I could complete the implementation for the eventbus and CQRS pattern using MediatR.
- But all of that would be a bit overkill for a code test.


## Author

**Yazan Ghafir**
