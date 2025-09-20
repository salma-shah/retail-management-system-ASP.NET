# CozyComfort - an ASP.NET application, built with C#

## About
This project is a website for CozyComfort. It has three main roles: a seller, distributor, and manufacturer.
The seller is the one who deals with the customer and makes orders. If there is no stock for a specific order, a request will be sent to the distributor. 
The distributor acts as a middleman between the seller and manufacturer, while handling their own inventory. The distributor checks their own inventory first, and if it is not present, a request will be sent to the manufacturer.
The manufacturer checks stock based on requests. If unavailable, the manufacturer informs the distributor about production capacity and lead times for the product. 
Through this network of communication, stock management and purchase requests are made easy. 


## Built With
* C#
* ASP.NET web forms
* HTML
* CSS
* Microsoft SQL Server

This was coded on Visual Studio 2022. Technically, the code is reusable and modular, due to the service-oriented architecture. There are four main web services, categorized by closely related functions. 

The frontend of the website was built with HTML and CSS for the web forms. 

The backend was built with C# and the ASP.NET framework, and the application was integrated with a Microsoft SQL Server database named 'CozyComfort DB'. 

You will have to create a database, and enter the data source, username, and password you set in each of the web services, for the application to run without any errors. 

Similar functions were categorized in one web service; then, each service was assigned a reference to the frontend. An instance of the web service was invoked as needed through a SOAP client. This approach enables clean, easily maintainable code, where the frontend and backend are neatly separated.

## Lessons Learned

* This is the first service-oriented computing project I built, and it allowed me to learn about a new architecture and the differences between it and a monolithic architecture.
* My initial plan was not to have any CSS applied to the web forms, but I researched how to do it using a master page and went ahead, without any templates.
* Writing functions in web services, then running the service first before using it in the frontend, was a new topic for me, but I learnt that it made building and debugging the application much easier.
* I learnt the best way to keep the functions separated from the frontend, to maintain a scalable approach, where you don't have to debug any function errors from the frontend.
