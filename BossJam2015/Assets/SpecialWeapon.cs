using UnityEngine;
using System.Collections;

public class SpecialWeapon : MonoBehaviour 
{
    public SPECIAL_WEAPON m_weaponType;

	// Use this for initialization
	void Start () 
    {
        m_weaponType = SPECIAL_WEAPON.SPEED_BOOST;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1" 
            || collision.gameObject.tag == "Player2"
            || collision.gameObject.tag == "Player3"
            || collision.gameObject.tag == "Player4")
        {
            PlayerController colliderPC = collision.gameObject.GetComponent<PlayerController>();
            colliderPC.m_specialWeapon = m_weaponType;
            colliderPC.m_ammoCount = 3;
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
