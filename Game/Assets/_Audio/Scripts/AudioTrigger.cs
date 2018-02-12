using UnityEngine;

public class AudioTrigger : MonoBehaviour {

   [SerializeField] private AudioClip  _audioclip  = null;

   [Tooltip("Only GameObject belonging to this layer will be able to activate the trigger")]
   [SerializeField] private int     _layerFilter   = Layer.Player; // TODO: Make editor script to show layer name in editor for easier use
   [SerializeField] private float   _triggerRadius = 5f;
   
   [SerializeField] private bool    _playOnlyOnce	= false;
   [SerializeField] private bool    _hasPlayed     = false;
   
   private AudioSource              _audioSource   = null;

// -- Initialisation

   private void CreateAudioSource() {
      _audioSource = gameObject.AddComponent<AudioSource>();
      _audioSource.playOnAwake = false;
      _audioSource.clip = _audioclip;
   }

   private void CreateSphereCollider() {
      SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
      sphereCollider.isTrigger = true;
      sphereCollider.radius = _triggerRadius;
   }

// -- Play audio

   private void RequestPlayAudio() {
      if (_playOnlyOnce && _hasPlayed) {
         return;
      } else if (!_audioSource.isPlaying) {
         _audioSource.Play();
         _hasPlayed = true;
      }
   }

// -- Game init and loops

   void Start() {
      CreateAudioSource();
      CreateSphereCollider();
   }	

// -- On Events

   void OnTriggerEnter(Collider collider) {
   //	if (collider.gameObject.layer == _layerFilter) {
         
   //	}
   }

// -- Editor Only

   void OnDrawGizmos() {
      Gizmos.color = new Color(100f, 200f, 0, 1f);
   	Gizmos.DrawWireSphere(transform.position, _triggerRadius);
   }
}
