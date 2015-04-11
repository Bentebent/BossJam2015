using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
	public GameObject groundPrefab;

	// Use this for initialization
	void Start () 
	{
		int width = 100;
		int height = 100;

		for (int h = -height / 2 + 1; h < height / 2; ++h)
		{
			for (int w = -width / 2 + 1; w < width / 2; ++w)
			{
				GameObject ground = Instantiate(groundPrefab);
				ground.transform.position = new Vector3(w, 0.0f, h);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
