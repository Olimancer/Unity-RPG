using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Consider converting into abstract class
namespace Game.Weapons {
   public class Projectile : MonoBehaviour {
      const float                      DESTROY_DELAY     = 0.05f;

      [SerializeField] private float   _projectileDamage = 5f; // Base damage of the projectile. 
      internal float                   _attackDamage     = 0f; // Added damage made by the throw. To be set externally when the projectile is spawn.


   // -- On Events

      void OnCollisionEnter(Collision collision) {
         var damageable = collision.collider.gameObject.GetComponent(typeof(IDamageable));
         if (damageable) {
            (damageable as IDamageable).TakeDamage(_attackDamage + _projectileDamage);
         }
         Destroy(gameObject, DESTROY_DELAY); // Delay to allow trail to briefly appear
      }
   }
}

