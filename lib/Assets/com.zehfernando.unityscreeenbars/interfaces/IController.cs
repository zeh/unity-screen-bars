using UnityEngine;

namespace com.zehfernando.UnityScreenBars {
	interface IController {
		bool lowProfile { get; set; }

		bool statusBarVisible { get; set; }
		bool statusBarTranslucent { get; set; }
		bool statusBarForegroundDark { get; set; }
		uint statusBarBackgroundColor { get; set; }
		int statusBarHeight { get; }

		bool navigationBarVisible { get; set; }
		bool navigationBarTranslucent { get; set; }
		bool navigationBarOverlay { get; set; }
		bool navigationBarForegroundDark { get; set; }
		uint navigationBarBackgroundColor { get; set; }
		int navigationBarHeight { get; }
		bool navigationBarAvailable { get; }
	}
}
