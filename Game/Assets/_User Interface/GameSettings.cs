using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {
	const int ENEMY_LAYER 		= 9;
	const int ENEMYATTACK_LAYER = 13;

	void Awake() {
		Physics.IgnoreLayerCollision(ENEMY_LAYER, ENEMYATTACK_LAYER);
	}
}
