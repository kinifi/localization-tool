# localization-tool
A Language Localization Tool For Unity3D

This is a string table to store localized text for your games into a Language Asset File. 

## Localization Language Files

## Localization Editor Window

## Localization Manager


### 1.0v Features 

- Single Language files with Key->Value pairs. 
- A Localize Text Component that stores multiple language files. Works similar to the Animation component and its animation array.
- UI Localize Text Component that works with Unity's UI system. 
- Editor Window with Localization Editor

2.0v Features:
- Unity uses SystemLanguage Enum. This is not culture specific. There is no way to know the difference between Swiss German and Germany German. Moving Unity to System.Globalization CultureInfo would be the best way. This is already avaliable in C#. 

3.0v Features
