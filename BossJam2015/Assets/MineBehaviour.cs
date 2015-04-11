using UnityEngine;
using System.Collections;

public class MineBehaviour : MonoBehaviour {
	public Material mWarningMaterial;

	// Use this for initialization
	void Start () 
	{
		//mWarningMaterial = Resources.Load<Material>("Assets/Materials/MineWarningMaterial");
		gameObject.GetComponent<Renderer>().material = mWarningMaterial;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
