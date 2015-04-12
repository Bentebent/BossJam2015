using UnityEngine;
using System.Collections;

public class PSAutoDestroy : MonoBehaviour 
{
	// Use this for initialization
	 private void Start()
     {
         if (GetComponent<ParticleSystem>() != null)
             Destroy(gameObject, GetComponent<ParticleSystem>().duration);
         else
             Destroy(gameObject, 20.0f);
     }
	
	// Update is called once per frame
	void Update () {
	
	}
}