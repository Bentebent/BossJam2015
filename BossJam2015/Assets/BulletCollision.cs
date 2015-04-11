using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
        
		// Hitting anything apart from its parent will destroy the bullet.
        if (other.tag != GetComponent<BulletBehaviour>().ParentTag)
		{
            if (other.gameObject.tag == "Player1"
            || other.gameObject.tag == "Player2"
            || other.gameObject.tag == "Player3"
            || other.gameObject.tag == "Player4")
            {
                if (other.gameObject.GetComponent<PlayerController>() != null)
                {
                    PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            
                    pc.BounceMe();
                    Destroy(gameObject);
                }
            }
		}
	}
}
