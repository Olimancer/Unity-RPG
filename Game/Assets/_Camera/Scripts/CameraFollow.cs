using UnityEngine;
using System.Collections;

namespace Game.CameraNS {
	public class CameraFollow : MonoBehaviour {

		GameObject _player;

		// Use this for initialization
		void Start () {
			_player = GameObject.FindGameObjectWithTag("Player");
		}
		
		// Update is called once per frame
		void LateUpdate () {
			transform.position = _player.transform.position;
		}
	}
}

