using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.CameraNS;
using Game.Weapons;

namespace Game.Characters {
	public class Player : Character, IDamageable {

		[SerializeField] private float 				_meleeAttackRadius 		= 2.15f;
		private MeleeWeaponAttack					_meleeAttackController 	= null;

		[SerializeField] private float 				_maxHealth 				= 250;
		private float								_currentHealth 			= 250;
		public float 								_health 				{ get { return _currentHealth; } }
		public float 								_healthAsPercentage 	{ get { return (_currentHealth / _maxHealth); } }

		public delegate void OnPlayerDamaged();
		public event OnPlayerDamaged _notifyPlayerDamaged;

	// -- Game init and loops

		void Start() {
			Camera.main.GetComponent<CameraRaycaster>()._notifyMouseClicked += OnMouseClicked;
			_meleeAttackController = GetComponent<MeleeWeaponAttack>();
		}

	// -- On events

		public void TakeDamage(float damage) {
			_currentHealth -= Mathf.Clamp(damage, 0f, _maxHealth);
			_notifyPlayerDamaged();
		}

		private void OnMouseClicked(RaycastHit raycastHit, Layer layerHit) {
			if (layerHit == Layer.Enemy) {
				var enemy = raycastHit.collider.gameObject;
				if (_meleeAttackController.IsTargetInRange(enemy)) {
					_meleeAttackController.AttackTarget(enemy);
				}
			}
		}

	// -- Editor Only

		private void OnDrawGizmos() {
	
		}
	}
}

