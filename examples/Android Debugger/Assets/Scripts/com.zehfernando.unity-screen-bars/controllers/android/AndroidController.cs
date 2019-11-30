using System;
using UnityEngine;

using static com.zehfernando.UnityScreenBars.android.Utils;

namespace com.zehfernando.UnityScreenBars.android {
	public class AndroidController : IController {
		public AndroidController() {}

		public bool lowProfile {
			get {
				return getViewFlag(ViewFlags.SYSTEM_UI_FLAG_LOW_PROFILE);
			}
			set {
				setViewFlag(ViewFlags.SYSTEM_UI_FLAG_LOW_PROFILE, value);
			}
		}

		// TODO: make it work for all versions https://learnpainless.com/android/material/make-fully-android-transparent-status-bar
		public bool statusBarVisible {
			get {
				return getWindowFlag(WindowFlags.FLAG_FORCE_NOT_FULLSCREEN);
			}
			set {
				setViewFlag(ViewFlags.SYSTEM_UI_FLAG_FULLSCREEN, !value);
				setWindowFlag(WindowFlags.FLAG_FORCE_NOT_FULLSCREEN, value);
				setWindowFlag(WindowFlags.FLAG_FULLSCREEN, !value);
			}
		}

		public bool statusBarTranslucent {
			get {
				return getWindowFlag(WindowFlags.FLAG_TRANSLUCENT_STATUS);
			}
			set {
				setWindowFlag(WindowFlags.FLAG_TRANSLUCENT_STATUS, value);
			}
		}

		public bool statusBarForegroundDark {
			get {
				return getViewFlag(ViewFlags.SYSTEM_UI_FLAG_LIGHT_STATUS_BAR);
			}
			set {
				setViewFlag(ViewFlags.SYSTEM_UI_FLAG_LIGHT_STATUS_BAR, value);
			}
		}

		public uint statusBarBackgroundColor {
			get {
				// TODO: this API 21+ only, detect and protect
				using (var window = Utils.getWindow()) {
					return (uint)window.Call<int>("getStatusBarColor");
				}
			}
			set {
				Utils.runOnAndroidUiThread(() => setStatusBarBackgroundColorInThread(value));
			}
		}

		public int statusBarHeight {
			get {
				using (var resources = Utils.getResources()) {
					var heightResourceId = resources.Call<int>("getIdentifier", "status_bar_height", "dimen", "android");
					if (heightResourceId > 0) {
						return resources.Call<int>("getDimensionPixelSize", heightResourceId);
					} else {
						return 0;
					}
				}
			}
		}

		public bool navigationBarVisible {
			// Reference: https://developer.android.com/training/system-ui/navigation#40
			get {
				return !getViewFlag(ViewFlags.SYSTEM_UI_FLAG_HIDE_NAVIGATION);
			}
			set {
				// TODO: finish this
				// var willHide = navigationBarVisible && !value;
				// var isAlreadyOverlaying = navigationBarTranslucent || navigationBarOverlay;
				// if (willHide && !isAlreadyOverlaying) {
				// 	// We can't set the navigation bar to visible if it's not already prepared for it, so we overlay first
				// 	navigationBarOverlay = true;
				// }

				setViewFlag(ViewFlags.SYSTEM_UI_FLAG_HIDE_NAVIGATION, !value);
			}
		}

		public bool navigationBarTranslucent {
			get {
				return getWindowFlag(WindowFlags.FLAG_TRANSLUCENT_NAVIGATION);
			}
			set {
				setWindowFlag(WindowFlags.FLAG_TRANSLUCENT_NAVIGATION, value);
			}
		}

		public bool navigationBarOverlay {
			// Reference: https://developer.android.com/training/system-ui/navigation#behind
			get {
				return
					Utils.getSDKVersion() >= BuildVersionCodes.JELLY_BEAN &&
					getViewFlag(ViewFlags.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION);
			}
			set {
				if (Utils.getSDKVersion() >= BuildVersionCodes.JELLY_BEAN) {
					if (value) {
						// Overlay
						setViewFlag(ViewFlags.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION, value);
						setViewFlag(ViewFlags.SYSTEM_UI_FLAG_LAYOUT_STABLE, value);
					} else {
						// Solid
						setViewFlag(ViewFlags.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION, value);
					}
				}
				// var _navigationBarVisible = navigationBarVisible;
				// setViewFlag(ViewFlags.SYSTEM_UI_FLAG_HIDE_NAVIGATION, !_navigationBarVisible);
				// setViewFlag(ViewFlags.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN, value);
				// setViewFlag(ViewFlags.SYSTEM_UI_FLAG_LAYOUT_STABLE, value);
			}
		}

		public bool navigationBarForegroundDark {
			get {
				return getViewFlag(ViewFlags.SYSTEM_UI_FLAG_LIGHT_NAVIGATION_BAR);
			}
			set {
				setViewFlag(ViewFlags.SYSTEM_UI_FLAG_LIGHT_NAVIGATION_BAR, value);
			}
		}

		public uint navigationBarBackgroundColor {
			get {
				// TODO: this API 21+ only, detect and protect
				using (var window = Utils.getWindow()) {
					return (uint)window.Call<int>("getNavigationBarColor");
				}
			}
			set {
				Utils.runOnAndroidUiThread(() => setNavigationBarBackgroundColorInThread(value));
			}
		}

		public int navigationBarHeight {
			// TODO: check navigationBarAvailable?
			get {
				using (var resources = Utils.getResources()) {
					var heightResourceId = resources.Call<int>("getIdentifier", "navigation_bar_height", "dimen", "android");
					if (heightResourceId > 0) {
						return resources.Call<int>("getDimensionPixelSize", heightResourceId);
					} else {
						return 0;
					}
				}
			}
		}

		public bool navigationBarAvailable {
			get {
				using (var resources = Utils.getResources()) {
					// Check device navigation bar option
					var barResourceId = resources.Call<int>("getIdentifier", "config_showNavigationBar", "bool", "android");
					if (barResourceId > 0 && resources.Call<bool>("getBoolean", barResourceId)) return true;

					// Check emulator navigation bar option
					if (Utils.getSystemProperty<string>("qemu.hw.mainkeys") == "1") return true;

					// Otherwise, it's just not available
					return false;
				}
			}
		}

		private void setStatusBarBackgroundColorInThread(uint value) {
			// TODO: this API 21+ only, detect and protect
			using (var window = Utils.getWindow()) {
				window.Call("setStatusBarColor", unchecked((int)value));
			}
		}

		private void setNavigationBarBackgroundColorInThread(uint value) {
			// TODO: this API 21+ only, detect and protect
			using (var window = Utils.getWindow()) {
				window.Call("setNavigationBarColor", unchecked((int)value));
			}
		}
	}
}
