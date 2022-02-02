# CarRental.WebAPI [![.NET](https://github.com/pabferir/CarRental.WebAPI/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/pabferir/CarRental.WebAPI/actions/workflows/dotnet.yml) 
> An ASP.NET Core 6.0 Web API using a Car Rental data model for training in .NET fundamentals and design patterns.

## Data Model
<p align="center">
    <img src=".github\assets\CarRentalWebAPI-data-model.png" width="600">
</p>

## Solution Design
Design decisions are inspired by Clean Architecture guidelines from [ardalis/CleanArchitecture](https://github.com/ardalis/CleanArchitecture).

### Architecture
<p align="center">
    <img src=".github\assets\CarRentalWebAPI-project-architecture.png" width="600">
</p>

### Project Structure
<p align="center">
    <img src=".github\assets\CarRentalWebAPI-project-structure.png" width="600">
</p>

#### Core Project
The Core project is the center of the architecture design, as Web project and Infrastructure project components depend upon it. It hosts both the Domain and Business layers.

> Notice the Domain layer hosts `DbContext` instances as some Business components depend upon it.

#### Shared Kernel Project
The Shared Kernel project hosts all generic non-domain-specific components used across the solution. It could be extracted and referenced as a NuGet dependency.

#### Infrastucture Project
The Infrastructure project hosts the Data layer and contains most external dependencies. Its components implement interfaces defined in the Core project.

#### Web Project
The Web project is the entry point of the Web API. It consists of a Console Application and hosts the Application Layer. It also contains custom Configuration classes.

#### Test Projects
The Test projects mimic and point to the source projects. Currently, the only existing Test project is the Core.Test project.


## Design Patterns
This project implements the following design patterns:
- [Repository](https://martinfowler.com/eaaCatalog/repository.html)
- [Unit of Work](https://martinfowler.com/eaaCatalog/unitOfWork.html)
- [Service Layer](https://martinfowler.com/eaaCatalog/serviceLayer.html)

## Technology Stack
This project makes use of the following technologies:
- [.NET 6](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6)
- [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- [Entity Framework Core 6](https://docs.microsoft.com/ef/core/what-is-new/ef-core-6.0/whatsnew)
- [xUnit](https://xunit.net/)
- [Moq](https://github.com/moq/moq)
- [GitHub Actions](https://github.com/features/actions)

## License [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
This project is licensed under the Apache-2.0 License - see the [LICENSE](LICENSE) file for details.

Copyright © 2021 Pablo Ferrando Iranzo
