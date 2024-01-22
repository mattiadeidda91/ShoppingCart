## Name
ShoppingCart

## Scope
A Web Api simulation of a shopping cart to test and use various systems such as:
- Dapper for DB access
- Serilog for logging
- Hangfire for scheduled job
- XUnit Test

## Dependencies Projects
ShoppingCart.Abstractions<br>
ShoppingCart.Sql -> ShoppingCart.Abstractions<br>
ShoppingCart.Dependencies -> ShoppingCart.Sql<br>
ShoppingCart.Api -> ShoppingCart.Dependencies<br>

## Usage
1. Clone repository github
2. Run SQL script from script/SqlServer folder in versioning order to create the database and Sql Server tables<br>
3. Start ShoppingCart.Api project<br>
4. Serilog will automatically create the Logs table on the database<br>
5. The DB connection and query operations are manage to Dapper<br>
6. A HangFire scheduled job will be automatically created and started when the application starts and will run every 10 minutes to check all carts<br>

## License
2024 - Copyright (c) All rights reserved.