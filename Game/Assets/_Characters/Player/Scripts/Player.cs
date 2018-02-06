using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.CameraNS;
using Game.Weapons;

namespace Game.Characters {
	public class Player : Character, IDamageable {

		[SerializeField] private float 				_meleeAttackRadius 		= 2.15f;

		[SerializeField] private float 				_maxHealth 				= 250;
		private float								_currentHealth;
		public float 								_health 				{ get { return _currentHealth; } }
		public float 								_healthAsPercentage 	{ get { return (_currentHealth / _maxHealth); } }

		public delegate void OnPlayerDamaged();
		public event OnPlayerDamaged _notifyPlayerDamaged;

	// -- Game init and loops

		void Start() {
			_currentHealth = _maxHealth;
		}

	// -- On events

		public void TakeDamage(float damage) {
			_currentHealth -= Mathf.Clamp(damage, 0f, _maxHealth);
			_notifyPlayerDamaged();
		}
	// -- Editor Only

		private void OnDrawGizmos() {
	
		}
	}
}

