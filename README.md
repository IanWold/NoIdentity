# NoIdentity

The ASP.NET Identity team hates me; this is the secret they don't want you to know!

This project demonstrates the basics of using semi-custom cookie authentication in ASP.NET Core MVC 5 to handle user authentication and state management. It has examples of the following common things:

* Logging in
* Logging out
* Authorizing user is logged in
* Authorizing roles
* Validating cookie state
* Updating `cshtml` based on user state

This is probably a lot less secure than Identity, but I don't like Identity and that's probably why you're here too.

## `Source/NoIdentity`

The authentication/login implementation is spread across these files:

* `Startup.cs` registers cookie authentication service
* `Controllers/AuthenticationController.cs` performs authentication
* `Views/Authentication/Login.cshtml` has the login UI

## Business Logic

I have some mockup business logic going. I've separated the Business and Data Access layers in a manner similar to what's done by the [CSLA Framework](https://github.com/MarimerLLC/csla). If you don't like the DAL abstraction it's pretty easy to use `Source/NoIdentity.DataAccess/Pretend Database` alone with the Business layer.

## Contributing

Feel free to add anything you think could help the project demonstrate cookie authentication. Here's a couple things I haven't gotten to yet that are on my mind:

* Security things (unsure about these sorts)
* An actual database

I'm trying to keep this repo as organized as possible (for referenceability), so when you submit a PR I might ask you to organize things a bit differently than what you have, for the sake of consistency (it's NBD).

Keep in mind: No Entity Framework, and no Identity :)