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
        else if (other.tag == "Missile")
        {
            Missile m = other.gameObject.GetComponent<Missile>();

            if (m.m_timer > 1.0f)
            {
                Destroy(gameObject);
            }
           
        }
	}
}
