using UnityEngine;
using System.Collections;

public class GroundCollision : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Projectile")
		{
			Destroy(gameObject);
			Debug.Log("Bullet colliding with ground!");
		}
	}
}
