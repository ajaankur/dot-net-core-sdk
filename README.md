# LoginRadius .NET Core SDK


![Home Image](https://d2lvlj7xfpldmj.cloudfront.net/support/github/banner-1544x500.png)

## Introduction ##

LoginRadius ASP.NET Core Customer Registration wrapper provides access to LoginRadius Identity Management Platform API.

LoginRadius is an Identity Management Platform that simplifies user registration while securing data. LoginRadius Platform simplifies and secures your user registration process, increases conversion with Social Login that combines 30 major social platforms, and offers a full solution with Traditional User Registration. You can gather a wealth of user profile data from Social Login or Traditional User Registration. 

LoginRadius centralizes it all in one place, making it easy to manage and access. Easily integrate LoginRadius with all of your third-party applications, like MailChimp, Google Analytics, Livefyre and many more, making it easy to utilize the data you are capturing.

LoginRadius helps businesses boost user engagement on their web/mobile platform, manage online identities, utilize social media for marketing, capture accurate consumer data, and get unique social insight into their customer base.

Please visit [here](http://www.loginradius.com/) for more information.


## Prerequisites

* .NET Core


## Contents ##

* [Demo Application](https://github.com/LoginRadius/dot-net-core-sdk/tree/master/Demo/src/Demo): It contains a basic demo of the SDK
library
* [SDK](https://github.com/LoginRadius/dot-net-core-sdk/tree/master/LoginradiusCoreSdk/src/src/LoginradiusCoreSdk): It contains all the sourced compiled SDK.

## Demo Application

In order to configure the LoginRadius Demo Application and SDK, set your LoginRadius API Key and Secret to **appsetting.json**.
```
  "loginradius": {
    "appsecret": "", //put your appsecret here
    "appkey": "", //put your appkey here
    "appname": "" //put your appname here
  }
```

#### Restore NuGet Packages

NuGet is required to acquire any .NET assembly dependency, You need to restore/download the rest of the demo dependencies via NuGet, as they are not yet part of the Demo. These NuGet dependencies contain facades (type forwarders) that point to mscorlib and Libraries.

Right click on Solution in Solution Explorer and click on "Restore Nuget Packages"

## Documentation

* [Getting Started](https://docs.loginradius.com/api/v1/sdk-libraries/aspnetcore) - Everything you need to begin using this SDK.


General documentation regarding the DotNet REST API and related flows can be found on the [LoginRadius API Documentations](http://apidocs.loginradius.com/) site. 
