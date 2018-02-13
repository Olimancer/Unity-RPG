using System;
using UnityEngine;
using UnityEngine.AI;
using Game.Cameras;

namespace Game.Characters.Players {
   [RequireComponent(typeof (AICharacterControl))]
   [RequireComponent(typeof (ThirdPersonCharacter))]
   [RequireComponent(typeof (MeleeWeaponAttack))]
   
   public class PlayerControls : MonoBehaviour {

      private GameObject            _moveTarget             = null;
      private AICharacterControl    _aiController           = null;
      private MeleeWeaponAttack     _meleeAttackController  = null;
   //	private Vector3               _movement               = Vector3.zero;
      private bool                  _isMovingToTarget       = false;
      private bool                  _isInDirectMode         = false;

      delegate bool ActionBool();

      private Vector3 ShortenDestination(Vector3 destination, float shortFactor) {
         return destination - ((destination - transform.position).normalized * shortFactor);
      }



      private void MoveTo(Transform target) {
         _aiController.SetTarget(target);
      }

      // TODO: Rethink approach
      private void MoveToAnd<DelegateType>(Transform target, DelegateType action) {
         
      }

      private bool AttackTargetInRange(GameObject target) {
         if (_meleeAttackController.IsTargetInRange(target)) {
            _meleeAttackController.AttackTarget(target);
            return true;
         }
         return false;
      }

      private void ProcessMouseClick(RaycastHit raycastHit, int layerHit) {
         // Change beheviour depending on the layer hit
         switch(layerHit) {
            case (Layer.Walkable):
               _moveTarget.transform.position = raycastHit.point;
               MoveTo(_moveTarget.transform);
               break;
            case (Layer.Enemy):
               if (!AttackTargetInRange(raycastHit.collider.gameObject)) {
                  MoveTo(raycastHit.transform);
               } else {
               //	TODO: Move to target and attack
               }
               break;
            case (Layer.Object):
               // TODO: Interact with object
               print("Object[ion]!");
               break;
            default:
               Debug.LogError("No layer detected! Are you under or at the edge of the map?");
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
            // _isMovingToTarget = false;
            print("Is In direct Mode : " + _isInDirectMode.ToString());
         }
      }

// -- Game loops

      void Start() {
         _aiController           = GetComponent<AICharacterControl>();
         _moveTarget             = new GameObject("Player Move Target");
         _meleeAttackController  = GetComponent<MeleeWeaponAttack>();
         Camera.main.GetComponent<CameraRaycaster>()._notifyMouseClicked += ProcessMouseClick;
      }

      void LateUpdate() {
         if (_isMovingToTarget) {

         }
      }
   }
}


