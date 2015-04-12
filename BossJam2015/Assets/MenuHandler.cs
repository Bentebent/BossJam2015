using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour 
{
	private float mCountDown = 3.0f;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		int inGameCount = 0;
		int selectedCount = 0;

		for (int i = 0; i < 4; ++i)
		{
			GameObject currShowcase = transform.Find("ShowCase-Player" + (i + 1)).gameObject;
			showCase sc = currShowcase.GetComponent<showCase>();

			if (sc.IsInGame)
			{
				inGameCount++;
			}

			if (sc.HasSelected)
				selectedCount++;
		}

		if (inGameCount > 0 && inGameCount == selectedCount)
		{
			mCountDown -= Time.deltaTime;


			if (mCountDown <= 0.0f)
			{
				// Start game
				GameObject main = Resources.Load<GameObject>("Main");
				Instantiate(main);

				Destroy(gameObject);
			}
		}
	}
}
