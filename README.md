# SpatialiteForms
A [nuget package](https://www.nuget.org/packages/Plugin.SpatialiteForms/) that enables spatialite for Xamarin Forms. As an example this allows to do offline reverse geocoding on mobile devices (this barely scratches the surface of Spatialite, but it's a very common usecase for mobile dev)

It's entirely based on:

 - [sqlite-net-base](https://github.com/praeclarum/sqlite-net) and
 - [Spatialite](https://www.gaia-gis.it/fossil/libspatialite/index) and 
 - [SQLitePCL.raw](https://github.com/ericsink/SQLitePCL.raw) for SQLitePCLRaw.bundle_e_sqlite3 - which gives better iOS supprort for sqlite extensions

**If you find this library useful, please support the above two projects.**

What this means is that it exposes a SQLiteConnection that supports spatial SQL capabilities.

To enable this functionality native libraries/framework is required (apart from the nuget package itself)  :

 1. Get **Android** libraries from  [here](https://github.com/breekmd/SpatialiteForms/tree/master/NativeLibraries/android) and add under libs folder, with build action as "AndroidNativeLibrary". Folder structure under Android project:
 ```
   .
  ├── Assets
  ├── ...
  ├── libs                    
  │   ├── armeabi-v7a       
  │       ├── libspatialite.so   
  │   ├── x86
  │       ├── libspatialite.so   
  └── ...
  ```
 2. Get **iOS** framework from [here](https://github.com/breekmd/SpatialiteForms/tree/master/NativeLibraries/ios/iOSSpatialite.framework) and add as Native Reference to iOS project


<h2>Targeted architecture (these architectures are compatible with most devices)</h2>

| Platform | Arch | Comments |
| ------------- | ------------- |------------- |
| Android | x86 | will also run on x86_64 |
| Android | armeabi-v7a| will also run on arm64-v8a |
| iOS | x86_64 | for emulators |
| iOS | arm64 | for devices starting with iPhone 5s |

<h2>Supported Spatialite functionality</h2>

|Functionality| Android | iOS |  
|--|--|--|
|  HasEpsg| <ul><li> - [x] </li></ul> | <ul><li> - [x] </li></ul> |
|  HasFreeXL| <ul><li> - [ ] </li></ul>| <ul><li> - [ ] </li></ul> |
|  HasGeoCallbacks| <ul><li> - [ ] </li></ul>  | <ul><li> - [ ] </li></ul> |
|  HasGeos| <ul><li> - [x] </li></ul> |  <ul><li> - [x] </li></ul>|
|  HasGeosAdvanced| <ul><li> - [x] </li></ul> |<ul><li> - [x] </li></ul> |
|  HasGeosTrunk| <ul><li> - [ ] </li></ul> | <ul><li> - [ ] </li></ul> |
|  HasIconv| <ul><li> - [x] </li></ul> | <ul><li> - [x] </li></ul>|
|  HasLibxml2| <ul><li> - [x] </li></ul> | <ul><li> - [x] </li></ul> |
|  HasMathSQL|  <ul><li> - [x] </li></ul> | <ul><li> - [x] </li></ul> |
|  HasProj| <ul><li> - [x] </li></ul> |  <ul><li> - [x] </li></ul>|


<h2>Spatialite component versions </h2>

|Component| Android version | iOS version |  
|--|--|--|
|  Geos | 3.5.0-CAPI-1.9.0 r4084 | 3.5.0-CAPI-1.9.0 r4084 |
|  Libxml2 | 2.9.1 | 2.9.4 |
|  Proj4 | Rel. 4.9.2, 08 September 2015 | Rel. 4.9.2, 08 September 2015 |
|  Spatialite | 4.4.0-topo-1 | 4.4.0-RC1 |

There's small version mismatches which shouldn't cause problems, but please raise an issue if anything occurs.

Current versioning/capabilities information can be accessed via **SpatialiteInfo** property, available in the **SpatialiteConnection** class.

SpatialiteForms also allows to use "pre-packaged" database files using **Assets** folder in Android and **Resources** folder for iOS. 

<h2>Usage</h2>

Example using [countryData.db](https://github.com/breekmd/SpatialiteForms/tree/master/ExampleSpatialDb) on how to prepackage a database with geometry data within the xamarin app and to get a Spatialite-capable SQLiteConnection.

Database countryData was created using Spatialite-GUI (very powerful tool) through which shape data was imported (shape data originated from [here](https://www.naturalearthdata.com/downloads/10m-cultural-vectors/))

 1. Add countryData.db under **Assets** folder on Android (build action - Android asset) and **Resources** folder on iOS (build action - Bundle resource)
 
 2. Define a Region class
 
 ```csharp
public class Region {  
   public string Name { get; set; }  
}
  ```
  
 3.  Get the Spatialite capable SQLiteConnection
```csharp
var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "countryData.db");
SpatialiteConnection spatialite = CrossSpatialiteForms.Current.GetSpatialiteConnection(dbPath, "countryData.db", true);

var info = spatialite.SpatialiteInfo;
var region = spatialite.SQLiteConnection.Query<Region>("select name from region where within(Makepoint(-100.7594387, 46.77519), geometry);").FirstOrDefault();
```
the result region should have the name of North Dakota

Again this is only a small thing of what spatialite is capable of, please check official Spatialite documentation for more details.

<h2>Nuget package</h2>

https://www.nuget.org/packages/Plugin.SpatialiteForms/

<h2>License</h2>

 This project is licensed under [GPL3](https://github.com/breekmd/SpatialiteForms/blob/master/LICENSE/)
 
Spatialite libraries have been compiled from original source code and no changes were made to it.
 
If you believe that I have infringed your copyright in any way please let me know and I will remove any code that is your copyright ASAP.
