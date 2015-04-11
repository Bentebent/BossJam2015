using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour 
{

	public float speed;
	private Vector3 mDirection;
	private string mParentTag;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		transform.position += mDirection * speed * Time.deltaTime;
	}

	public Vector3 Direction
	{
		get { return mDirection; }
		set { mDirection = value; }
	}

	public string ParentTag
	{
		get { return mParentTag; }
		set { mParentTag = value; }
	}
}
