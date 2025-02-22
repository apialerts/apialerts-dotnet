# API Alerts • Dotnet Client

NOTE: This repo has been superseded by [apialerts-csharp](https://github.com/apialerts/apialerts-csharp)

[GitHub](https://github.com/apialerts/apialerts-dotnet) • [Nuget](https://www.nuget.org/packages/ApiAlerts.Common)

### Overview

The ApiAlerts NuGet package simplifies the process of setting up and managing alerts within your API projects. It provides functionalities to activate the package with an API key and offers methods for publishing alerts asynchronously and synchronously.

### Installation
To install the ApiAlerts package, simply use NuGet Package Manager or the Package Manager Console:

````bash
PM> Install-Package ApiAlerts
````

### Initialize the client

````csharp
ApiAlerts.Activate(yourApiKey);
````

### Send Events

To publish alerts, you'll utilize the IAlertService interface.

#### Synchronous Method
You can publish an alert synchronously using the PublishAlert method, which takes an ApiAlert object and an optional API key.

````csharp
var alert = new ApiAlert { /* alert properties */ };
IAlertService alertService = new AlertService(); // Instantiate or inject IAlertService
alertService.PublishAlert(alert, optionalApiKey);
````

#### Asynchronous Method
Alternatively, you can publish an alert asynchronously using the PublishAlertAsync method, which also takes an ApiAlert object and an optional API key.

````csharp
var alert = new ApiAlert { /* alert properties */ };
IAlertService alertService = new AlertService(); // Instantiate or inject IAlertService
await alertService.PublishAlertAsync(alert, optionalApiKey);
````
