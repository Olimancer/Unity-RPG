using UnityEngine;

// Force the object to always face the camera (works better with 2D UI objects)

namespace Game.Utils {
	public class FaceCamera : MonoBehaviour {
		void LateUpdate() {
			transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
		}
	}
}
