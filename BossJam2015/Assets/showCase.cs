using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class showCase : MonoBehaviour 
{
	private GameObject mTank;
	private List<GameObject> mTankList;

	private Vector3 mUpDirection = new Vector3(0.0f, 1.0f, 0.0f);
	private float mRotationSpeed = 36.0f;

	// Use this for initialization
	void Start ()
	{
		mTank = transform.Find("tiger_tiger").gameObject;

		mTankList = new List<GameObject>();

		mTankList.Add(Resources.Load<GameObject>("tiger_tiger"));
		mTankList.Add(Resources.Load<GameObject>("tiger_eagle"));
		mTankList.Add(Resources.Load<GameObject>("tiger_dragon"));
		mTankList.Add(Resources.Load<GameObject>("tiger_bear"));
	}
	
	// Update is called once per frame
	void Update ()
	{
		mTank.transform.Rotate(mUpDirection, mRotationSpeed * Time.deltaTime);

		if(Input.GetButtonDown(""))
	}
}
