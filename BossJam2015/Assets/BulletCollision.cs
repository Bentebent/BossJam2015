using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
        BulletBehaviour b = GetComponent<BulletBehaviour>();
		// Hitting anything apart from its parent will destroy the bullet.
        if (other.tag != b.ParentTag)
		{
            if (other.gameObject.tag == "Player1"
            || other.gameObject.tag == "Player2"
            || other.gameObject.tag == "Player3"
            || other.gameObject.tag == "Player4")
            {
                PlayerController pc = other.gameObject.GetComponent<PlayerController>();
                GameObject go = other.gameObject;
                if (pc == null)
                {
                    while (true)
                    {
                        go = go.transform.parent.gameObject;

                        if (go == null)
                            break;
                        else
                            pc = go.GetComponent<PlayerController>();

                        if (pc != null)
                        {
                            pc.BounceMe();
                            Destroy(gameObject);
                            break;
                        }
                    }
                }
            }
            else
                Destroy(gameObject);

            GameObject boom = (GameObject)Instantiate(Resources.Load("explosion_sound"));
            AudioSource aso = boom.GetComponent<AudioSource>();
            Destroy(boom, aso.clip.length);
		}
	}
}
