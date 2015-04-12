using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayState
{
    GameObject mPUResource;

    float mPUTimer = 0.0f;

    public GameObject mPlayerOneGO;
    public GameObject mPlayerTwoGO;
    public GameObject mPlayerThreeGO;
    public GameObject mPlayerFourGO;

    public PlayerController mPlayerOnePC;
    public PlayerController mPlayerTwoPC;
    public PlayerController mPlayerThreePC;
    public PlayerController mPlayerFourPC;

    public GameplayState()
    {
        mPUResource = Resources.Load<GameObject>("powerup");
    }

	// Use this for initialization
	public void Start ()
    {
        mPlayerOneGO = GameObject.Instantiate(Resources.Load<GameObject>("sherman_tiger"));
        mPlayerOnePC = mPlayerOneGO.GetComponent<PlayerController>();
        mPlayerOnePC.SetPlayerTag("Player1");
        mPlayerOnePC.m_playerName = "One";

        mPlayerTwoGO = GameObject.Instantiate(Resources.Load<GameObject>("sherman_tiger"));
        mPlayerTwoPC = mPlayerTwoGO.GetComponent<PlayerController>();
        mPlayerTwoPC.SetPlayerTag("Player2");
        mPlayerTwoPC.m_playerName = "Two";

        mPlayerThreeGO = GameObject.Instantiate(Resources.Load<GameObject>("sherman_tiger"));
        mPlayerThreePC = mPlayerThreeGO.GetComponent<PlayerController>();
        mPlayerThreePC.SetPlayerTag("Player3");
        mPlayerThreePC.m_playerName = "Three";

        mPlayerFourGO = GameObject.Instantiate(Resources.Load<GameObject>("sherman_tiger"));
        mPlayerFourPC = mPlayerFourGO.GetComponent<PlayerController>();
        mPlayerFourPC.SetPlayerTag("Player4");
        mPlayerFourPC.m_playerName = "Four";
	}
	
	// Update is called once per frame
	public void Update () 
    {
        for (int i = 0; i < 4; i++ )
        {

        }


        mPUTimer += Time.deltaTime;

        if (mPUTimer >= 2.5f)
        {
            SpawnPU();
            mPUTimer = 0.0f;
        }
       
	}

    public void SpawnPU()
    {
        int chance = Random.Range(0, 1000);

        if (chance <= 750)
        {
            GameObject go = GameObject.Instantiate(mPUResource);
            SpecialWeapon sw = go.GetComponent<SpecialWeapon>();

            int x = Random.Range(-100, 100);
            int y = Random.Range(-100, 100);

            go.transform.position = new Vector3(x, 40, y);

            int wep = Random.Range(0, 3);

            if (wep == 0)
            {
                sw.m_weaponType = SPECIAL_WEAPON.MISSILE;
                sw.ammoCount = 1;
            }
            else if (wep == 1)
            {
                sw.m_weaponType = SPECIAL_WEAPON.MINE;
                sw.ammoCount = 3;
            }
            else if (wep == 2)
            {
                sw.m_weaponType = SPECIAL_WEAPON.SPEED_BOOST;
                sw.ammoCount = 3;
            }
        }
    }
}
