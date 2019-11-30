using UnityEngine;

namespace com.zehfernando.UnityScreenBars.simulated {
	public class SimulatedController : IController {
		private bool _lowProfile = false;
		private bool _statusBarVisible = false;
		private bool _statusBarTranslucent = false;
		private bool _statusBarForegroundDark = false;
		private uint _statusBarBackgroundColor = 0x00000000;
		private int _statusBarHeight = 0;

		private bool _navigationBarVisible = false;
		private bool _navigationBarOverlay = false;
		private bool _navigationBarTranslucent = false;
		private bool _navigationBarForegroundDark = false;
		private uint _navigationBarBackgroundColor = 0x00000000;
		private int _navigationBarHeight = 0;
		private bool _navigationBarAvailable = false;

		public SimulatedController() {}

		public bool lowProfile {
			get { return _lowProfile; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting lowProfile with a value of {value}");
				_lowProfile = value;
			}
		}

		public bool statusBarVisible {
			get { return _statusBarVisible; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting statusBarVisible with a value of {value}");
				_statusBarVisible = value;
			}
		}

		public bool statusBarTranslucent {
			get { return _statusBarTranslucent; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting statusBarTranslucent with a value of {value}");
				_statusBarTranslucent = value;
			}
		}

		public bool statusBarForegroundDark {
			get { return _statusBarForegroundDark; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting statusBarForegroundDark with a value of {value}");
				_statusBarForegroundDark = value;
			}
		}

		public uint statusBarBackgroundColor {
			get { return _statusBarBackgroundColor; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting statusBarBackgroundColor with a value of {value}");
				_statusBarBackgroundColor = value;
			}
		}

		public int statusBarHeight {
			get { return _statusBarHeight; }
		}

		public bool navigationBarVisible {
			get { return _navigationBarVisible; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting navigationBarVisible with a value of {value}");
				_navigationBarVisible = value;
			}
		}

		public bool navigationBarTranslucent {
			get { return _navigationBarTranslucent; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting navigationBarTranslucent with a value of {value}");
				_navigationBarTranslucent = value;
			}
		}

		public bool navigationBarOverlay {
			get { return _navigationBarOverlay; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting navigationBarOverlay with a value of {value}");
				_navigationBarOverlay = value;
			}
		}

		public bool navigationBarForegroundDark {
			get { return _navigationBarForegroundDark; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting navigationBarForegroundDark with a value of {value}");
				_navigationBarForegroundDark = value;
			}
		}

		public uint navigationBarBackgroundColor {
			get { return _navigationBarBackgroundColor; }
			set {
				Debug.Log($"[ScreenBars] Simulating setting navigationBarBackgroundColor with a value of {value}");
				_navigationBarBackgroundColor = value;
			}
		}

		public int navigationBarHeight {
			get { return _navigationBarHeight; }
		}

		public bool navigationBarAvailable {
			get { return _navigationBarAvailable; }
		}
	}
}
