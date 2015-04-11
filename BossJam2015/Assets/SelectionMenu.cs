using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionMenu
{
    GameObject mShermanTiger;
    GameObject mShermanDragon;
    GameObject mShermanBear;
    GameObject mShermanEagle;
    GameObject mTigerTiger;
    GameObject mTigerDragon;
    GameObject mTigerBear;
    GameObject mTigerEagle;

  

    GameObject mPlayerOne = null;
    bool mPlayerOneStarted = false;

    GameObject mPlayerTwo = null;
    bool mPlayerTwoStarted = false;

    GameObject mPlayerThree = null;
    bool mPlayerThreeStarted = false;

    GameObject mPlayerFour = null;
    bool mPlayerFourStarted = false;


    string mPOneTank = "tiger_";
    string mPOneAnimal = "tiger";

    string mPTwoTank = "tiger_";
    string mPTwoAnimal = "bear";

    string mPThreeTank = "tiger_";
    string mPThreeAnimal = "dragon";

    string mPFourTank = "tiger_";
    string mPFourAnimal = "eagle";

    Dictionary<string, GameObject> mTanks;

    public SelectionMenu()
    {
        mShermanTiger = Resources.Load<GameObject>("sherman_tiger");
        mShermanDragon = Resources.Load<GameObject>("sherman_dragon");
        mShermanBear = Resources.Load<GameObject>("sherman_bear");
        mShermanEagle = Resources.Load<GameObject>("sherman_eagle");
        mTigerTiger = Resources.Load<GameObject>("tiger_tiger");
        mTigerDragon = Resources.Load<GameObject>("tiger_dragon");
        mTigerBear = Resources.Load<GameObject>("tiger_bear");
        mTigerEagle = Resources.Load<GameObject>("tiger_eagle");

        mTanks = new Dictionary<string, GameObject>();

        mTanks.Add("sherman_tiger", mShermanTiger);
        mTanks.Add("sherman_dragon", mShermanDragon);
        mTanks.Add("sherman_bear", mShermanBear);
        mTanks.Add("sherman_eagle", mShermanEagle);

        mTanks.Add("tiger_tiger", mTigerTiger);
        mTanks.Add("tiger_dragon", mTigerDragon);
        mTanks.Add("tiger_bear", mTigerBear);
        mTanks.Add("tiger_eagle", mTigerEagle);
    }

	// Update is called once per frame
	public void Update () 
    {
        SwitchTank();
        SwitchHead();
        EnterGame();
	}

    private string ST(string tankType)
    {
        if (tankType == "tiger_")
            return "sherman_";
        else
            return "tiger_";
    }

    private string SH(string player)
    {
        if(Input.GetButtonDown("RBumper_Player" + player))
        {
            return "tiger";
        }
        else if (Input.GetButtonDown("LBumper_Player" + player))
        {
            return "dragon";
        }
        else if (Input.GetAxis("LTrigger_Player" + player) < 0)
        {
            return "bear";
        }
        else if (Input.GetAxis("RTrigger_Player" + player) > 0)
        {
            return "eagle";
        }

        return "empty";
    }

    private void SwitchHead()
    {
        if (Input.GetButtonDown("RBumper_PlayerOne") && mPlayerOneStarted)
        {
            mPOneTank = ST(mPOneTank);

            GameObject.DestroyImmediate(mPlayerOne);
            GameObject go;

            mTanks.TryGetValue(mPOneTank + mPOneAnimal, out go);
            mPlayerOne = GameObject.Instantiate(go);
        }

        if (Input.GetButtonDown("RBumper_PlayerTwo") && mPlayerTwoStarted)
        {
            mPTwoTank = ST(mPTwoTank);
            GameObject.DestroyImmediate(mPlayerTwo);

            GameObject go;
            mTanks.TryGetValue(mPTwoTank + mPTwoAnimal, out go);
            mPlayerTwo = GameObject.Instantiate(go);
        }

        if (Input.GetButtonDown("RBumper_PlayerThree") && mPlayerThreeStarted)
        {
            mPThreeTank = ST(mPThreeTank);
            GameObject.DestroyImmediate(mPlayerThree);

            GameObject go;
            mTanks.TryGetValue(mPThreeTank + mPThreeAnimal, out go);
            mPlayerThree = GameObject.Instantiate(go);
        }

        if (Input.GetButtonDown("RBumper_PlayerFour") && mPlayerFourStarted)
        {
            mPFourTank = ST(mPFourTank);
            GameObject.DestroyImmediate(mPlayerFour);

            GameObject go;
            mTanks.TryGetValue(mPFourTank + mPFourAnimal, out go);
            mPlayerFour = GameObject.Instantiate(go);
        }
    }

    private void SwitchTank()
    {
        if (Input.GetButtonDown("Start_PlayerOne") && mPlayerOneStarted)
        {
            mPOneTank = ST(mPOneTank);

            GameObject.DestroyImmediate(mPlayerOne);
            GameObject go;

            mTanks.TryGetValue(mPOneTank + mPOneAnimal, out go);
            mPlayerOne = GameObject.Instantiate(go);
        }

        if (Input.GetButtonDown("Start_PlayerTwo") && mPlayerTwoStarted)
        {
            mPTwoTank = ST(mPTwoTank);
            GameObject.DestroyImmediate(mPlayerTwo);

            GameObject go;
            mTanks.TryGetValue(mPTwoTank + mPTwoAnimal, out go);
            mPlayerTwo = GameObject.Instantiate(go);
        }

        if (Input.GetButtonDown("Start_PlayerThree") && mPlayerThreeStarted)
        {
            mPThreeTank = ST(mPThreeTank);
            GameObject.DestroyImmediate(mPlayerThree);

            GameObject go;
            mTanks.TryGetValue(mPThreeTank + mPThreeAnimal, out go);
            mPlayerThree = GameObject.Instantiate(go);
        }

        if (Input.GetButtonDown("Start_PlayerFour") && mPlayerFourStarted)
        {
            mPFourTank = ST(mPFourTank);
            GameObject.DestroyImmediate(mPlayerFour);

            GameObject go;
            mTanks.TryGetValue(mPFourTank + mPFourAnimal, out go);
            mPlayerFour = GameObject.Instantiate(go);
        }
    }

    private void EnterGame()
    {
        if (Input.GetButtonDown("Start_PlayerOne") && !mPlayerOneStarted)
        {
            GameObject go;
            mTanks.TryGetValue(mPOneTank + mPOneAnimal, out go);
            mPlayerOne = GameObject.Instantiate(go);

            mPlayerOneStarted = true;
        }

        if (Input.GetButtonDown("Start_PlayerTwo") && !mPlayerTwoStarted)
        {
            GameObject go;
            mTanks.TryGetValue(mPTwoTank + mPTwoAnimal, out go);
            mPlayerTwo = GameObject.Instantiate(go);

            mPlayerTwoStarted = true;
        }

        if (Input.GetButtonDown("Start_PlayerThree") && !mPlayerThreeStarted)
        {
            GameObject go;
            mTanks.TryGetValue(mPThreeTank + mPThreeAnimal, out go);
            mPlayerThree = GameObject.Instantiate(go);

            mPlayerThreeStarted = true;
        }

        if (Input.GetButtonDown("Start_PlayerFour") && !mPlayerFourStarted)
        {
            GameObject go;
            mTanks.TryGetValue(mPFourTank + mPFourAnimal, out go);
            mPlayerFour = GameObject.Instantiate(go);

            mPlayerFourStarted = true;
        }
    }
}
