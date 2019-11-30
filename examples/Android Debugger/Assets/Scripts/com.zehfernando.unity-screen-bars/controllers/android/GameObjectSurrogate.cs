using UnityEngine;

namespace com.zehfernando.UnityScreenBars.android {
	public class GameObjectSurrogate : MonoBehaviour {
		void Awake() {
			DontDestroyOnLoad(this);
		}
	}
}
