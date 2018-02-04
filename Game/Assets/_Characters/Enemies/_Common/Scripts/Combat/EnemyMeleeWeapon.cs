using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Enemies {
	[RequireComponent(typeof(Enemy))]

	public class EnemyMeleeWeapon : MeleeWeaponAttack {

		[Tooltip("Added to the weapon recovery time (in seconds) to make the attack rate faster or slower")]
		[SerializeField] private float 	_attackRecoveryOffset 	= 0;
		private GameObject 				_player 				= null;
		private Enemy 					_owner 					= null;


	// -- Game init and loops
		void Start() {
			Init();
			_player = GameObject.FindGameObjectWithTag("Player");
			_owner = GetComponent<Enemy>();
		}

		void LateUpdate() {
			if (_owner._distanceToPlayer < _currentMeleeWeapon.GetAttackRange()) {
				AttackTarget(_player, _attackRecoveryOffset);
			}
		}
	}
}

