﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour 
{
	public const int mWorldWidthBlocks = 30;
	public const int mWorldDepthBlocks = 20;
	public const int mWorldHeightBlocks = 2;

	private Vector2 mWorldHalfWidth;
	private Vector3[] mSpawnPositions = new Vector3[4];

    SelectionMenu mSelectionMenu;
    GameplayState mGameplayState;

    bool mSpawningPlayers;
    bool mGameplay;

    public List<bool> mIsPlaying = new List<bool>();
    public List<string> mTank = new List<string>();
	public List<string> mMapNames = new List<string>();

    public GameObject WHATEVER;

    public AudioSource[] mAudioSources;

    int song;

	// Use this for initialization
	void Start () 
	{
		mMapNames.Add("Maps/map_one");
		mMapNames.Add("Maps/map_two");
		mMapNames.Add("Maps/map_three");

        mSelectionMenu = new SelectionMenu();
        mGameplayState = new GameplayState();

		SetupWorld();
		SetupPlayerSpawns();

        mSpawningPlayers = false;
        mGameplay = true;

        WHATEVER = GameObject.FindGameObjectWithTag("MenuScene");
        MenuHandler mh = WHATEVER.GetComponent<MenuHandler>();

		mGameplayState.Start(mh.mTankNames, mSpawnPositions);
		Destroy(WHATEVER);

        GameObject musicGO = (GameObject)Instantiate(Resources.Load("gameplay_music"));
        mAudioSources = musicGO.GetComponents<AudioSource>();

        song = Random.Range(0, 3);

        mAudioSources[song].Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (!mAudioSources[song].isPlaying)
        {
            song = Random.Range(0, 3);
            mAudioSources[song].Play();
        }


        if (mSpawningPlayers)
        {
            mSelectionMenu.Update();
        }
        else if (mGameplay)
        {
            mGameplayState.Update();
        }
		// Keep time.
	}

	private void SetupWorld()
	{
		GameObject groundPrefab = Resources.Load<GameObject>("GroundTile");
		GameObject worldBottomPrefab = Resources.Load<GameObject>("WorldBottom");
		Material lavaMat = Resources.Load<Material>("Materials/LavaMaterial");
		Vector3 groundScale = groundPrefab.transform.localScale;

		for (int d = -mWorldDepthBlocks / 2; d <= mWorldDepthBlocks / 2; ++d)
		{
			for (int w = -mWorldWidthBlocks / 2; w <= mWorldWidthBlocks / 2; ++w)
			{
				for (int h = 0; h > -mWorldHeightBlocks; --h)
				{
					GameObject ground = Instantiate(groundPrefab);
					ground.transform.position = new Vector3(w * groundScale.x, h * groundScale.y, d * groundScale.z);

					if (h < -1)
						ground.GetComponent<Renderer>().material = lavaMat;
				}
			}
		}

		GameObject worldBottom = Instantiate(worldBottomPrefab);
		worldBottom.GetComponent<Renderer>().material.mainTextureScale = new Vector2(mWorldWidthBlocks, mWorldDepthBlocks);
		worldBottom.transform.localScale = new Vector3((mWorldWidthBlocks + 1) * groundScale.x * 0.1f, 1.0f, (mWorldDepthBlocks + 1) * groundScale.z * 0.1f);
		worldBottom.transform.position = new Vector3(0.0f, groundScale.y * (-mWorldHeightBlocks) + (groundScale.y * 0.5f), 0.0f);

		mWorldHalfWidth = new Vector2((mWorldWidthBlocks - 1) * groundScale.x * 0.5f, (mWorldDepthBlocks - 1) * groundScale.z * 0.5f);

		int randomMap = Random.Range(0, mMapNames.Count - 1);
        GameObject map_one = (GameObject)Instantiate(Resources.Load(mMapNames[randomMap]));
	}

	private void SetupPlayerSpawns()
	{
		GameObject spawnPrefab = Resources.Load<GameObject>("9x9Platform-Dirt");

		for (int i = 0; i < 4; ++i)
		{
			int xModifier = i % 2 == 0 ? -1 : 1;
			int yModifier = i < 2 ? -1 : 1;
			
			mSpawnPositions[i] = new Vector3(mWorldHalfWidth.x * xModifier, 10.0f, mWorldHalfWidth.y * yModifier);
			GameObject spawnPoint = Instantiate(spawnPrefab);
			spawnPoint.transform.position = mSpawnPositions[i];
		}
	}

	private void SpawnPlayers()
	{
	}
}
