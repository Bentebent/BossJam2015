using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		int width = 50;
		int depth = 50;
		int height = 3;

		GameObject groundPrefab = Resources.Load<GameObject>("GroundTile");
		GameObject worldBottomPrefab = Resources.Load<GameObject>("WorldBottom");
		Material lavaMat = Resources.Load<Material>("Materials/LavaMaterial");
		Vector3 groundScale = groundPrefab.transform.localScale;

		for (int d = -depth / 2 - 1; d < depth / 2; ++d)
		{
			for (int w = -width / 2 - 1; w < width / 2; ++w)
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

		GameObject worldBottom = Instantiate(worldBottomPrefab);
		worldBottom.GetComponent<Renderer>().material.mainTextureScale = new Vector2(width, depth);
		worldBottom.transform.localScale = new Vector3((width + 1) * groundScale.x * 0.1f, 1.0f, (depth + 1) * groundScale.z * 0.1f);
		worldBottom.transform.position = new Vector3(-groundScale.x, groundScale.y * (-height) + (groundScale.y * 0.5f), -groundScale.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
