# Android Flags

Dealing with the visibility properties of status and navigation bars on Android is a complicated affair. Rather than being accessible through clear properties or methods, the bars are controlled by a confusing mix of [`View` flags](https://developer.android.com/reference/android/view/View.html#setSystemUiVisibility(int)), [`Window` flags](https://developer.android.com/reference/android/view/Window.html#setFlags(int,%20int)), [toggleable `Window` features](https://developer.android.com/reference/android/view/Window.html#FEATURE_ACTION_BAR_OVERLAY), [theme settings](https://developer.android.com/training/system-ui/status#40), and [`ActionBar` methods](https://developer.android.com/reference/android/app/Activity.html#getActionBar()). This means that the answer to a simple question like "is the status bar visible?" is scattered along multiple different places, each conflicting with each other in some way.

Oh, and it also changes from Android version to Android version. Android's UI - and thus its bars - evolved with time, with features added atop features, as well as some features forgotten or ignored. Therefore Android's screen bars API should be thought as something that happened _by accident_, over a long period of time, rather than something that was _designed_ in its current state.

Anyhow, it's a freaking mess. Thus, this file is an attempt to document the flags used in those bars to some extent, written mostly for my own sanity.

The information contained here should not be needed by users of UnityScreenBars, so feel free to ignore it. They can, however, come in handy when debugging the library itself, or for developers attempting to implement screen bar low-level functionality in their own code. It took a long time, and a lot of trial and error, to get this document together.

## Flag reference

Most of the features supported in this library use two specific sets of flags on Android: `View` and `Window` flags.

### `View` flags

`View` flags are added to individual `View` elements via [`View.setSystemUiVisibility()`](https://developer.android.com/reference/android/view/View.html#setSystemUiVisibility(int)). They dictate how the application will behave when that specific view is visible. Any normal Android application (even one created with Unity) can contain several different `View` instances nested inside each other; in this library's case, the flags are applied to the "Decor View", which is [the root view](https://stackoverflow.com/questions/23276847/what-is-an-android-decorview) used by the system's window. This ensures that the flags we activate are always applied.

| Flag                                 | Value (decimal) | Value (binary) | API Level |
| :----------------------------------- | :-------------: | :------------: | :-------: |
| [`View.SYSTEM_UI_FLAG_FULLSCREEN`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_FULLSCREEN)<br>Go into "full screen" mode, hiding non-critical screen decorations: in practice, hides the status bar. | `4` | `100` | 16+ |
| [`View.SYSTEM_UI_FLAG_HIDE_NAVIGATION`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_HIDE_NAVIGATION)<br>Hides the navigation bar, if one is present. | `2` | `10` | 14+ |
| [`View.SYSTEM_UI_FLAG_IMMERSIVE`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_IMMERSIVE)<br>States that the view would like to continue to be interactive when using `SYSTEM_UI_FLAG_HIDE_NAVIGATION` to hide the navigation bar, therefore not clearing that flag after the first user interaction. | `2048` | `100000000000` | 19+ |
| [`View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_IMMERSIVE_STICKY)<br>States that the view would like to continue to be interactive when using `SYSTEM_UI_FLAG_FULLSCREEN` and/or `SYSTEM_UI_FLAG_HIDE_NAVIGATION` to hide any of the existing UI, therefore not clearing `SYSTEM_UI_FLAG_HIDE_NAVIGATION` after the first user interaction and `SYSTEM_UI_FLAG_FULLSCREEN` when pulling from the top of the screen. | `4096` | `1000000000000` | 19+ |
| [`View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN)<br>Calculates the view layout space as if `SYSTEM_UI_FLAG_FULLSCREEN` was applied, even if it's not. | `1024` | `10000000000` | 16+ |
| [`View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION)<br>Calculates the view layout space as if the navigation was hidden using `SYSTEM_UI_FLAG_HIDE_NAVIGATION`, even if it's not. | `512` | `1000000000` | 16+ |
| [`View.SYSTEM_UI_FLAG_LAYOUT_STABLE`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_LAYOUT_STABLE)<br>Complex flag that tells the system to use stable values when calculating application insets for state transitions. | `256` | `100000000` | 16+ |
| [`View.SYSTEM_UI_FLAG_LIGHT_NAVIGATION_BAR`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_LIGHT_NAVIGATION_BAR)<br>Assumes the navigation bar will be drawn over a light background, so all icons are rendered with a dark color. | `16` | `10000` | 26+ |
| [`View.SYSTEM_UI_FLAG_LIGHT_STATUS_BAR`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_LIGHT_STATUS_BAR)<br>Assumes the status bar will be drawn over a light background, so all icons are rendered with a dark color. | `8192` | `10000000000000` | 23+ |
| [`View.SYSTEM_UI_FLAG_LOW_PROFILE`](https://developer.android.com/reference/android/view/View.html#SYSTEM_UI_FLAG_LOW_PROFILE)<br>Icons in the status and navigation bar are dimmed (when visible). This can be used by "immersive" applications when the usual system chrome is considered too distracting. | `1` | `1` | 14+ |

### `Window` flags

On top of the `View` flags, we also have `Window` flags. Those also dictate how the application's boundaries will be calculated. They are applied to its layout properties via [`Window.setFlags()`](https://developer.android.com/reference/android/view/Window.html#setFlags(int,%20int)).

| Flag                                 | Value (decimal) | Value (binary) | API Level |
| :----------------------------------- | :-------------: | :------------: | :-------: |
| [`WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS`](https://developer.android.com/reference/android/view/WindowManager.LayoutParams.html#FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS)<br>Indicates the window is responsible for drawing the background for system bars (including the status and navigation bars), ultimately allowing custom background colors to be used. | `-2147483648` | `10000000000000000000000000000000` | 21+ |
| [`WindowManager.LayoutParams.FLAG_FORCE_NOT_FULLSCREEN`](https://developer.android.com/reference/android/view/WindowManager.LayoutParams.html#FLAG_FORCE_NOT_FULLSCREEN)<br>Overrides `FLAG_FULLSCREEN`, forcing screen decorations to be shown. | `2048` | `100000000000` | 1+ |
| [`WindowManager.LayoutParams.FLAG_FULLSCREEN`](https://developer.android.com/reference/android/view/WindowManager.LayoutParams.html#FLAG_FULLSCREEN)<br>Hide all screen decorations (such as the status bar) while this window is displayed. | `1024` | `10000000000` | 1+ |
| [`WindowManager.LayoutParams.FLAG_LAYOUT_IN_SCREEN`](https://developer.android.com/reference/android/view/WindowManager.LayoutParams.html#FLAG_LAYOUT_IN_SCREEN)<br>Uses the entire screen to render the application, ignoring decorations like the status bar. | `256` | `100000000` | 1+ |
| [`WindowManager.LayoutParams.FLAG_LAYOUT_INSET_DECOR`](https://developer.android.com/reference/android/view/WindowManager.LayoutParams.html#FLAG_LAYOUT_INSET_DECOR)<br>When used with `FLAG_LAYOUT_IN_SCREEN`, indicates the status bar will overlay the screen content. | `65536` | `10000000000000000` | 1+ |
| [`WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS`](https://developer.android.com/reference/android/view/WindowManager.LayoutParams.html#FLAG_LAYOUT_NO_LIMITS)<br>Allow window to extend outside of the screen. | `512` | `1000000000` | 1+ |
| [`WindowManager.LayoutParams.FLAG_TRANSLUCENT_NAVIGATION`](https://developer.android.com/reference/android/view/WindowManager.LayoutParams.html#FLAG_TRANSLUCENT_NAVIGATION)<br>Requests a translucent navigation bar with minimal system-provided background protection. | `134217728` | `1000000000000000000000000000` | 19+ |
| [`WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS`](https://developer.android.com/reference/android/view/WindowManager.LayoutParams.html#FLAG_TRANSLUCENT_STATUS)<br>Requests a translucent status bar with minimal system-provided background protection. | `67108864` | `100000000000000000000000000` | 19+ |

## Built-in Unity settings

When toggling `Screen.fullScreen` in Unity (either via a script, or by setting the player's default settings for Android), Unity uses the same flags to toggle bars' visibility settings.

For reference, these are the flag values set for each option:

| Flag                                                           | `Screen.fullScreen false` | `Screen.fullScreen true`    |
| :------------------------------------------------------------- | :-----------------------: | :-------------------------: |
| `View.SYSTEM_UI_FLAG_FULLSCREEN`                               | `0`                       | `1`                         |
| `View.SYSTEM_UI_FLAG_HIDE_NAVIGATION`                          | `0`                       | `1`                         |
| `View.SYSTEM_UI_FLAG_IMMERSIVE`                                | `0`                       | `0`                         |
| `View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY`                         | `0`                       | `1`                         |
| `View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN`                        | `1`                       | `1`                         |
| `View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION`                   | `0`                       | `1`                         |
| `View.SYSTEM_UI_FLAG_LAYOUT_STABLE`                            | `0`                       | `1`                         |
| `View.SYSTEM_UI_FLAG_LIGHT_NAVIGATION_BAR`                     | `0`                       | `0`                         |
| `View.SYSTEM_UI_FLAG_LIGHT_STATUS_BAR`                         | `0`                       | `0`                         |
| `View.SYSTEM_UI_FLAG_LOW_PROFILE`                              | `0`                       | `0`                         |
| `WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS` | `1`                       | `1`                         |
| `WindowManager.LayoutParams.FLAG_FORCE_NOT_FULLSCREEN`         | `0`                       | `0`                         |
| `WindowManager.LayoutParams.FLAG_FULLSCREEN`                   | `1`                       | `1`                         |
| `WindowManager.LayoutParams.FLAG_LAYOUT_IN_SCREEN`             | `1`                       | `1`                         |
| `WindowManager.LayoutParams.FLAG_LAYOUT_INSET_DECOR`           | `1`                       | `1`                         |
| `WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS`             | `0`                       | `0`                         |
| `WindowManager.LayoutParams.FLAG_TRANSLUCENT_NAVIGATION`       | `0`                       | `0`                         |
| `WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS`           | `0`                       | `0`                         |

## Android API Reference

For easy reference, a list of Android's recent release versions and their relevant API levels follows.

| Version | Name                | API Level | Release date    |
| ------- | ------------------- | --------- | ----------------|
| 10      | Q                   | 29        | September 2019  |
| 9       | Pie                 | 28        | August 2018     |
| 8.1     | Oreo                | 27        | December 2017   |
| 8.0     | Oreo                | 26        | August 2017     |
| 7.1     | Nougat              | 25        | October 2016    |
| 7.0     | Nougat              | 24        | August 2016     |
| 6.0     | Marshmallow         | 23        | Octover 2015    |
| 5.1     | Lollipop            | 22        | March 2015      |
| 5.0     | Lollipop            | 21        | October 2014    |
| 4.4W    | KitKat              | 20        | June 2014       |
| 4.4     | KitKat              | 19        | October 2013    |
| 4.3     | Jelly Bean          | 18        | July 2013       |
| 4.2     | Jelly Bean          | 17        | October 2012    |
| 4.1     | Jelly Bean          | 16        | July 2012       |
| 4.0.3   | Ice Cream Sandwich  | 15        | December 2011   |
| 4.0     | Ice Cream Sandwich  | 14        | October 2011    |

For a complete list, check Android's [platforms documentation](https://developer.android.com/studio/releases/platforms).

## Further references

* [Hiding the status bar](https://developer.android.com/training/system-ui/status)
* [Hiding the navigation bar](https://developer.android.com/training/system-ui/navigation)
* [Dimming the bars](https://developer.android.com/training/system-ui/dim)
