# BankingAppMVC
This was my first time implementing a C# web application. I used the Asp.NET Core 2.0 MVC framework to implement this solution.

# Data Store
For local data store I used the [HttpContext.Session](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext.session?view=aspnetcore-2.1) as a means to store data and share between application controllers. I was able to serialize complex objects into JSON and deserialize when updating data models. I used serializer methods found [here](https://github.com/NeelBhatt/DotNetCoreSessionSample/blob/master/NeelSessionExample/Utility/SessionExtension.cs) which could convert complex object to JSON and deserialize from JSON back to a usable object. This proved helpful for adding new Transactions to the RegisteredUser class as the user completed withdraw and deposit actions.

# Front End
As this was my first time implementing a .NET Core web application, I used [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-2.1&tabs=visual-studio) as it was the default setting creating a new .NET Core MVC application in Visual Studio Community (Mac). Razor Pages allow for simple data binding by passing a Model to the view and updating Model attributes through different actions or web forms.

# Authentication
For authentication, I implemented [Cookie Authentication](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-2.1&tabs=aspnetcore2x) with the default "CookieAuthenticationDefaults.AuthenticationScheme" as the authentication scheme. I was able to use the [SignInAsync](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.authenticationhttpcontextextensions.signinasync?view=aspnetcore-2.1) (an extension of [Microsoft.AspNetCore.Authentication](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication?view=aspnetcore-2.1)) to perform a simple authentication operation and create an authorized user login. Then I added the Authorize tag to my entire main controller to ensure all routes required an authorized user.
```language=C#
    [Authorize]
    public class HomeController : Controller
    {
      ...
    }
```
Additionally, I was able to capitalize on the .NET Core HttpContext User Identity by creating a dynamic logout button that only shows when the user is logged in:
```language=C#
    @if (@User.Identity.IsAuthenticated)
    {
       <input type="button" class="btn btn-danger" value="Logout"
              onclick="location.href='@Url.Action("Logout", "Security")'" />
    }
```
# Difficulties
First and foremost, the largest difficulty for this project was the learning curve involved with implementing a C# application for the first time. 

# Things I Learned
