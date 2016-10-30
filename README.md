# localization-tool
A Language Localization Tool For Unity3D

This is a string table to store localized text for your games into a Language Asset File. 

![Localization Icon](http://i.imgur.com/CxUqIt3.png)

## How To Get Started
- (optional) Fork this repository into your own BitBucket account
- Clone this repository onto a location on your computer.
- Open the project in Unity.
- Import the folder into your Unity Project

## Native Language - Player Settings
- Each game selects a native language in the player settings
- API
  - GetLanguage() returns SystemLanguage
  - SetLanguage(SystemLanguage NewLanguage) - changes the games language

## Localization Language Files
- Contains the Language type from SystemLanguage Enum
- 2 arrays. Keys & Values.
- int that contains Index. The Index is used to get the Key and Value from the array. A key and Value use the same Index.
- API
  - GetTranslation(String Key) returns the string Translation
  - GetLanguageType() returns the SystemLanguage enum for this Language file
  - GetKey(String Translation) returns string with given translation

## Localization Editor Window
- Displays Key-Value pairs from a single language file
- Language file must be selected or notification " Select Language File" is displayed in the Editor Window
- Shortcut %L to open up the Editor Window

## Localization Manager Component
- Holds an array of Language Files to access with the API's from the language files
- API
  - [BOOL]Auto Update Language - searches through the array of Language Files and sees if GetLanguage() matches any of the LanguageTypes of the Language files.
  - GetTranslation(String Key) returns the string Translation
  - GetLanguageType() returns the SystemLanguage enum for this Language file
  - GetKey(String Translation) returns string with given translation
  - GetLanguageCount() returns int of number of languages on the manager.

### Contributing

If you want to contribute back, please keep it under the unmodified MIT license so it can be integrated in future versions and shared under the same license.

We will look at everyones contributions and on the last business day of the month they will be merged into Master. 


### License 

The Localization system is released under an MIT/X11 license; see the LICENSE file.
This means that you pretty much can customize and embed it in any software under any license without any other constraints than preserving the copyright and license information while adding your own copyright and license information.
You can keep the source to yourself or share your customized version under the same MIT license or a compatible license.

### 1.0v Features 
- Single Language files with Key->Value pairs. 
- A Localize Text Component that stores multiple language files. Works similar to the Animation component and its animation array.
- Editor Window with Localization Editor

### 2.0v Features:
- Unity uses SystemLanguage Enum. This is not culture specific. There is no way to know the difference between Swiss German and Germany German. Moving Unity to System.Globalization CultureInfo would be the best way. This is already avaliable in C#. 

### 3.0v Features
- UI Localize Text Component that works with Unity's UI system. 
- UI Localize Textures, Materials, Sprites, UIImages and more. 
