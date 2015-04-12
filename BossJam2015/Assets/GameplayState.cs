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
    public void Start(string[] tankType, Vector3[] spawnPos)
    {

		if (tankType[0] != "")
        {
			Debug.Log("Player 1 is playing!");
            mPlayerOneGO = GameObject.Instantiate(Resources.Load<GameObject>(tankType[0]));

            mPlayerOneGO.transform.position = spawnPos[0];


            mPlayerOnePC = mPlayerOneGO.GetComponent<PlayerController>();
            mPlayerOnePC.SetPlayerTag("Player1");
            mPlayerOnePC.m_playerName = "One";
        }

		if (tankType[1] != "")
		{
			Debug.Log("Player 2 is playing!");
            mPlayerTwoGO = GameObject.Instantiate(Resources.Load<GameObject>(tankType[1]));

            mPlayerTwoGO.transform.position = spawnPos[1];

            mPlayerTwoPC = mPlayerTwoGO.GetComponent<PlayerController>();
            mPlayerTwoPC.SetPlayerTag("Player2");
            mPlayerTwoPC.m_playerName = "Two";
        }

		if (tankType[2] != "")
		{
			Debug.Log("Player 3 is playing!");
            mPlayerThreeGO = GameObject.Instantiate(Resources.Load<GameObject>(tankType[2]));

            mPlayerThreeGO.transform.position = spawnPos[2];

            mPlayerThreePC = mPlayerThreeGO.GetComponent<PlayerController>();
            mPlayerThreePC.SetPlayerTag("Player3");
            mPlayerThreePC.m_playerName = "Three";
        }

		if (tankType[3] != "")
		{
			Debug.Log("Player 4 is playing!");
            mPlayerFourGO = GameObject.Instantiate(Resources.Load<GameObject>(tankType[3]));

            mPlayerFourGO.transform.position = spawnPos[3];

            mPlayerFourPC = mPlayerFourGO.GetComponent<PlayerController>();
            mPlayerFourPC.SetPlayerTag("Player4");
            mPlayerFourPC.m_playerName = "Four";
        }
      
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
