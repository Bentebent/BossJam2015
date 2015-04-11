using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		// Hitting anything apart from its parent will destroy the bullet.
		if (other.tag != GetComponent<BulletBehaviour>().ParentTag)
		{
			Destroy(gameObject);
		}
	}
}
