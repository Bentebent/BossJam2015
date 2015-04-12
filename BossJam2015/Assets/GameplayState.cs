using UnityEngine;
using UnityEngine.UI;
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

    public bool m_first = true;
    public float m_timer;

    Text mP1Score;
    Text mP2Score;
    Text mP3Score;
    Text mP4Score;

    Text mP1Win;
    Text mP2Win;
    Text mP3Win;
    Text mP4Win;

    public GameplayState()
    {
        mPUResource = Resources.Load<GameObject>("powerup");
    }

    static public GameObject GetChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName)
                return t.gameObject;
        return null;
    }

	// Use this for initialization
    public void Start(string[] tankType, Vector3[] spawnPos)
    {
        GameObject canvas = (GameObject)GameObject.Instantiate(Resources.Load("GUI"));

        
		if (tankType[0] != "")
        {
			Debug.Log("Player 1 is playing!");
            mPlayerOneGO = GameObject.Instantiate(Resources.Load<GameObject>(tankType[0]));

            mPlayerOneGO.transform.position = spawnPos[0];


            mPlayerOnePC = mPlayerOneGO.GetComponent<PlayerController>();
            mPlayerOnePC.SetPlayerTag("Player1");
            mPlayerOnePC.m_playerName = "One";

            GameObject go = GetChildGameObject(canvas, "p1Score");
            mP1Score = go.GetComponent<Text>();

            go = GetChildGameObject(canvas, "p1Win");
            mP1Win = go.GetComponent<Text>();
        }

		if (tankType[1] != "")
		{
			Debug.Log("Player 2 is playing!");
            mPlayerTwoGO = GameObject.Instantiate(Resources.Load<GameObject>(tankType[1]));

            mPlayerTwoGO.transform.position = spawnPos[1];

            mPlayerTwoPC = mPlayerTwoGO.GetComponent<PlayerController>();
            mPlayerTwoPC.SetPlayerTag("Player2");
            mPlayerTwoPC.m_playerName = "Two";

            GameObject go = GetChildGameObject(canvas, "p2Score");
            mP2Score = go.GetComponent<Text>();

            go = GetChildGameObject(canvas, "p2Win");
            mP2Win = go.GetComponent<Text>();
        }

		if (tankType[2] != "")
		{
			Debug.Log("Player 3 is playing!");
            mPlayerThreeGO = GameObject.Instantiate(Resources.Load<GameObject>(tankType[2]));

            mPlayerThreeGO.transform.position = spawnPos[2];

            mPlayerThreePC = mPlayerThreeGO.GetComponent<PlayerController>();
            mPlayerThreePC.SetPlayerTag("Player3");
            mPlayerThreePC.m_playerName = "Three";

            GameObject go = GetChildGameObject(canvas, "p3Score");
            mP3Score = go.GetComponent<Text>();

            go = GetChildGameObject(canvas, "p3Win");
            mP3Win = go.GetComponent<Text>();
        }

		if (tankType[3] != "")
		{
			Debug.Log("Player 4 is playing!");
            mPlayerFourGO = GameObject.Instantiate(Resources.Load<GameObject>(tankType[3]));

            mPlayerFourGO.transform.position = spawnPos[3];

            mPlayerFourPC = mPlayerFourGO.GetComponent<PlayerController>();
            mPlayerFourPC.SetPlayerTag("Player4");
            mPlayerFourPC.m_playerName = "Four";

            GameObject go = GetChildGameObject(canvas, "p4Score");
            mP4Score = go.GetComponent<Text>();

            go = GetChildGameObject(canvas, "p4Win");
            mP4Win = go.GetComponent<Text>();
        }
      
	}
	
	// Update is called once per frame
	public void Update () 
    {
        if (m_first)
        {
            m_first = false;

            m_timer = 90.0f;
        }

        if (m_timer < 0.0f)
        {
            EndShit();

            //End condition
        }

        mPUTimer += Time.deltaTime;

        if (mPUTimer >= 2.5f)
        {
            SpawnPU();
            mPUTimer = 0.0f;
        }

        m_timer -= Time.deltaTime;

        if (mP1Score != null)
            mP1Score.text = "Score: " + mPlayerOnePC.m_score;

        if (mP2Score != null)
            mP2Score.text = "Score: " + mPlayerTwoPC.m_score;

        if (mP3Score != null)
            mP3Score.text = "Score: " + mPlayerThreePC.m_score;

        if (mP4Score != null)
            mP4Score.text = "Score: " + mPlayerFourPC.m_score;
       
	}

    private void EndShit()
    {
        int score = 1;

        bool win1 = false;
        bool win2 = false;
        bool win3 = false;
        bool win4 = false;

        if (mPlayerOnePC != null)
        {
            score = mPlayerOnePC.m_score;
            win1 = true;
        }

        if (mPlayerTwoPC != null)
        {
            if (mPlayerTwoPC.m_score > score)
            {
                score = mPlayerTwoPC.m_score;
                win1 = false;
                win2 = true;
            }
            else if (mPlayerTwoPC.m_score == score)
            {
                win2 = true;
            }
        }

        if (mPlayerThreePC != null)
        {
            if (mPlayerThreePC.m_score > score)
            {
                score = mPlayerThreePC.m_score;
                win1 = false;
                win2 = false;
                win3 = true;
            }
            else if (mPlayerThreePC.m_score == score)
            {
                win3 = true;
            }

        }

        if (mPlayerFourPC != null)
        {
            if (mPlayerFourPC.m_score > score)
            {
                score = mPlayerThreePC.m_score;
                win1 = false;
                win2 = false;
                win3 = false;
                win4 = true;
            }
            else if (mPlayerThreePC.m_score == score)
            {
                win4 = true;
            }
        }

        int winners = 0;

        string msg = "wins";

        if (win1)
            winners++;
        if (win2)
            winners++;
        if (win3)
            winners++;
        if (win4)
            winners++;

        if (winners > 1)
            msg = "was tied for the win";

        if (win1)
            mP1Win.text = "Player 1 " + msg;
        if (win2)
            mP2Win.text = "Player 2 " + msg;
        if (win3)
            mP3Win.text = "Player 3 " + msg;
        if (win4)
            mP4Win.text = "Player 4 " + msg;

        if (Input.GetButtonDown("Start_PlayerOne") || Input.GetButtonDown("Start_PlayerTwo") || Input.GetButtonDown("Start_PlayerThree") ||Input.GetButtonDown("Start_PlayerFour"))
        {
            DestroyAllGameObjects();
            GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("MenuScene"));
        }
    }

    public void DestroyAllGameObjects()
    {
        GameObject[] GameObjects = (GameObject.FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)
        {
            GameObject.Destroy(GameObjects[i]);
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
