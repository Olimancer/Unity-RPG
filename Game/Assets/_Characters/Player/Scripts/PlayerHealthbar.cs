using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game.Characters.Players {
   public class PlayerHealthbar : MonoBehaviour {

      private Slider    _healthbar  = null;
      private Player    _player     = null;

// -- Game init and loops

      void Start() {
         _healthbar = GetComponentInChildren<Slider>();
         _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
         _player._notifyPlayerDamaged += OnPlayerDamaged;
      }
      
// -- On Events

      private void OnPlayerDamaged() {
         _healthbar.value = _player._healthAsPercentage;
      }
   }
}

