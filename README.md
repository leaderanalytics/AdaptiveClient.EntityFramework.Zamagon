# AdaptiveClient.EntityFramework.Zamagon
Demo application for AdaptiveClient.EntityFrameworkCore

To use the full functionality of this demo you need access to both Microsoft SQL Server and MySQL.  If you have only one of those you can still use the demo for the database you have.  

Scroll to the end of this document for instructions on how to run this demo.


![WPF UI](https://raw.githubusercontent.com/leaderanalytics/AdaptiveClient.EntityFramework.Zamagon/master/WPF_UI.png)
![WEB UI](https://raw.githubusercontent.com/leaderanalytics/AdaptiveClient.EntityFramework.Zamagon/master/Web_UI.png)


## Basic concepts

Zamagon illustrates how to organize services into APIs.  These APIs can be used as a single unit, greatly simplifying how services are injected and used by the objects that depend on them.

Services are classes that contain methods that are often common to a specific entity.  These are commonly CRUD operations but just as often contain methods for reporting, data warehousing, etc.  Services may also be organized by task such as shipping, order processing, etc.

APIs are collections of services.  APIs may be common to an entire organization, an application within an organization, a department, or a specific function such as banking or purchasing.

The Zamagon demo illustrates four services:  `OrdersService`, `ProductsService`, `EmployeesService`, and `TimeCardsSerice`.

Services are organized into two APIs:  `BackOffice` (Employee and TimeCards services) and `StoreFront` (Orders and Products services).

The demo illustrates how each service can be written for a specific database (MSSQL/MySQL) or transport (LAN/HTTP).  At runtime, the user can select a specific database or transport and AdaptiveClient will resolve the components that are necessary to perform the requested call.


## Understanding the code

#### Zamagon.Model project

Data Transfer Object (DTO) classes.  Note that the classes exactly match the structure of the database table they represent and contain no code.

#### Zamagon.Domain project

Application interfaces and utility classes.  

#### Zamagon.Services.Common project

Classes that common to both `BackOffice` and `StoreFront` services.


#### Zamagon.Services.BackOffice project

`BOServiceManifest` class is a manifest (i.e. list) of services that comprise the BackOffice API.

`Db` is intended to be an in-memory representation of the BackOffice database.

Services, with platform-specific implementations.

`BODatabaseInitializer` seeds the database after creation or a migration.

`BaseService` contains a property that references the parent ServiceManifest.  From this property the service can access any other service listed on the manifest.


#### Zamagon.Services.StoreFront project

Same as BackOffice project.

#### Zamagon.Web project

A simple website to illustrate how calls are made to the API using AdapitveClient.

`BasePageModel` in the Web project contains a property of type `IAdaptiveClient<TManifest>`.  Because of this, all derived pages have immediate, statically typed access to every service exposed by the API the page built for:

    await ServiceClient.TryAsync(x => x.EmployeesService.GetEmployees());

#### Zamagon.WPF project


Conceptually the same as the web project.  Note how `BasePageModel<TModel, TManifest>` allows common view properties to be defined on the base class.

## How to run the Zamagon Demo

There are three solution files included with the demo.
Each solution file contains five projects that are common to all solutions:

*Zamagon.Domain* - Low level business logic and interfaces.  
*Zamagon.Model* - Data Transfer Objects.  
*Zamagon.Services.BackOffice*  - Platform and transport specific services that support the BackOffice API.  
*Zamagon.Services.StoreFront* - Platform and transport specific services that support the StoreFront API.  
*Zamagon.Services.Common* - Platform and transport specific services that support both APIs.  

Each solution contains additional projects as specified:

*Zamagon* - Utility and Test.  
*Zamagon.API* - WebAPI host.  
*Zamagon.UI* - Web UI and WPF UI.  


### Modify the connection strings in appsettings.json
Open appsettings.json in the EndPoints directory.  
Set the IsActive flag to false for *_SQLServer or *_MySQL if you do not run one of those servers.  
Modify the ConnectionString as necessary.  Be sure to remove the curly braces in the MySQL Uid and Pwd.  

### Build and run the API server
Open the Zamagon.API solution.  
Make sure Zamagon.API is the startup project.  
Open the OrdersController and place a break point on the first line of code in the GetOrders method.  
Open the Zamagon.Services.StoreFront project.   
Open the OrdersService.cs files in the MSSQL directory and MySQL directory.  
Place a breakpoint on the first line of code in the GetOrders method.  
Start debugging.  The `ZamagonBackOffice` and `ZamagonStoreFront` databases will be created.  

### Build and run the Web UI
Open the Zamagon.UI solution.  
Make sure Zamagon.Web is the startup project.  
Open the Zamagon.Services.StoreFront project.   
Open the OrdersService.cs file in the MSSQL, MySQL, WebAPI folders.  
Note that each service implements `IOrdersService`.  
Place a breakpoint on the first line of code in the GetOrders method in each file.  
Start debugging.   
Select MSSQL or MySQL from the Data Source dropdown in the upper right.  
Click the Orders link on the black menu bar. Note that the breakpoint in the service corresponding to your Data Source selection is hit.  
Click Continue or press F5 in Visual Studio.  
Select WebAPI from the Data Source dropdown in the upper right.  
GetOrders on the WebAPI implementation of `IOrdersService` is invoked.  This triggers a call to the API server which makes another call to GetOrders.  The implementation of IOrdersService that is injected into the controller gets the requested data from disk (MSSQL or MySQL) and returns it.


### Build and run the WPF UI
Open the Zamagon.UI solution.  
Make sure Zamagon.WPF is the startup project.  
Start debugging.   
The Home tab describes the functionality demonstrated by the application.

