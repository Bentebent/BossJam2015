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
			string showCaseName = "ShowCase-Player" + (i + 1);

			GameObject currShowcase = transform.Find(showCaseName).gameObject;
			showCase sc = currShowcase.GetComponent<showCase>();

			if (sc.IsInGame)
			{
				Debug.Log(showCaseName + " is in game! ");
				inGameCount++;
			}

			if (sc.HasSelected)
				selectedCount++;
		}

		Debug.Log("In game: " + inGameCount + "; Ready: " + selectedCount);

		if (inGameCount > 0 && inGameCount == selectedCount)
		{
			mCountDown -= Time.deltaTime;

			Debug.Log("Counting Down!");

			if (mCountDown <= 0.0f)
			{
				Debug.Log("Starting Game!");
				// Start game
				GameObject main = Resources.Load<GameObject>("Main");
				Instantiate(main);

				Destroy(gameObject);
			}
		}
	}
}
