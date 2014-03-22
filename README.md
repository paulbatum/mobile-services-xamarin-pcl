Mobile Services + Xamarin + PCL
===========================

This is a simple tutorial on how to use Mobile Services from a cross platform project using Xamarin, with a focus on using Portable Class Libraries (PCL) to make it easier to share code.

### Prerequisites

- XCode 4.5 and above (to build for iOS)
- [Xamarin tools](http://xamarin.com/download)
- A Windows Azure subscription ([free trial](http://www.windowsazure.com/en-us/pricing/free-trial))

### Step 1 - Create the mobile service and download the iOS project

I'll start by creating a new mobile service - lets call it `xamarinpcl`. [This tutorial](http://www.windowsazure.com/en-us/develop/mobile/tutorials/get-started-xamarin-ios/) covers the steps in detail.

![New Mobile Service](/images/new-mobile-service.png)

I follow the quickstart, clicking the button to create the TodoItem table and then I download the iOS project:

![Downloading quickstart project](/images/create-new-xamarin-app.png)

I'm going to unzip the downloaded project, and rename the folder and project to quickstartpcl-ios to avoid conflicts when I download the project for other platforms. Next I open Xamarin Studio (Visual Studio will work too!), create a new empty solution, add my ios project to it and run:

![Running iOS quickstart](/images/run-ios-quickstart.png)

### Step 2 - Download the Android project

Back on the quickstart page in the Windows Azure portal I'll switch the project type to Android, download, unzip, rename to quickstartpcl-android, add to the solution and run:

![Running android quickstart](/images/run-android-quickstart.png)

### Step 3 - Add the PCL project

In Xamarin Studio I add a new Portable Library project to the solution. I then go to options -> build -> general and select the platforms I want to target. For now I'm going to select .NET 4.5 and remove Silverlight and Windows Phone. I could keep Windows Phone but it makes things a bit more complicated because Windows Phone 8 is missing System.Net.Http so we'll come back to this a bit later.

![Configure PCL targets](/images/configure-pcl-project.png)

Next I'm going to add some references to this project. I want to reference the PCL library that is included in the [Azure Mobile Services component](http://components.xamarin.com/view/azure-mobile-services) that the downloaded projects already reference. I'll also need JSON.NET. I'm going to reference both of these from the `Components/azure-mobile-services-1.1.0/lib/iOS` folder (the versions in the Android folder are exactly the same so that would work too).

![Edit PCL references](/images/add-pcl-references.png)

Now I'm going to update my iOS and Android projects to reference this PCL project and make sure they build OK.

![Project references](/images/project-references.png)

### Step 3 - The Great Refactoring

Now the fun part! Lets pull some shared code into the PCL project. I'll want to take the Mobile Service client, the ToDoItem class, and the code that queries for todo items that are not marked as complete. I could certainly move more over but this should be enough to demonstrate the process. For the sake of simplicity I'll put it all in one file (no I would not do this for a real app):

<pre>
    public class Common
    {
        static Common()
        {
            InitializeClient();
        }

        public static MobileServiceClient MobileService;

        public static void InitializeClient(params DelegatingHandler[] handlers)
        {
            MobileService = new MobileServiceClient(
                @"https://xamarinpcl.azure-mobile.net/",
                @"your app key goes here",
                handlers
            );
        }

        public static IMobileServiceTableQuery<ToDoItem> GetIncompleteItems()
        {
            var toDoTable = MobileService.GetTable&lt;ToDoItem&gt;();
            return toDoTable.Where(item => item.Complete == false);
        }
    }

    public class ToDoItem
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }
    }
</pre>

I then have a bunch of changes to make to the Android and iOS projects to use the shared code. If you want to see the changes, just take a look at [this git commit](https://github.com/paulbatum/mobile-services-xamarin-pcl/commit/d58ec604baee1daaf4178662b9f39e652523d1cf).

Lets build and run both projects to make sure we haven't broken anything:

![Everything works](/images/everything-works-great.png)

Awesome!

### Step 4 - Adding Windows Phone 8

Its time to switch gears and flip over to Visual Studio so we can add projects for the Windows platforms. Lets do Windows Phone first. Like before I'll start by downloading the quickstart from the Azure portal and renaming it, but the project file requires a few tweaks before I can add it to the solution. Specifically, I open up the .csproj in a text editor and remove these lines:

	<Import Project="..\packages\Microsoft.Bcl.Build.1.0.8\tools\Microsoft.Bcl.Build.targets" />
  	<Import Project="$(SolutionDir)\.nuget\nuget.targets" />

The first of these lines will get added again when I update or reinstall the Bcl.Build Nuget package. The second line is no longer necessary because Visual Studio now restores NuGet packages naturally.

Now the project can be added to the solution without errors. Next I want to do a bit of cleanup on the NuGet packages used by this project so I open the NuGet package explorer, let it restore packages if required, and then remove the async package and update all the remaining packages. Lets make sure it builds and runs before we make any more changes:

![Add Windows Phone 8 project](/images/add-wp8-proj.png)



### Contact

If you have any questions or run into problems feel free to email me at pbatum@microsoft.com or catch me on twitter (@paulbatum).