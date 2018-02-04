using System;
using UnityEngine;

namespace Game.Characters {
    [RequireComponent(typeof (ThirdPersonCharacter))]
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]

    public class AICharacterControl : MonoBehaviour {

        public UnityEngine.AI.NavMeshAgent  _agent       { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter         _character   { get; private set; } // the character we are controlling
        private Transform                   _target      = null;               // target to aim for


        public void SetTarget(Transform target) {
            _target = target;
        }

    // -- Game init and loops

        private void Start() {
            _agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            _agent.updateRotation = false;
	        _agent.updatePosition = true;
            _character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update() {
            if (_target != null)
                _agent.SetDestination(_target.position);

            if (_agent.remainingDistance > _agent.stoppingDistance)
                _character.Move(_agent.desiredVelocity, false, false);
            else
                _character.Move(Vector3.zero, false, false);
        }
    }
}
