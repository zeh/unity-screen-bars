using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.zehfernando.UnityScreenBars.android {
	public class Utils {
		private static int queuedViewFlags = -1;
		private static int queuedWindowFlags = -1;
		private static bool hasQueuedFlagUpdates = false;
		private static GameObjectSurrogate surrogate = null;

		public static bool getViewFlag(int bitMask) {
			return (getViewFlags() & bitMask) != 0;
		}

		public static void setViewFlag(int bitMask, bool value) {
			var flags = getViewFlags();
			var newBit = value ? bitMask : 0;
			setViewFlags((flags & ~bitMask) | newBit);
		}

		public static bool getWindowFlag(int bitMask) {
			return (getWindowFlags() & bitMask) != 0;
		}

		public static void setWindowFlag(int bitMask, bool value) {
			var flags = getWindowFlags();
			var newBit = value ? bitMask : 0;
			setWindowFlags((flags & ~bitMask) | newBit);
		}

		public static int getViewFlags() {
			if (queuedViewFlags != -1) return queuedViewFlags;

			using (var window = getWindow()) {
				using (var view = window.Call<AndroidJavaObject>("getDecorView")) {
					return view.Call<int>("getSystemUiVisibility");
				}
			}
		}

        public static int getStackedViewFlags() {
            using (var window = getWindow()) {
                using (var view = window.Call<AndroidJavaObject>("getDecorView")) {
                    return view.Call<int>("getWindowSystemUiVisibility");
                }
            }
        }

		public static void setViewFlags(int value) {
			queuedViewFlags = value;
			queueFlagUpdates();
		}

		public static int getWindowFlags() {
			if (queuedWindowFlags != -1) return queuedWindowFlags;

			using (var window = getWindow()) {
				// Although we do window.setFlags() to set the, we need to do
				// window.getAttributes().flags to get them back
				// See: https://android.googlesource.com/platform/frameworks/base/+/master/core/java/android/view/Window.java#1144
				using (var attributes = window.Call<AndroidJavaObject>("getAttributes")) {
					return attributes.Get<int>("flags");
				}
			}
		}

		public static void setWindowFlags(int value) {
			queuedWindowFlags = value;
			queueFlagUpdates();
		}

		private static GameObjectSurrogate getGameObject() {
			if (surrogate == null) {
				// We create a GameObject so we can run end-of-frame updates for flags (see queueFlagUpdates() for details)
				GameObject surrogateObject  = new GameObject("AndroidScreenBarsSurrogate");
				surrogate = surrogateObject.AddComponent<GameObjectSurrogate>();
			}
			return surrogate;
		}

		private static void queueFlagUpdates() {
			// We perform View.* and Window.* flag updates in a pretty roundabout way:
			// we call a coroutine at the end of the current frame, which then asks Android to run
			// some code on the UI thread.
			// This potentially adds a frame of delay to screen bar updates, but is done because:
			// * we can successive calls to setViewFlag()/setWindowFlag() to stack correctly,
			//   without transient flag states that can be reverted by the OS (plus, we want
			//   getViewFlags() and getWindowFlags() to return the correct flag status, even
			//   if unset)
			// * view flags can only be set on Android's UI thread
			if (!hasQueuedFlagUpdates) {
				hasQueuedFlagUpdates = true;
				var go = getGameObject();
				go.StartCoroutine(performFlagUpdatesOnUiThread());
			}
		}

		private static IEnumerator<object> performFlagUpdatesOnUiThread() {
			yield return new WaitForEndOfFrame();
			runOnAndroidUiThread(() => {
				// Update view flags
				if (queuedViewFlags != -1) {
					using (var window = getWindow()) {
						using (var view = window.Call<AndroidJavaObject>("getDecorView")) {
							// We also remove the existing listener. It seems Unity uses it internally
							// to detect changes to the visibility flags, and re-apply its own changes.
							// For example, if we hide the navigation bar, it shows up again 1 sec later.
							view.Call("setOnSystemUiVisibilityChangeListener", null);
							view.Call("setSystemUiVisibility", queuedViewFlags);
						}
					}
				}

				// Update window flags
				if (queuedWindowFlags != -1) {
					using (var window = getWindow()) {
						window.Call("setFlags", queuedWindowFlags, -1); // (int)0x7FFFFFFF, or 1111111111111111111111111111111
					}
				}

				queuedViewFlags = -1;
				queuedWindowFlags = -1;
				hasQueuedFlagUpdates = false;
			});
		}

		public static void runOnAndroidUiThread(Action target) {
			using (var activity = getActivity()) {
				activity.Call("runOnUiThread", new AndroidJavaRunnable(target));
			}
		}

		public static T getSystemProperty<T>(string name) {
			using (var SystemProperties = new AndroidJavaClass("android.os.SystemProperties")) {
				return SystemProperties.CallStatic<T>("get", name);
			}
		}

		public static int getSDKVersion() {
			using (var BuildVersion = new AndroidJavaClass("android.os.Build$VERSION")) {
				return BuildVersion.GetStatic<int>("SDK_INT");
			}
		}

		public static AndroidJavaObject getActivity() {
			using (var UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				return UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			}
		}

		public static AndroidJavaObject getWindow() {
			using (var activity = getActivity()) {
				return activity.Call<AndroidJavaObject>("getWindow");
			}
		}

		public static AndroidJavaObject getResources() {
			using (var activity = getActivity()) {
				return activity.Call<AndroidJavaObject>("getResources");
			}
		}
	}
}
