using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.Enemies {
	[RequireComponent(typeof(NavMeshAgent))]			// [!] Acceleration, Speed and Stopping Distance are handled by the NavMeshAgent, modify values in Editor.
	[RequireComponent(typeof(AICharacterControl))]
	
	public class ChaseOnSight : MonoBehaviour {
		[SerializeField] float			_detectionRange 	= 10f;
		[Tooltip("Offset by which the Enemy will keep pursuit before disengaging (Added to Detection Range)")]
		[SerializeField] float			_pursuitOffset		= 3f;
		[ExecuteInEditMode] void OnValidate() {
			_detectionRange = Mathf.Clamp(_detectionRange, 1f, float.MaxValue);
			_pursuitOffset = Mathf.Clamp(_pursuitOffset, 0f, float.MaxValue);
		}

		private AICharacterControl		_aiController		= null;
		private Enemy					_owner				= null;
//		private Transform 				_origin				= null;

		// Use this for initialization
		void Start () {
			_aiController		= GetComponent<AICharacterControl>();
			_owner				= GetComponent<Enemy>();
		}
		
		// Update is called once per frame
		void Update () {

		}

		void LateUpdate() {
			if ((_owner._distanceToPlayer <= _detectionRange)) {
				_aiController.SetTarget(_owner._player.transform);
			} else if ((_owner._distanceToPlayer >= _detectionRange + _pursuitOffset)) { 
				// TODO: Make enemy return to patrol or guard point
				_aiController.SetTarget(transform);
			}
		}

		void OnDrawGizmos() {
			Gizmos.color = new Color(1f, 1f, 1f, 1f);
			Gizmos.DrawWireSphere(transform.position, _detectionRange);

			Gizmos.color = new Color(0f, 0f, 1f, 1f);
			Gizmos.DrawWireSphere(transform.position, _detectionRange + _pursuitOffset);

			Gizmos.color = new Color(0f, 0f, 0f, 1f);
			Gizmos.DrawWireSphere(transform.position, GetComponent<NavMeshAgent>().stoppingDistance);
		}
	}
}


