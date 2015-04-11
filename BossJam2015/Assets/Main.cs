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
		Vector2 groundScale = new Vector2(groundPrefab.transform.localScale.x, groundPrefab.transform.localScale.z);

		for (int h = -height / 2 + 1; h < height / 2; ++h)
		{
			for (int w = -width / 2 + 1; w < width / 2; ++w)
			{
				GameObject ground = Instantiate(groundPrefab);
				ground.transform.position = new Vector3(w * groundScale.x, 0.0f, h * groundScale.y);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
