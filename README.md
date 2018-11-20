# AdaptiveClient.EntityFramework.Zamagon
Demo application for AdaptiveClient.EntityFrameworkCore

To use the full functionality of this demo you need access to both Microsoft SQL Server and My SQL.  However you can still use the demo if you have only only one of those.  
Go to the end of this document for instructions on how to run this demo.


![WPF UI](https://raw.githubusercontent.com/leaderanalytics/AdaptiveClient.EntityFramework.Zamagon/master/WPF_UI.png)
![WEB UI](https://raw.githubusercontent.com/leaderanalytics/AdaptiveClient.EntityFramework.Zamagon/master/Web_UI.png)

## Understand the solution files
There are three solution files included with the demo.
Each solution file contains five projects that are common to all solutions:

*Zamagon.Domain* - Low level business logic and interfaces
*Zamagon.Model* - Data Transfer Objects
*Zamagon.Services.BackOffice*  - Platform and Transport specific services that support the BackOffice API.
*Zamagon.Services.StoreFront* - Platform and Transport specific services that support the StoreFront API.
*Zamagon.Services.Common* - Platform and Transport specific services that support both APIs.

Each solution contains additonal projects as specified:
*Zamagon* - Utility and Test.
*Zamagon.API* - WebAPI host.
*Zamagon.UI* - Web UI and WPF UI.


## Modify the connection strings in EndPoints.json
Open EndPoints.json in the EndPoints directory. Set the IsActive flag to false for *_SQLServer or *_MySQL if you do not run one of those servers.  
Modify the ConnectionString as necessary.  Be sure to remove the curly braces in the MySQL Uid and Pwd.


## Build and run the API server
Open the Zamagon.API solution.
Make sure Zamagon.API is the startup project.
Start debugging.  The `ZamagonBackOffice` and `ZamagonStoreFront` databases will be created.
Open the OrdersController and place a break point on the first line of code in the GetOrders method.
 

## Build and run the Web UI
Open the Zamagon.UI solution.
Make sure Zamagon.Web is the startup project.
Open the Zamagon.Services.StoreFront project. 
Open each of the following folders and note the OrdersService.cs file:  MSSQL, MySQL, WebAPI.
Open all three instances of OrdersService.cs.  Place a breakpoint on the closing curly brace of the constructor.  Note that each service implements `IOrdersService`.
Start debugging. 
Select MSSQL or MySQL from the Data Source dropdown in the upper right.
Click the Orders link on the black menu bar. Note that the breakpoint in the service corresponding to your Data Source selection is hit.
Click Continue or press F5 in Visual Studio.







