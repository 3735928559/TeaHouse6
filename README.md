This is a simple ordering system targeted to setup within a local tea house. Client headheld devices could be used as customer interface to interact with the system, while shopkeeper could also make use of administrator login to perform some system admin tasks.

This application is developed using visual studio 2019 hence it is highly recommended to rebuild using the same version to prevent unexpected behavior.

There are default users created for testing purpose

User: Administrator 
Password: P@ssw0rd

User: Table 1
Password: P@ssw0rd

links to access the api
https://siteurl/api/apifood
https://siteurl/api/apichoice
https://siteurl/api/apiorder


Directory Structure should as below.

+---.vs
|   \---TeaHouse
|       +---config
|       \---v16
|           \---Server
|               \---sqlite3
+---Api
+---App_Data
+---App_Start
+---bin
|   \---roslyn
+---Content
+---Controllers
+---DAL
+---Migrations
+---Models
+---obj
|   \---Debug
|       +---edmxResourcesToEmbed
|       \---TempPE
+---packages
|   +---Antlr.3.5.0.2
|   |   \---lib
|   +---bootstrap.4.4.1
|   |   +---content
|   |   |   +---Content
|   |   |   \---Scripts
|   |   \---contentFiles
|   |       +---Content
|   |       \---Scripts
|   +---EntityFramework.6.4.0
|   |   +---build
|   |   |   \---netcoreapp3.0
|   |   +---buildTransitive
|   |   |   \---netcoreapp3.0
|   |   +---content
|   |   |   \---net40
|   |   +---lib
|   |   |   +---net40
|   |   |   +---net45
|   |   |   \---netstandard2.1
|   |   \---tools
|   |       +---net40
|   |       |   +---any
|   |       |   \---win-x86
|   |       +---net45
|   |       |   +---any
|   |       |   \---win-x86
|   |       \---netcoreapp3.0
|   |           \---any
|   +---jQuery.3.3.1
|   |   +---Content
|   |   |   \---Scripts
|   |   \---Tools
|   +---jQuery.Validation.1.17.0
|   |   \---Content
|   |       \---Scripts
|   +---Microsoft.AspNet.Identity.Core.2.2.2
|   |   \---lib
|   |       \---net45
|   +---Microsoft.AspNet.Identity.EntityFramework.2.2.2
|   |   \---lib
|   |       \---net45
|   +---Microsoft.AspNet.Identity.Owin.2.2.2
|   |   \---lib
|   |       \---net45
|   +---Microsoft.AspNet.Mvc.5.2.7
|   |   +---Content
|   |   \---lib
|   |       \---net45
|   +---Microsoft.AspNet.Razor.3.2.7
|   |   \---lib
|   |       \---net45
|   +---Microsoft.AspNet.Web.Optimization.1.1.3
|   |   \---lib
|   |       \---net40
|   +---Microsoft.AspNet.WebApi.5.2.7
|   +---Microsoft.AspNet.WebApi.Client.5.2.7
|   |   \---lib
|   |       +---net45
|   |       +---netstandard2.0
|   |       \---portable-wp8%2Bnetcore45%2Bnet45%2Bwp81%2Bwpa81
|   +---Microsoft.AspNet.WebApi.Core.5.2.7
|   |   +---Content
|   |   \---lib
|   |       \---net45
|   +---Microsoft.AspNet.WebApi.WebHost.5.2.7
|   |   \---lib
|   |       \---net45
|   +---Microsoft.AspNet.WebPages.3.2.7
|   |   +---Content
|   |   \---lib
|   |       \---net45
|   +---Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.0
|   |   +---build
|   |   |   +---net45
|   |   |   \---net46
|   |   +---content
|   |   |   +---net45
|   |   |   \---net46
|   |   +---lib
|   |   |   \---net45
|   |   \---tools
|   |       +---net45
|   |       +---Roslyn45
|   |       \---RoslynLatest
|   +---Microsoft.jQuery.Unobtrusive.Validation.3.2.11
|   |   \---Content
|   |       \---Scripts
|   +---Microsoft.Owin.4.0.0
|   |   \---lib
|   |       \---net451
|   +---Microsoft.Owin.Host.SystemWeb.4.0.0
|   |   \---lib
|   |       \---net451
|   +---Microsoft.Owin.Security.4.0.0
|   |   \---lib
|   |       \---net451
|   +---Microsoft.Owin.Security.Cookies.4.0.0
|   |   \---lib
|   |       \---net451
|   +---Microsoft.Owin.Security.Facebook.4.0.0
|   |   \---lib
|   |       \---net451
|   +---Microsoft.Owin.Security.Google.4.0.0
|   |   \---lib
|   |       \---net451
|   +---Microsoft.Owin.Security.MicrosoftAccount.4.0.0
|   |   \---lib
|   |       \---net451
|   +---Microsoft.Owin.Security.OAuth.4.0.0
|   |   \---lib
|   |       \---net451
|   +---Microsoft.Owin.Security.Twitter.4.0.0
|   |   \---lib
|   |       \---net451
|   +---Microsoft.Web.Infrastructure.1.0.0.0
|   |   \---lib
|   |       \---net40
|   +---Modernizr.2.8.3
|   |   +---Content
|   |   |   \---Scripts
|   |   \---Tools
|   +---Newtonsoft.Json.11.0.1
|   |   \---lib
|   |       +---net20
|   |       +---net35
|   |       +---net40
|   |       +---net45
|   |       +---netstandard1.0
|   |       +---netstandard1.3
|   |       +---netstandard2.0
|   |       +---portable-net40%2Bsl5%2Bwin8%2Bwp8%2Bwpa81
|   |       \---portable-net45%2Bwin8%2Bwp8%2Bwpa81
|   +---Owin.1.0
|   |   \---lib
|   |       \---net40
|   +---PagedList.1.17.0.0
|   |   \---lib
|   |       \---net40
|   +---PagedList.Mvc.4.5.0.0
|   |   +---Content
|   |   |   \---Content
|   |   \---lib
|   |       \---net40
|   +---popper.js.1.16.0
|   |   \---content
|   |       \---Scripts
|   |           +---esm
|   |           +---src
|   |           |   +---methods
|   |           |   +---modifiers
|   |           |   \---utils
|   |           \---umd
|   \---WebGrease.1.6.0
|       +---lib
|       \---tools
+---Properties
+---Scripts
|   +---esm
|   +---src
|   |   +---methods
|   |   +---modifiers
|   |   \---utils
|   \---umd
\---Views
    +---Account
    +---AdminOrder
    +---Choice
    +---Food
    +---FoodType
    +---Home
    +---Manage
    +---OrderModel
    \---Shared
