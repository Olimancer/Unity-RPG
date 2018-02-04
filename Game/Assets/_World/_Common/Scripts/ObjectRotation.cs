using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.World {
	public class ObjectRotation : MonoBehaviour {

		// To be set in editor
		[SerializeField] float xRotationsPerMinute = 0f;
		[SerializeField] float yRotationsPerMinute = 0f;
		[SerializeField] float zRotationsPerMinute = 0f;
		
		void Update () {
			transform.RotateAround(transform.position, transform.right, (Time.deltaTime / 60 * 360 * xRotationsPerMinute));
			transform.RotateAround(transform.position, transform.up, (Time.deltaTime / 60 * 360 * yRotationsPerMinute));
			transform.RotateAround(transform.position, transform.forward, (Time.deltaTime / 60 * 360 * zRotationsPerMinute));
		}
	}
}

