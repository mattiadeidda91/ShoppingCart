## Name
ShoppingCart

## Scope
A Web Api simulation of a shopping cart to test and use various systems such as:
- Dapper for DB access
- Serilog for logging
- Hangfire for scheduled job to send email
- XUnit Test

## Dependencies Projects
ShoppingCart.Abstractions<br>
ShoppingCart.Sql -> ShoppingCart.Abstractions<br>
ShoppingCart.Dependencies -> ShoppingCart.Sql<br>
ShoppingCart.Api -> ShoppingCart.Dependencies<br>

## Usage
1. Clone repository github
2. In appsettings add your ConnectionString and EmailOptions configurations
3. Run SQL script from script/SqlServer folder in versioning order to create the database and Sql Server tables<br>
4. Start ShoppingCart.Api project<br>
5. Serilog will automatically create the Logs table on the database<br>
6. The DB connection and query operations are manage to Dapper<br>
7. A HangFire scheduled job will be automatically created and started when the application starts and will run hourly to send email with list of Users<br>
8. Open /jobs page in your browser to manage the Hangfire Dashboard

## License
2024 - Copyright (c) All rights reserved.