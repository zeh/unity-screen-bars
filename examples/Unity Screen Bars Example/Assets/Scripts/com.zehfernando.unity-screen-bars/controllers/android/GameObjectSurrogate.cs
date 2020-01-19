using UnityEngine;

namespace com.zehfernando.UnityScreenBars.android {
	public class GameObjectSurrogate : MonoBehaviour {
		private static GameObjectSurrogate instance;

		public delegate void SimpleHandler();

		public event SimpleHandler onGainedFocus;
		public event SimpleHandler onLostFocus;

		private bool hasFocus = true;
		private bool isPaused = false;
		private bool lastFocusValue = false;

		public static GameObjectSurrogate getInstance() {
			if (instance == null) {
				GameObject gameObject  = new GameObject("AndroidScreenBarsSurrogate");
				instance = gameObject.AddComponent<GameObjectSurrogate>();
			}

			return instance;
		}

		void Awake() {
			DontDestroyOnLoad(this);
		}

		void OnApplicationFocus(bool newHasFocus) {
			hasFocus = newHasFocus;
			updateFocusEvent();
		}

		void OnApplicationPause(bool newIsPaused) {
			isPaused = newIsPaused;
			updateFocusEvent();
		}

		private void updateFocusEvent() {
			bool newFocusValue = !isPaused && hasFocus;
			if (newFocusValue != lastFocusValue) {
				lastFocusValue = newFocusValue;
				if (lastFocusValue) {
					if (onGainedFocus != null) onGainedFocus();
				} else {
					if (onLostFocus != null) onLostFocus();
				}
			}
		}
	}
}
