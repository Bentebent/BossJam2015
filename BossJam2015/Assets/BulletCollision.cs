using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
		Debug.Log("Entering object!");
	}
}
