# Unity ScreenBars

*Unity ScreenBars* is a Unity script library meant to help developers control the visibility of screen bars on mobile platforms. Currently, it allows developers to programmatically control aspects of the status bar and navigation bar on Android, with iOS being the next targetted platform.

It comes as a free, [Unity Package Manager](https://docs.unity3d.com/Manual/upm-parts.html)-installable script library.

***Unity ScreenBars is still a work in progress and not stable enough for production use.***

## Advantages

Unity ScreenBars...

* Plays well with other scripts that might change the current screen bars status (e.g. `Screen.fullScreen`), complimenting its feature set
* Offers an easier to use API to control an application's appearance om mobile, specially considering [how difficult it is to do so on Android](ANDROIDFLAGS.md)
* Comes as an Unity Package Manager package, ensuring easy installation and updates
* Supports Android 4.0 (API 14) and above, downgrading features when needed

## Installation

### Method 1: installing via Unity Package Manager and NPM (recommended; Unity 2018.1+)

The [Unity Package Manager](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@1.8/manual/index.html) (UPM) is a new method to manage external packages. It keeps package contents separate from your main project files.

Using a [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html), you can use a version of this package that has been published to [NPM](https://www.npmjs.com/), a popular package host.

On your project's `Packages/manifest.json` file, add a new entry to the `scopedRegistries` array (creating it if necessary):

```json
{
  "name": "Zeh Fernando",
  "url": "https://registry.npmjs.com",
  "scopes": [ "com.zehfernando" ]
}
```

Then, a new entry to the `dependencies` object:

```json
"com.zehfernando.unityscreenbars": "1.0.0"
```

After these changes, your `manifest.json` file should look something like this:

```json
{
  "scopedRegistries": [
    {
      "name": "Zeh Fernando",
      "url": "https://registry.npmjs.com",
      "scopes": [ "com.zehfernando" ]
    }
  ],
  "dependencies": {
    "com.zehfernando.unityscreenbars": "1.0.0",
	...many other entries...
  }
}
```

### Method 2: installing via Unity Package Manager and GIT (Unity 2018.1+)

...TODO...

```
"com.zehfernando.unityscreenbars": "https://github.com/zeh/unity-screen-bars.git#package"
```

### Method 3: manual installation (not recommended)

...TODO...

If you don't use the Unity Package Manager, you can copy the folder from `lib/Assets/com.zehfernando.unityscreenbars` into your project's `Assets` folder.


## How to use

This library is called as a standalone, static class.

```CS
// Show status bar
ScreenBars.statusBarVisible = true;

// Dims all bars' icons
ScreenBars.lowProfile = true;

// Makes the status bar content light
ScreenBars.statusBarForegroundDark = false;
```

Please check the [reference](REFERENCE.md) for a list of all available properties.

## Todo

* See if it's possible to show the bar without overlaying
* Implement for iOS
* Finish Android
  + navigationBarVisible
  	* doesn't always work to hide, if Screen.fullScreen = false. We   already have code to detect this, need to apply solution (set overlsy   first?)
  + navigationBarOverlay
  	* not working
  	* only works when setting translucent first?
  	! Only on Android 4.1+ https://developer.android.com/training/  system-ui/navigation#behind

## Other acknowledgments

Unity ScreenBars is a more modern evolution of [`ApplicationChrome.cs`](https://github.com/zeh/unity-tidbits/blob/master/application/ApplicationChrome.cs), a script first published as a [a blog post](https://zehfernando.com/2015/unity-tidbits-changing-the-visibility-of-androids-navigation-and-status-bars-and-implementing-immersive-mode/).
