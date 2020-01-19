#region Common definitions
#if UNITY_ANDROID && !UNITY_EDITOR
#define USE_ANDROID_NATIVE
#elif UNITY_ANDROID && UNITY_EDITOR
#define USE_ANDROID_EDITOR
#endif
#endregion

using System;
using System.Collections.Generic;
using UnityEngine;

using com.zehfernando.UnityScreenBars;

public class ScreenBars {
	static readonly IController controller;

	static ScreenBars() {
		#if USE_ANDROID_NATIVE
			controller = new com.zehfernando.UnityScreenBars.android.AndroidController();
		#elif USE_ANDROID_EDITOR
			// TODO: better Android simulator
			controller = new com.zehfernando.UnityScreenBars.simulated.SimulatedController();
		#else
			controller = new com.zehfernando.UnityScreenBars.simulated.SimulatedController();
		#endif
	}

	public static bool lowProfile {
		get { return controller.lowProfile; }
		set { controller.lowProfile = value; }
	}

	public static bool statusBarVisible {
		get { return controller.statusBarVisible; }
		set { controller.statusBarVisible = value; }
	}

	public static bool statusBarTranslucent {
		get { return controller.statusBarTranslucent; }
		set { controller.statusBarTranslucent = value; }
	}

	public static bool statusBarForegroundDark {
		get { return controller.statusBarForegroundDark; }
		set { controller.statusBarForegroundDark = value; }
	}

	public static uint statusBarBackgroundColor {
		get { return controller.statusBarBackgroundColor; }
		set { controller.statusBarBackgroundColor = value; }
	}

	public static int statusBarHeight {
		get { return controller.statusBarHeight; }
	}

	public static bool navigationBarVisible {
		get { return controller.navigationBarVisible; }
		set { controller.navigationBarVisible = value; }
	}

	public static bool navigationBarTranslucent {
		get { return controller.navigationBarTranslucent; }
		set { controller.navigationBarTranslucent = value; }
	}

	public static bool navigationBarOverlay {
		get { return controller.navigationBarOverlay; }
		set { controller.navigationBarOverlay = value; }
	}

	public static bool navigationBarForegroundDark {
		get { return controller.navigationBarForegroundDark; }
		set { controller.navigationBarForegroundDark = value; }
	}

	public static uint navigationBarBackgroundColor {
		get { return controller.navigationBarBackgroundColor; }
		set { controller.navigationBarBackgroundColor = value; }
	}

	public static int navigationBarHeight {
		get { return controller.navigationBarHeight; }
	}

	public static bool navigationBarAvailable {
		get { return controller.navigationBarAvailable; }
	}
}
