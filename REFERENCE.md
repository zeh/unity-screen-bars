# Full reference

Properties are available on all platforms, but might not have any effect depending on what that platform or platform version supports.

### `ScreenBars.lowProfile` (`bool`)

All foreground content on bars is dimmed out (if visible). This is useful for games, book readers, video players, or any other "immersive" applications where the usual system chrome is considered too distracting.

Applies to: Android 4.0+

Default: `false`

### `ScreenBars.statusBarVisible` (`bool`)

Hides or shows the status bar atop the screen.

Applies to: Android (all versions)

Default: `false`

### `ScreenBars.statusBarTranslucent` (`bool`)

Xxx

Applies to: Android ?

Default: ?

Caveats:

* When this property is enabled on Android, the value of `statusBarBackgroundColor` is ignored.

### `ScreenBars.statusBarForegroundDark` (`bool`)

Requests the status bar to draw in a mode that is compatible with light backgrounds. In other words, it causes the foreground content of the status bar (text and icons) to be drawn using a dark color.

Applies to: Android 6.0+

Default: `false`

### `ScreenBars.statusBarBackgroundColor` (`bool`)

Xxx

Applies to: Android ?

Default: `0xff757575`

Caveats:

* On Android, a custom background color is only applied to the status bar when `statusBarTranslucent` is set to `false`.

### `ScreenBars.statusBarHeight` (`readonly int`)

Returns the current height of the status bar, in physical screen pixels. This can be used to adjust your own screen elements in a way that they align with the status bar content.

Notice that this always returns the system's configuration height for the status bar, regardless of its current visibility properties.

Applies to: Android ?

### `ScreenBars.navigationBarVisible` (`bool`)

Xxx

Applies to: Android ?

Default: ?

### `ScreenBars.navigationBarTranslucent` (`bool`)

Xxx

Applies to: Android ?

Default: ?

Caveats:

* When this property is enabled on Android, the value of `navigationBarBackgroundColor` is ignored.
* When this property is enabled, it imples `navigationBarOverlay`, drawning atop the existing application content.


### `ScreenBars.navigationBarOverlay` (`bool`)

Xxx

Applies to: Android ?

Default: ?

### `ScreenBars.navigationBarBackgroundColor` (`bool`)

Xxx

Applies to: Android ?

Default: `0xff000000`

Caveats:

* On Android, a custom background color is only applied to the navigation bar when `navigationBarTranslucent` is set to `false`.

### `ScreenBars.navigationBarForegroundDark` (`bool`)

Requests the navigation bar to draw in a mode that is compatible with light backgrounds. In other words, it causes the foreground content of the navigation bar (icons) to be drawn using a dark color.

Applies to: Android 8.0+

Default: `false`

### `ScreenBars.navigationBarHeight` (`readonly int`)

Returns the current height of the navigation bar, in physical screen pixels. This can be used to adjust your own screen elements in a way that they align with the navigation bar content.

Notice that this always returns the system's configuration height for the navigation bar, regardless of its current visibility properties.

Applies to: Android ?

### `ScreenBars.navigationBarAvailable` (`bool`)

Returns whether the device supports an on-screen navigation bar or not.

Notice that this always returns the device's ability to display a navigation bar, regardless of its current visibility properties.

Applies to: Android (all versions)
