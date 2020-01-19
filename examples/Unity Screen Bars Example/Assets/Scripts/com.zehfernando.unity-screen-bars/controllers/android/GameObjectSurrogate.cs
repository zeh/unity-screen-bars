using UnityEngine;

namespace com.zehfernando.UnityScreenBars.android {
	public class GameObjectSurrogate : MonoBehaviour {
		private static GameObjectSurrogate instance;

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
	}
}
