using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class showCase : MonoBehaviour 
{
	public string playerNumberStr;

	private GameObject mSelected;
	private GameObject mTank;
	private List<GameObject> mTankList;
	private List<Texture> mFlagList;
	private int mTankIndex = 0;

	private Vector3 mUpDirection = new Vector3(0.0f, 1.0f, 0.0f);
	private float mRotationSpeed = 36.0f;

	private bool mHasSelected = false;

	private Material mTapestryLeftMat;
	private Material mTapestryRightMat;

	// Use this for initialization
	void Start ()
	{
		GameObject platform = transform.Find("Platform").gameObject;
		mSelected = Resources.Load<GameObject>("SelectedText");

		mTankList = new List<GameObject>();

		mTankList.Add(Resources.Load<GameObject>("tiger_tiger"));
		mTankList.Add(Resources.Load<GameObject>("tiger_eagle"));
		mTankList.Add(Resources.Load<GameObject>("tiger_dragon"));
		mTankList.Add(Resources.Load<GameObject>("tiger_bear"));
		mTankList.Add(Resources.Load<GameObject>("sherman_tiger"));
		mTankList.Add(Resources.Load<GameObject>("sherman_eagle"));
		mTankList.Add(Resources.Load<GameObject>("sherman_dragon"));
		mTankList.Add(Resources.Load<GameObject>("sherman_bear"));

		mFlagList = new List<Texture>();

		mFlagList.Add(Resources.Load<Texture>("Materials/SWE"));
		mFlagList.Add(Resources.Load<Texture>("Materials/USA"));
		mFlagList.Add(Resources.Load<Texture>("Materials/CHI"));
		mFlagList.Add(Resources.Load<Texture>("Materials/RUS"));

		mTank = Instantiate(mTankList[0]);
		mTank.transform.position = platform.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
		mTank.transform.Rotate(mUpDirection, Random.Range(0, 360));

		mTapestryLeftMat = transform.Find("TapestryLeft").gameObject.GetComponent<Renderer>().material;
		mTapestryRightMat = transform.Find("TapestryRight").gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
	{
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
				selectedText.transform.position = transform.position;
				selectedText.transform.position += new Vector3(-2.0f, -0.8f, -1.0f);
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
		mTank.transform.position = tankTransform.position;
		mTank.transform.rotation = tankTransform.rotation;
		mTank.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);


		Destroy(mTank.GetComponent<Rigidbody>());
		Destroy(mTank.GetComponent<PlayerController>());
		Transform spot = mTank.transform.Find("tank_turret").gameObject.transform.Find("spotlight");
			if(spot != null)
				spot.gameObject.GetComponent<Light>().intensity = 0.0f;

		mTapestryLeftMat.SetTexture("_MainTex", mFlagList[mTankIndex % 4]);
		mTapestryRightMat.SetTexture("_MainTex", mFlagList[mTankIndex % 4]);
	}

	public GameObject SelectedTank
	{
		get { return mTank; }
	}
}
