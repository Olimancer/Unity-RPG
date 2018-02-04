using UnityEngine;
using UnityEngine.UI;

namespace Game.Characters.Enemies {
	public class EnemyHealthbar : MonoBehaviour {
		private Enemy	_owner;
		private Slider	_healthBar;

// -- Game init and loops

		void Awake() {
			_owner 		= GetComponentInParent<Enemy>();
			_healthBar 	= GetComponentInChildren<Slider>();
		}

		void Start() {
			_owner._notifyDamageTaken += OnOwnerDamaged;
		}

// -- On Events

		private void OnOwnerDamaged() {
			_healthBar.value = _owner._healthAsPercentage;
		}
	}
}

