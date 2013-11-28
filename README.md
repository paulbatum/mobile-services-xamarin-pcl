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

![Downloading quickstart project](/images/run-ios-quickstart.png)