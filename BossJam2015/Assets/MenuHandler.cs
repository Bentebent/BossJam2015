using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour 
{
	private float mCountDown = 3.0f;
	public string[] mTankNames = new string[4];

	private GameObject mOne;
	private GameObject mTwo;
	private GameObject mThree;
	private int mShowingNumber = 0;

	private GameObject[] mPlayerNumber = new GameObject[4];

	private bool mIsDead = false;

	// Use this for initialization
	void Start () 
	{
		mOne = Resources.Load<GameObject>("no1");
		mTwo = Resources.Load<GameObject>("no2");
		mThree = Resources.Load<GameObject>("no3");

		for (int i = 0; i < 4; ++i)
		{
			mPlayerNumber[i] = null;
		}
	}

	// Update is called once per frame
	void Update()
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

        if (mShowingNumber != 0)
        {
            GameObject menu_sound = GameObject.Find("menu_theme");
            menu_sound.GetComponent<AudioSource>().volume -= 0.1f * Time.deltaTime;
        }

		if (inGameCount > 0 && inGameCount == selectedCount)
		{
			mCountDown -= Time.deltaTime;

			// Show a 3
			if (mShowingNumber == 0)
			{
				for (int i = 0; i < 4; ++i)
				{
					showCase sc = transform.Find("ShowCase-Player" + (i + 1)).gameObject.GetComponent<showCase>();

					if (sc.HasSelected)
					{
						mPlayerNumber[i] = Instantiate(mThree);
						mPlayerNumber[i].transform.position += new Vector3(6.0f * i, 0.0f, 0.0f);
					}
				}

				mShowingNumber = 3;
			}

			if (mCountDown <= 2.0f && mShowingNumber == 3)
			{
				for (int i = 0; i < 4; ++i)
				{
					if (mPlayerNumber[i] != null)
					{
						Destroy(mPlayerNumber[i]);
						mPlayerNumber[i] = Instantiate(mTwo);
						mPlayerNumber[i].transform.position += new Vector3(6.0f * i, 0.0f, 0.0f);
					}
				}

				mShowingNumber = 2;
			}
			else if (mCountDown <= 1.0f && mShowingNumber == 2)
			{
				for (int i = 0; i < 4; ++i)
				{
					if (mPlayerNumber[i] != null)
					{
						Destroy(mPlayerNumber[i]);
						mPlayerNumber[i] = Instantiate(mOne);
						mPlayerNumber[i].transform.position += new Vector3(6.0f * i, 0.0f, 0.0f);
					}
				}

				mShowingNumber = 1;
			}
			else if (mCountDown <= 0.0f)
			{

                // Start game
				GameObject main = Resources.Load<GameObject>("Main");
                Instantiate(main);

                Main ms = main.GetComponent<Main>();
                

                for (int i = 0; i < 4; ++i)
                {
                    GameObject currShowcase = transform.Find("ShowCase-Player" + (i + 1)).gameObject;
                    showCase sc = currShowcase.GetComponent<showCase>();

                    if (sc.HasSelected)
                    {
						mTankNames[i] = sc.SelectedTank;
                    }

					Destroy(mPlayerNumber[i]);
                }

				mIsDead = true;
			}
		}
	}
}
