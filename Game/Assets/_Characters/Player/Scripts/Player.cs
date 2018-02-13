using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cameras;
using Game.Weapons;

namespace Game.Characters {
   public class Player : Character, IDamageable {

      // [SerializeField] private float      _meleeAttackRadius   = 2.15f;

      [SerializeField] private float      _maxHealth           = 250;
      private float                       _currentHealth;
      public float                        _health              { get { return _currentHealth; } }
      public float                        _healthAsPercentage  { get { return (_currentHealth / _maxHealth); } }

      [SerializeField] private float      _maxMana             = 100;
      private float                       _currentMana;
      public float                        _mana                { get { return _currentMana; } }
      public float                        _manaAsPercentage    { get { return (_currentMana / _maxMana); } }

      // TODO: Consider other methods of notification when player data changes.
      public delegate void OnPlayerDamaged();
      public event OnPlayerDamaged _notifyPlayerDamaged;

      public delegate void OnManaUsed();
      public event OnManaUsed _notifyManaUsed;

   // -- Game init and loops

      void Start() {
         _currentHealth = _maxHealth;
         _currentMana = _maxMana;
      }

      void Update() {
         if(Input.GetKeyDown(KeyCode.F)) {
            _currentMana -= 5;
            _notifyManaUsed();
         }
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

