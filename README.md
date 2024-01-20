## Name
ShoppingCart

## Scope
A Web Api simulation of a shopping cart to test and use various systems such as:
- Dapper for DB access
- Serilog for logging
- Hangfire for scheduled job

## Dependencies Projects
ShoppingCart.Abstractions
ShoppingCart.Sql -> ShoppingCart.Abstractions
ShoppingCart.Dependencies -> ShoppingCart.Sql
ShoppingCart.Api -> ShoppingCart.Dependencies 

## Usage
Clone repository github
Run SQL script from script/SqlServer folder in versioning order to create the database and Sql Server tables
Start ShoppingCart.Api project
Serilog will automatically create the Logs table on the database
The DB connection and query operations are manage to Dapper
A HangFire scheduled job will be automatically created and started when the application starts and will run every 10 minutes to check all carts

## License
2024 - Copyright (c) All rights reserved.