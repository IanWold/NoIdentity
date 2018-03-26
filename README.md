# NoIdentity

The ASP.NET Identity team hates me; this is the secret they don't want you to know!

This project demonstrates the basics of using semi-custom cookie authentication in ASP.NET Core MVC 5 to handle user authentication and state management. It's not totally complete at the moment, but the login functionality is working.

## `Source/NoIdentity`

There are two main places you need to look:

* `Startup.cs` registers cookie authentication service
* `Controllers/AuthenticationController.cs` performs authentication

## Business Logic

I have some mockup business logic going. I've separated the Business and Data Access layers in a manner similar to what's done by the [CSLA Framework](https://github.com/MarimerLLC/csla). If you don't like the DAL abstraction it's pretty easy to use `Source/NoIdentity.DataAccess/Pretend Database` alone with the Business layer.

## Contributing

I'm going to go it alone until I get a fully working project. From there any contributions (particularly with future updates to ASP.NET) will be welcome! I'll have this README changed when that happens. Stay tuned!