using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters {
	public class Enemy : Character, IDamageable {

		[SerializeField] private float 	_maxHealth 			= 250f;
		[SerializeField] internal float _maxMeleeRange		= 7f; // NOTE: To be replace with ability range at some point

		private float					_currentHealth;
		
		internal bool					_isAttacking		= false;
		internal GameObject				_player				= null;

		public float 					_health 			{ get { return _currentHealth; } }
		public float 					_healthAsPercentage { get { return (_currentHealth / _maxHealth); } }
		public float 					_distanceToPlayer	{ get { return (transform.position - _player.transform.position).magnitude; } }	

		[ExecuteInEditMode] void OnValidate() {
			_maxHealth = Mathf.Clamp(_maxHealth, 0f, float.MaxValue);
			_maxMeleeRange = Mathf.Clamp(_maxMeleeRange, 0f, float.MaxValue);
		}

		public delegate void 			OnEnemyDamaged();
		public event OnEnemyDamaged 	_notifyDamageTaken;

		void CheckForDeath() {
			if (_currentHealth <= 0) {
				Destroy(gameObject);
			}
		}

// -- Game loops

		void Awake() {
			_player = GameObject.FindGameObjectWithTag("Player");
			_currentHealth = _maxHealth;
		}

// -- On Events

		public void TakeDamage(float damage) {
			_currentHealth -= Mathf.Clamp((damage), 0f, _maxHealth);
			_notifyDamageTaken();
			CheckForDeath();
		}

// -- Editor Only

		void OnDrawGizmos() {
			Gizmos.color = new Color(1f, 0f, 0f, 1f);
			Gizmos.DrawWireSphere(transform.position, _maxMeleeRange);
		}
	}
}
