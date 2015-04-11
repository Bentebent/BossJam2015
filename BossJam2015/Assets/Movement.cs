using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour 
{
    public GameObject bullet;

    private bool mWasUpPressed = false;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        bool isUpPressed = Input.GetKey(KeyCode.UpArrow);

        if (isUpPressed && !mWasUpPressed)
        {
            // Shoot
            GameObject newBullet = Instantiate(bullet);

            Transform child = transform.Find("Turret");
			if (child != null)
			{
				BulletBehaviour bulletInfo = newBullet.GetComponent<BulletBehaviour>();
				bulletInfo.Direction = new Vector3(1.0f, 0.0f, 0.0f);
				bulletInfo.ParentTag = gameObject.tag;
				newBullet.transform.position = child.position;
			}
			else
				Debug.Log("Child not found!");
        }

        mWasUpPressed = isUpPressed;
	}
}
