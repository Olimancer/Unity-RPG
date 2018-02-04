using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters {
	public class GripSocket : MonoBehaviour {

		// Add to the hands of the character prefab and set the boolean in inspector

		[SerializeField]  private bool _isDominantHand = true;
		
		public bool IsDominantHand() { return _isDominantHand; }
	}
}

