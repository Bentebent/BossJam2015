using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour 
{
	private float mCountDown = 3.0f;
	public string[] mTankNames = new string[4];

	private bool mIsDead = false;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (mIsDead)
			return;

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

                Main ms = main.GetComponent<Main>();
                
                //ms.WHATEVER = this.gameObject;
				//
                //for (int i = 0; i < 4; i++)
                //{
                //    ms.mIsPlaying.Add(false);
                //    ms.mTank.Add("empty");
                //}

                for (int i = 0; i < 4; ++i)
                {
                    GameObject currShowcase = transform.Find("ShowCase-Player" + (i + 1)).gameObject;
                    showCase sc = currShowcase.GetComponent<showCase>();

                    if (sc.HasSelected)
                    {
                        //ms.mIsPlaying[i] = true;
                        //ms.mTank[i] = sc.SelectedTank;

						mTankNames[i] = sc.SelectedTank;
                    }
                }

				mIsDead = true;
				//Destroy(gameObject);
			}
		}
	}
}
