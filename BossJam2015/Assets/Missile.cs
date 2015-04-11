using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour 
{
    public GameObject m_target;
    public float m_speed = 20.0f;

    public float m_timer = 0.0f;

	// Use this for initialization
	void Start () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (m_timer > 1.0f)
        {
            Explode();
        }
       
    }

    void Explode()
    {
        // BLOW UP!
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 10.0f);
        Collider thisCollider = gameObject.GetComponent<Collider>();

        for (int i = 0; i < hitColliders.Length; ++i)
        {
            if (hitColliders[i] != thisCollider)
                hitColliders[i].SendMessage("OnTriggerEnter", thisCollider, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (m_timer < 1.0f)
        {
            float step = m_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 1, 0), step);
        }
        else if (m_timer < 4.0f)
        {
            float step = m_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, m_target.transform.position, step);
           
        }
        else
        {
            Explode();
        }

        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, m_target.transform.position, 9001.0f, 0.0f));

        m_timer += Time.deltaTime;
	}
}
