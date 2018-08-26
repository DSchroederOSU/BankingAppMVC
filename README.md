# BankingAppMVC
This was my first time implementing a C# web application. I used the Asp.NET Core 2.0 MVC framework to implement this solution.
# Assignment Criteria
You have been tasked with writing the world’s greatest banking ledger. Please code a solution that can perform the following workflows through a console application (accessed via the command line):

- Create a new account
- Login
- Record a deposit
- Record a withdrawal
- Check balance
- See transaction history
- Log out

For additional credit, you may implement this through a web page. They don’t have to run at the same time, but if you would like to do that, feel free.

C# is preferred but not required. Use whatever frameworks/libraries you wish, and just make sure they are included or available via via NuGet/npm/etc. Please use a temporary memory store (local cache) instead of creating an actual database, and don't spend much time on the UI (unless you love doing that).

# Data Store
For local data store I used the [HttpContext.Session](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext.session?view=aspnetcore-2.1) as a means to store data and share between application controllers. I was able to serialize complex objects into JSON and deserialize when updating data models. I used serializer methods found [here](https://github.com/NeelBhatt/DotNetCoreSessionSample/blob/master/NeelSessionExample/Utility/SessionExtension.cs) which could convert complex object to JSON and deserialize from JSON back to a usable object. This proved helpful for adding new Transactions to the RegisteredUser class as the user completed withdraw and deposit actions.

# Field Validation
I implemented a very simple data validation scheme to verify that the user submits valid deposit and withdraw values (i.e. not negative or values of 0). I also created a banner message that gets injected on validation error and tells the user what was wrong with the submission. This was done by using the [TempData](Controller.TempData Property) property in the controller to inject a string of HTML code that was rendered in the Razor View Page with:
```
 @Html.Raw(TempData["errormsg"])
```
Additionally, there was a JQuery script to fade the error message out after 2 seconds so the error banner did not persist:
```language=JavaScript
<script language="javascript" type="text/javascript">
  setTimeout(function() {
      $('#alertMessage').fadeOut('fast');
  }, 2000);
</script>
```

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
First and foremost, the largest difficulty for this project was overcoming the learning curve involved with implementing a C# application for the first time. I needed to read through documentation and code examples to understand how .NET MVC worked and how to correctly implement all of the different features of the application.

Additionally, finding a way to persist data throughout the application without a db data store proved difficult until I found out how to use [Session state](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-2.1) to store user data. While this might not have been the ideal solution, it worked fine for the purpose of this application.

# Things I Learned
Everything.
