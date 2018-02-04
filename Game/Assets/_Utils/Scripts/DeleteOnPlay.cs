using UnityEngine;

// For gameobject useful in editor that shouldn't exist during play
// e.g NavMesh corrector
namespace Game.Utils {
	public class DeleteOnPlay : MonoBehaviour {
		void Awake() {
			Destroy(gameObject);
		}
	}
}