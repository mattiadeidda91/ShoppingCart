## Name
ShoppingCart

## Scope
A Web Api simulation of a shopping cart to test and use various systems such as:
- Dapper for DB access
- Serilog for logging
- Hangfire for scheduled job

## Dependencies Projects
ShoppingCart.Abstractions<br>
ShoppingCart.Sql -> ShoppingCart.Abstractions<br>
ShoppingCart.Dependencies -> ShoppingCart.Sql<br>
ShoppingCart.Api -> ShoppingCart.Dependencies<br>

## Usage
Clone repository github
Run SQL script from script/SqlServer folder in versioning order to create the database and Sql Server tables<br>
Start ShoppingCart.Api project<br>
Serilog will automatically create the Logs table on the database<br>
The DB connection and query operations are manage to Dapper<br>
A HangFire scheduled job will be automatically created and started when the application starts and will run every 10 minutes to check all carts<br>

## License
2024 - Copyright (c) All rights reserved.