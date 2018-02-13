using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game.Characters.Players {
   public class PlayerManabar : MonoBehaviour {

      private Slider    _manabar  = null;
      private Player    _player   = null;

   // -- Game init and loops

      void Start() {
         _manabar = GetComponentInChildren<Slider>();
         _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
         _player._notifyManaUsed += UpdateManabar;
      }
      
   // -- On Events

      private void UpdateManabar() {
         _manabar.value = _player._manaAsPercentage;
      }
   }
}

