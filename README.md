## Weavy Spaces Mobile
Weavy Spaces Mobile is a Xamarin Forms hybrid app targeting both the Android and iOS platforms. This repository was created as a boiler plate to allow developers of the Weavy platform to easily get up and running with mobile development.

### What can it be used for?
Build apps for Android and iOS that users of the Weavy platform can use to access the content and get notified by push notifications.

### How to use it?
Fork the repository and customize it to your needs. Follow the guide below how to customize the various parts. 

### What are the components?

**Weavy** - .NET Standard shared code library
**Weavy.Android** - Android implementation of the app
**Weavy.iOS** - iOS implementation of the app



### A short description of the app...
The app basically consists of only two **Views**. The **Select site** page  where the user enters the url to the Weavy site and the **Main** page which contains the hybrid webview displaying the web site.

All communication between the web page and the native app is handled through a javascript bridge executing javascript on the web page and receiving messages from the web page.

Push notifications are sent to the app from the Weavy web application through  Azure Notification Hubs. Please take a look at the **Push Notifications** section below for more info on how to set up this.






