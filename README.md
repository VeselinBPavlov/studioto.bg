# Studioto.bg

Studioto.bg is a sample application for appointments built using ASP.NET Core and Entity Framework Core. It follow the CQRS + MediatR pattern and clean architecture principles.

## There are four base layers in the project.
1. Domain Layer - contains all entities, enums, exceptions, types and logic specific to the domain. The Entity Framework related classes are abstract, and should be considered in the same light as .NET Core. For testing, use an InMemory provider.
2. Application Layer - contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers.
3. Persistence Layer - contains database context, all configurations, migrations and data seed. It depends only on the application layer.
4. Presentation Layer - contains all presentation logic. For Admin panel is a Single Page Application working with ASP.NET WebAPI and jQuery. There is scaffolded indentity works with Razor Pages. The rest of the website is classic multipage MVC application. Presentation layer depends only on application layer.

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2019](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
     ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ``` 
  4. Once the front end has started, within the `Northwind.WebUI` directory, launch the back end by running:
     ```
	   dotnet run
	   ```
  5. Launch in your browser.
  6. First registered user will be with role "Administrator". Any other user will be with role "User".

## Technologies
* .NET Core 2.2
* ASP.NET Core 2.2
* ASP.NET Core WebAPI 2.2
* Entity Framework Core 2.2
* xUnit, MyTested.AspNetCore.Mvc
* CQRS, MediatR, Automapper, SendGrid, View Components, jQuery, Bootstrap

## License

This project is licensed under the MIT License - see the [LICENSE.md]
