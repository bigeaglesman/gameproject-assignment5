using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoremanager : MonoBehaviour
{
	public static Scoremanager instance;
	public int killScore;
	public int clearScore;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			print("Duplecated ScoreManager, ignoring this one");
			Destroy(gameObject);
		}
	}
	void Start()
	{
		killScore = 0;
	}

	void Update()
	{
		if (killScore >= clearScore)
			SceneManager.LoadScene(2);
	}
}
