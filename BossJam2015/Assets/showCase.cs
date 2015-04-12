using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class showCase : MonoBehaviour 
{
	public string playerNumberStr;

	private GameObject mSelected;
	private GameObject mTank;
	private List<GameObject> mTankList;
	private int mTankIndex = 0;

	private Vector3 mUpDirection = new Vector3(0.0f, 1.0f, 0.0f);
	private float mRotationSpeed = 36.0f;

	private bool mHasSelected = false;

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

		foreach (GameObject tank in mTankList)
		{
			Rigidbody tankPhysics = tank.GetComponent<Rigidbody>();
			tankPhysics.useGravity = false;
			tankPhysics.isKinematic = true;

			tank.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

			DestroyImmediate(tank.GetComponent<PlayerController>(), true);

			Transform spot = tank.transform.Find("tank_turret").gameObject.transform.Find("spotlight");
			if(spot != null)
				spot.gameObject.GetComponent<Light>().intensity = 0.0f;
		}

		mTank = Instantiate(mTankList[0]);
		mTank.transform.position = platform.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		mTank.transform.Rotate(mUpDirection, mRotationSpeed * Time.deltaTime);

		if (!mHasSelected)
		{

			if (Input.GetButtonDown("RBumper_Player" + playerNumberStr))
			{
				ChangeTank();
			}

			if (Input.GetButtonDown("Start_Player" + playerNumberStr))
			{
				GameObject selectedText = Instantiate(mSelected);
				selectedText.transform.position = transform.position;
				selectedText.transform.position -= new Vector3(2.0f, 0.0f, 0.0f);
				selectedText.transform.rotation = transform.rotation;
				selectedText.transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f), 25);

				mHasSelected = true;
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
		mTank = Instantiate(mTankList[mTankIndex]);
		mTank.transform.position = tankTransform.position;
		mTank.transform.rotation = tankTransform.rotation;
		mTank.transform.parent = transform;
	}
}
