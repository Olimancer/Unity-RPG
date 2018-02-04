using System;
using UnityEngine;
using UnityEngine.AI;
using Game.CameraNS;

namespace Game.Characters.PlayerNS {
	[RequireComponent(typeof (AICharacterControl))]
	[RequireComponent(typeof (ThirdPersonCharacter))]
	
	public class PlayerMovement : MonoBehaviour {

		//private ThirdPersonCharacter 	_character;
		private GameObject				_moveTarget			= null;
		private AICharacterControl		_aiController		= null;
	//	private Vector3					_movement			= Vector3.zero;
	//	private bool					_isMovingToTarget 	= false;
		private bool					_isInDirectMode 	= false; // TODO: consider making static later (likely inside the main player class script once created)

		private Vector3 ShortenDestination(Vector3 destination, float shortFactor) {
			return destination - ((destination - transform.position).normalized * shortFactor);
		}

		private void ProcessMouseClick(RaycastHit raycastHit, Layer layerHit) {
			// Change beheviour depending on the layer hit
			switch(layerHit) {
				case (Layer.Walkable): // Walkable
					_moveTarget.transform.position = raycastHit.point;
					_aiController.SetTarget(_moveTarget.transform);
					break;
				case (Layer.Enemy): // Enemies
					_aiController.SetTarget(raycastHit.transform);
					break;
				case (Layer.Object): // Objects
					print("Object[ion]!");
					break;
				default:
					print("The Eternal void has been detected! (shouln't be!)");
					return;
			}
		}

		// Convert inputs coming from the keyboard or a gamepad and save them to _movement
		private void DirectMovement() {
			// read inputs
			// float xInput 			= Input.GetAxis("Horizontal");
			// float yInput 			= Input.GetAxis("Vertical");
			// Transform camTransform 	= Camera.main.transform;

			// // calculate camera relative direction to move:
			// _movement = yInput 
			// 	* Vector3.Scale(camTransform.forward, new Vector3(1, 0, 1)).normalized 
			// 	+ xInput * camTransform.right;
		}

		private void ControlModeHandler() {
			// TODO: Change logic to activate automatically without the need for input
			if (Input.GetKeyDown(KeyCode.G)) { // G for gamepad. 
				_isInDirectMode = !_isInDirectMode;
	//			_isMovingToTarget = false;
				print("Is In direct Mode : " + _isInDirectMode.ToString());
			}
		}

// -- Game loops

		void Start() {
			_aiController		= GetComponent<AICharacterControl>();
			_moveTarget			= new GameObject("Player Move Target");

			Camera.main.GetComponent<CameraRaycaster>()._notifyMouseClicked += ProcessMouseClick;
		}
	}
}


