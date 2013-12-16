# Clean Coders Community EBI TestApp

## Introduction

This is a test project to research and try out the EBI (Entity, Boundary, Interactor) pattern in C#, inspired by Uncle Bob's Clean Architecture: http://blog.8thlight.com/uncle-bob/2012/08/13/the-clean-architecture.html
and the many great blog posts and videos surround clean code (http://cleancoders.com/).

The architecture shares many similarities with Ports and Adapters, Hexagonal and Onion architectures.
I've noticed that there is a lack of sample projects out there (especially C#), and since I am experimenting with the approach myself,
I thought it might be nice to just share it to the Clean Coders Community and beyond.

The project includes a Web-based and Desktop-based UI.

### Status
Work in progress: General layout and structure is in place, pseudo UsersController with corresponding skeleton use cases are available.
Account and Home controllers are standard ASP.NET controllers, and can be ignored.

### Contribute/Discuss
I would love to hear from you what you think about my implementation of the pattern, and would love to accept pull requests with improvements to the approach, or additional samples.

### Interest points
* How will the same core application and principles work for both a web (state-less request/response cycles) and desktop (realtime continuous) function
 * How to make data update realtime and still be performant due to serialization/deserialization at the boundaries?
* Usecase interfaces and request/respond models are placed together with the Usecase interactor class, within a single file. This seems acceptable.
* Unclear if e.g a 'Password confirmation did not match' should be an exception, or part of the use case response. For now exceptions are used for cases like UserNotFound -> 404
 * Perhaps the better approach would be when UI validates the input, don't touch the use case when a mismatch has occurred. Then the use cases can throw exceptions since the contract requires the password and confirmation to be equal?
* Unclear if a responder should have multiple methods to receive different responses, or consolidate them all into a single method, with different outcomes based on what is set in the response model. For now using the latter approach.
* Using injected Lazy use cases in the MVC Controllers as usually only one of the use cases will be used at a time
  * Also because of circular dependencies (the controller also takes on the role of Presenter currently), same goes for the WPF ViewModels

## Architecture
EBI: Entity, Boundary, Interactor

* Entity; Enterprise wide business rules
* Boundary; Interfaces that decouple annoying details like Data Access and UI, from Enterprise and Application wide business rules
* Interactor; Application specific use cases; interacting between the Boundaries and Entities

### Layout
* Core
 * Core.Application: Use cases and use case services
 * Core.Domain: Entities and domain services
* Infrastructure
 * Infrastructure.DAL: Data Access Layer Implementation
* UI
 * UI.Desktop: WPF MVVM Desktop app
 * UI.Web: ASP.NET MVC Web app

## Technologies
* C#
* ASP.NET MVC
* WPF MVVM
 * AutoMapper
 * Caliburn.Micro
 * Managed Extensibility Framework as DI container
* TBD
