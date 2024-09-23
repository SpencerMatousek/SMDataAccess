# SMDataAccess
SMDataAccess is a lightweight, and flexible Data Access Layer (DAL) for .NET applications. Designed with simplicity and effectiveness in mind, it leverages best practices in repository patterns, asynchronous programming, and error handling to facilitate seamless interaction with SQL databases using Dapper.

## Features
- Logging is accomplished by wrapping the DbCommand and DbConnection classes. Within the appsettings you can easily toggle logging full SQL queries and parameters
- A query builder simplifies filtering and ordering datasets and makes pagination simple.
- Utilized dependency injection and provides a basic interface for CRUD options
- Robust enough to be easily expanded to fit specific needs
