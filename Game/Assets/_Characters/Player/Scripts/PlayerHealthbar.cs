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
         _player._notifyPlayerDamaged += UpdateHealthbar;
      }
      
   // -- On Events

      private void UpdateHealthbar() {
         _healthbar.value = _player._healthAsPercentage;
      }
   }
}

