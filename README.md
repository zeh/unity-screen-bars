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

TODO: UPM, manual?

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
