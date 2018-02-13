using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons {
   [CreateAssetMenu(menuName = ("RPG/Weapon"))]

   public class Weapon : ScriptableObject {
      [SerializeField] private GameObject          _weaponPrefab        = null;
      [SerializeField] private AnimationClip       _attackAnimation     = null;
      [SerializeField] private float               _attackRange         = 1f;
      [SerializeField] private float               _attackRecoveryTime  = 1f; // TODO: Get from animation time.
      // [SerializeField] private bool                _isTwoHanded         = false; // TODO: Complete 2 handed support

      public GameObject    GetWeaponPrefab()       { return _weaponPrefab; }
      public float         GetAttackRange()        { return _attackRange; }
      public float         GetAttackRecoveryTime() { return _attackRecoveryTime; }
      // public bool          IsTwoHanded()           { return _isTwoHanded; }
      public AnimationClip GetAttackAnimation()    { return _attackAnimation; }
      public AnimationClip GetEventsFreeAttackAnimation() { 
         RemoveAnimationEvents(); 
         return _attackAnimation; 
      }

      // Strip the animation of events to avoid conflicts from the imported package
      void RemoveAnimationEvents() {
         _attackAnimation.events = new AnimationEvent[0];
      }
   }
}

