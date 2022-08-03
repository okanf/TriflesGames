using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	
	[SerializeField] GameObject[] Canvases;
	
	private void Awake()
	{
		Instance = this;
		gameStat = GameStat.Start;
	}
	public enum GameStat
	{
		Start,
		play,
		Failed,
		Finish
	}
	
	public GameStat gameStat;
	
	
	void Update()
	{
		if (gameStat == GameStat.Failed)
		{
			Canvases[0].SetActive(true);
		}
		
		if(gameStat == GameStat.Finish)
		{
			Canvases[1].SetActive(true);
		}
		
		if(gameStat == GameStat.play)
		{
			Canvases[2].SetActive(false);
		}
		
	}
}
