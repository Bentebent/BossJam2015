using UnityEngine;
using System.Collections;

public class SpecialWeapon : MonoBehaviour 
{
    public SPECIAL_WEAPON m_weaponType;
    public int ammoCount;
    bool myChildrenAreDEAD = false;
	// Use this for initialization
	void Start () 
    {
        //m_weaponType = SPECIAL_WEAPON.SPEED_BOOST;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1" 
            || collision.gameObject.tag == "Player2"
            || collision.gameObject.tag == "Player3"
            || collision.gameObject.tag == "Player4")
        {
            PlayerController colliderPC = collision.gameObject.GetComponent<PlayerController>();

            if (colliderPC.m_ammoCount > 0)
                return;

            colliderPC.m_specialWeapon = m_weaponType;
            colliderPC.m_ammoCount = ammoCount;

            GameObject go;

            if (m_weaponType == SPECIAL_WEAPON.MINE)
                go = (GameObject)Instantiate(Resources.Load("MinePS"));
            else if (m_weaponType == SPECIAL_WEAPON.MISSILE)
                go = (GameObject)Instantiate(Resources.Load("MissilePS"));
            else
                go = (GameObject)Instantiate(Resources.Load("SpeedBoostPS"));

            go.transform.position = transform.position + new Vector3(0, 2, 0);

            Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.tag == "Ground" && !myChildrenAreDEAD)
            {
                Destroy(gameObject.transform.FindChild("parachute").gameObject);
                myChildrenAreDEAD = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
