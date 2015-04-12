using UnityEngine;
using System.Collections;

public class MineBehaviour : MonoBehaviour {
	private Material mWarningMaterial;
	private Material mNormalMaterial;

	private Renderer mRenderer;
	
	private float mBlinkDuration = 0.0f;
	private const float mBlinkInterval = 0.3f;
	private bool mBlinkIsNormal = true;
	private bool mExplodesSoon = false;

	private float mTimer = 5.0f;

	// Use this for initialization
	void Start () 
	{
		mRenderer = gameObject.GetComponent<Renderer>();

		mWarningMaterial = Resources.Load<Material>("Materials/MineWarningMaterial");
		mNormalMaterial = mRenderer.material;
	}
	
	// Update is called once per frame
	void Update () 
	{
		mTimer -= Time.deltaTime;

		if(mExplodesSoon)
			mBlinkDuration += Time.deltaTime;

		if (mBlinkDuration > mBlinkInterval)
		{
			if (mBlinkIsNormal)
				mRenderer.material = mWarningMaterial;
			else
				mRenderer.material = mNormalMaterial;

			mBlinkIsNormal = !mBlinkIsNormal;
			mBlinkDuration = 0.0f;
		}

		
		if (mTimer < 0.0f)
		{
			// BLOW UP!
			Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 10.0f);
			Collider thisCollider = gameObject.GetComponent<Collider>();

            //tag = "ExplodingMine";

			for (int i = 0; i < hitColliders.Length; ++i)
			{
				if (hitColliders[i] != thisCollider)
					hitColliders[i].SendMessage("OnTriggerEnter", thisCollider, SendMessageOptions.DontRequireReceiver);

                GameObject go = (GameObject)Instantiate(Resources.Load("Nuke"));
                go.transform.position = transform.position;

				Destroy(gameObject);
			}
		}
		else if (mTimer < 3.0f)
			mExplodesSoon = true;
	}
}
