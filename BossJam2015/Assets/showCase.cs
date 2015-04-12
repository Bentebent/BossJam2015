using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class showCase : MonoBehaviour 
{
	public string playerNumberStr;

	private GameObject mSelected;
	private GameObject mTank;
	private List<string> mTankFileNames;
	private List<GameObject> mTankList;
	private List<Texture> mFlagList;
	private int mTankIndex = 0;

	private Vector3 mUpDirection = new Vector3(0.0f, 1.0f, 0.0f);
	private float mRotationSpeed = 36.0f;

	private bool mHasSelected = false;
	private bool mEnteredGame = false;

	private Material mTapestryLeftMat;
	private Material mTapestryRightMat;

	// Use this for initialization
	void Start ()
	{
		mSelected = Resources.Load<GameObject>("SelectedText");

		mTankFileNames = new List<string>();

		mTankFileNames.Add("tiger_tiger");
		mTankFileNames.Add("tiger_eagle");
		mTankFileNames.Add("tiger_dragon");
		mTankFileNames.Add("tiger_bear");
		mTankFileNames.Add("sherman_tiger");
		mTankFileNames.Add("sherman_eagle");
		mTankFileNames.Add("sherman_dragon");
		mTankFileNames.Add("sherman_bear");

		mTankList = new List<GameObject>();

		for (int i = 0; i < mTankFileNames.Count; ++i)
		{
			mTankList.Add(Resources.Load<GameObject>(mTankFileNames[i]));
		}

		mFlagList = new List<Texture>();

		mFlagList.Add(Resources.Load<Texture>("Materials/SWE"));
		mFlagList.Add(Resources.Load<Texture>("Materials/USA"));
		mFlagList.Add(Resources.Load<Texture>("Materials/CHI"));
		mFlagList.Add(Resources.Load<Texture>("Materials/RUS"));

		mTapestryLeftMat = transform.Find("TapestryLeft").gameObject.GetComponent<Renderer>().material;
		mTapestryRightMat = transform.Find("TapestryRight").gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!mEnteredGame)
		{
			if (Input.GetButtonDown("Start_Player" + playerNumberStr))
			{
				GameObject platform = transform.Find("Platform").gameObject;

				mTank = Instantiate(mTankList[0]);
				mTank.transform.parent = transform;
				mTank.transform.position = transform.position - new Vector3(0.0f, 1.8f, 0.0f);

				CleanTank();

				mEnteredGame = true;
			}

			return;
		}

		if (!mHasSelected)
		{
			mTank.transform.Rotate(mUpDirection, mRotationSpeed * Time.deltaTime);

			if (Input.GetButtonDown("RBumper_Player" + playerNumberStr))
			{
				ChangeTank();
			}

			if (Input.GetButtonDown("Start_Player" + playerNumberStr))
			{
				GameObject selectedText = Instantiate(mSelected);
				selectedText.transform.position = transform.position + new Vector3(-2.0f, -0.8f, -1.0f);
				selectedText.transform.parent = transform;
				selectedText.transform.rotation = transform.rotation;
				selectedText.transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f), -25.0f);

				mHasSelected = true;

				mTank.transform.rotation = transform.rotation;
				mTank.transform.Rotate(mUpDirection, 180.0f);
			}
		}
		else
		{
		}
	}

	private void ChangeTank()
	{
		mTankIndex = ++mTankIndex % mTankList.Count;
		Transform tankTransform = mTank.transform;

		Destroy(mTank);

		// Create a new tank and remove everything we don't want
		mTank = Instantiate(mTankList[mTankIndex]);
		mTank.transform.parent = transform;
		mTank.transform.position = tankTransform.position;
		mTank.transform.rotation = tankTransform.rotation;

		CleanTank();

		mTapestryLeftMat.SetTexture("_MainTex", mFlagList[mTankIndex % 4]);
		mTapestryRightMat.SetTexture("_MainTex", mFlagList[mTankIndex % 4]);
	}

	private void CleanTank()
	{
		Destroy(mTank.GetComponent<Rigidbody>());
		Destroy(mTank.GetComponent<PlayerController>());
		Destroy(mTank.transform.Find("SmokeTrail1").gameObject);
		Destroy(mTank.transform.Find("SmokeTrail2").gameObject);
		Destroy(mTank.transform.Find("Smoke_modded").gameObject);

		Transform spot = mTank.transform.Find("tank_turret").gameObject.transform.Find("spotlight");
		if (spot != null)
			spot.gameObject.GetComponent<Light>().intensity = 0.0f;
	}

	public bool HasSelected
	{
		get { return mHasSelected; }
	}

	public bool IsInGame
	{
		get { return mEnteredGame; }
	}

	public string SelectedTank
	{
		get { return mTankFileNames[mTankIndex]; }
	}
}
