using UnityEngine;
using System.Collections;

public class GroundCollision : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Projectile")
		{
			Destroy(gameObject);
		}
		else if (other.tag == "Mine")
		{
			Destroy(gameObject);
		}
	}
}
