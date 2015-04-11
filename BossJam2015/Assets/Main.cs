using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
	public GameObject groundPrefab;

	// Use this for initialization
	void Start () 
	{
		int width = 50;
		int depth = 50;
		int height = 3;

		Material lavaMat = Resources.Load<Material>("Materials/LavaMaterial");
		Vector3 groundScale = groundPrefab.transform.localScale;

		for (int d = -depth / 2 + 1; d < depth / 2; ++d)
		{
			for (int w = -width / 2 + 1; w < width / 2; ++w)
			{
				for (int h = 0; h > -height; --h)
				{
					GameObject ground = Instantiate(groundPrefab);
					ground.transform.position = new Vector3(w * groundScale.x, h * groundScale.y, d * groundScale.z);

					if (h < -1)
						ground.GetComponent<Renderer>().material = lavaMat;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
