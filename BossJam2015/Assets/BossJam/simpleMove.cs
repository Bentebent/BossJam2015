using UnityEngine;
using System.Collections;

public class simpleMove : MonoBehaviour {
	
	private Vector3	startPos;
	private float	tmp;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		tmp += Time.deltaTime * 40.0f;
		//transform.position = startPos + new Vector3 (Mathf.Cos(tmp), 0.0f, Mathf.Sin(tmp));
		transform.position = startPos + new Vector3 (0.0f, 0.0f, -tmp );
		if(tmp > 100.0f)
			tmp = 0.0f;
	}
}
