using UnityEngine;
using Game.Weapons;

namespace Game.Characters.Enemies {
   [RequireComponent(typeof(Enemy))]
   
   public class EnemyRangeAttack : MonoBehaviour {

      [SerializeField] bool         _canDamageAlly          = false;
      [SerializeField] float        _attackRange            = 10;
      [SerializeField] GameObject   _projectileToUse        = null; // TODO: Move to weapon
      
      [Tooltip("GameObject from which the projectile will spawn.")]
      [SerializeField] GameObject   _projectileSpawnPoint   = null;

      [SerializeField] float        _minDamage              = 5f;
      [SerializeField] float        _maxDamage              = 15f;
      [SerializeField] float        _projectileSpeed        = 15f;
      
      [Tooltip("Interval, in seconds, at which the projectile will spawn.")]
      [SerializeField] float        _shootingInterval       = 0.5f;
      private bool                  _isShooting             = false;

      private float                 _damageOutput           { get { return Random.Range(_minDamage, _maxDamage); } }

      private Enemy                 _owner                  = null;
      private ThirdPersonCharacter  _3rdPersonCharacter     = null;
      private Vector3               _aimOffset;

      [ExecuteInEditMode] void OnValidate() {
         _attackRange      = Mathf.Clamp(_attackRange, 2f, float.MaxValue);
         _minDamage        = Mathf.Clamp(_minDamage, 1f, float.MaxValue);
         _maxDamage        = Mathf.Clamp(_maxDamage, _minDamage, float.MaxValue);
         _projectileSpeed  = Mathf.Clamp(_projectileSpeed, 0f, float.MaxValue);
         _shootingInterval = Mathf.Clamp(_shootingInterval, 0f, float.MaxValue);
      }

      Projectile SpawnProjectile() {
         Projectile projectile = Instantiate(_projectileToUse, _projectileSpawnPoint.transform.position, Quaternion.identity).GetComponent<Projectile>();
         projectile._attackDamage = _damageOutput;
         if (!_canDamageAlly) { projectile.gameObject.layer = Layer.EnemyAttack; } // Place the projectile on the EnemyAttack layer to avoid collision with allies.
         return projectile;	
      }

      void FireProjectile() {
         Projectile projectile = SpawnProjectile();
         Vector3 unitVectorToPlayer = ((_owner._player.transform.position + _aimOffset) - _projectileSpawnPoint.transform.position).normalized;
         projectile.GetComponentInParent<Rigidbody>().velocity = (unitVectorToPlayer * _projectileSpeed);
      }

      // TODO: Animate enemy while he rotates
      void LookAtPlayer() {
         var rotation = Quaternion.LookRotation(_owner._player.transform.position - transform.position);
         transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _3rdPersonCharacter.m_StationaryTurnSpeed);
      }

      void ShootPlayer() {
         // TODO: Add and trigger animation
         if (!_isShooting) {
            InvokeRepeating("FireProjectile", 0.5f, _shootingInterval);
            _isShooting = true;
         }
      }

      void StopShooting() {
         CancelInvoke();
         _isShooting = false;
      }

   // -- Game loops
      
      void Start() {
         _owner = GetComponent<Enemy>();
         _aimOffset = _owner._player.GetComponent<CapsuleCollider>().center;
         _3rdPersonCharacter  = GetComponent<ThirdPersonCharacter>();
      }

      void LateUpdate() {
         bool playerInAttackRange      = _owner._distanceToPlayer <= _attackRange;
         bool playerOutOfAttackRange   = _owner._distanceToPlayer > _attackRange;
         bool playerAtMeleeRange       = _owner._distanceToPlayer <= _owner._maxMeleeRange;

         if (playerOutOfAttackRange || playerAtMeleeRange) {
            if (_isShooting) {
               StopShooting();
            }
            return;
         }
         if (playerInAttackRange) {
            LookAtPlayer();
            if (!_isShooting) {
               ShootPlayer();
            }
         }
      }

   // -- Editor Only

      void OnDrawGizmos() {
         Gizmos.color = new Color(0f, 1f, 1f, 1f);
         Gizmos.DrawWireSphere(transform.position, _attackRange);
      }
   }
}
