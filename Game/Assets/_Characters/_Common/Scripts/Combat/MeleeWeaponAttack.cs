using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Weapons;

namespace Game.Characters {
   public class MeleeWeaponAttack : MonoBehaviour { // TODO: Create Parent class MeleeAttack

      [SerializeField] protected Weapon      _currentMeleeWeapon  = null;
      protected GameObject                   _mainHand            = null; // Instanciate weapon reference to allow destruction
      protected Animator                     _animator            = null;
      protected AnimatorOverrideController   _animatorOverride    = null;
      protected Transform                    _dominantHand        = null;
   //	private Transform                      _secondaryHand       = null;

      [SerializeField] protected float       _minDamage           = 35f;
      [SerializeField] protected float       _maxDamage           = 50f;
      protected float                        _damageOutput        { get { return Random.Range(_minDamage, _maxDamage); } }

      protected float                        _lastHitTime         = 0f;


   // -- Attack
      // To be triggered by the owner
      public bool IsTargetInRange(GameObject target) {
         return ((transform.position - target.transform.position).magnitude <= _currentMeleeWeapon.GetAttackRange());
      }

      public void AttackTarget(GameObject target, float attackRecoveryOffset = 0f) {
         var damageable = target.GetComponent(typeof(IDamageable));
         bool notOnCooldown = ((Time.time - _lastHitTime) > (_currentMeleeWeapon.GetAttackRecoveryTime() + attackRecoveryOffset));

         if (damageable && notOnCooldown) {
            _lastHitTime = Time.time;
            (damageable as IDamageable).TakeDamage(_damageOutput);
            _animator.SetTrigger("Attack");
         }
      }

   // -- Equip

      public void EquipWeapon(Weapon weapon) {
         if (_mainHand) {
            Destroy(_mainHand.gameObject);
         }
         _currentMeleeWeapon = weapon;
         SpawnWeapon();
      }
   
      // TODO: Add support for off hand
      protected void SpawnWeapon() {
         var weaponPrefab = _currentMeleeWeapon.GetWeaponPrefab();
         if (weaponPrefab) {
            _mainHand = Instantiate(weaponPrefab, _dominantHand);
            _mainHand.transform.localPosition = _currentMeleeWeapon.GetWeaponPrefab().transform.localPosition;
            _mainHand.transform.localRotation = _currentMeleeWeapon.GetWeaponPrefab().transform.localRotation;
         }
         UpdateAttackAnimation();
      }

   // -- Animations

      protected void UpdateAttackAnimation() {
         _animatorOverride["DEFAULT ATTACK"] = _currentMeleeWeapon.GetEventsFreeAttackAnimation(); // TODO: remove const
      }

   // -- Initialisation

      private void InitHands() {
         GripSocket[] hands = GetComponentsInChildren<GripSocket>();
         Assert.IsFalse(hands.Length == 0, ("SocketableHand(s) are not implemented in prefab " + gameObject.ToString()));
         foreach (GripSocket hand in hands) {
            if (hand.IsDominantHand()) {
               Assert.IsNull(_dominantHand, gameObject.ToString() + " has more than 1 dominant SocketableHand, remove or modify one");
               _dominantHand = hand.transform;
            } else {
            //	_secondaryHand = hand.transform;
            }
         }
         Assert.IsNotNull(_dominantHand, (gameObject.ToString() + " has at least 1 SocketableHand but no dominant, make sure to have one"));
      }

      private void InitAnimatorOverride() {
         _animator = GetComponent<Animator>();
         _animatorOverride = new AnimatorOverrideController(_animator.runtimeAnimatorController);
         _animator.runtimeAnimatorController = _animatorOverride;
      }

      protected void Init() {
         InitHands();
         InitAnimatorOverride();
         SpawnWeapon();
      }

   // -- Game init and loops

   void Start() {
      Init();
   }


   // -- Editor

      protected void OnDrawGizmos() {
         Gizmos.color = new Color(255f, 0, 0, 1f);
         Gizmos.DrawWireSphere(transform.position, _currentMeleeWeapon.GetAttackRange());
      }
   }
}

