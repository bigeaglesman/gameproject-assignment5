using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerShooting : MonoBehaviour
{
	public GameObject bullet;
	public GameObject missile;
	public GameObject BFM;
	public int level1Score = 5;
	public int level2Score = 10;
	public GameObject shootPoint1;
	public GameObject shootPoint2;
	public GameObject shootPoint3;
	public ParticleSystem shootingParticlePrefab;
	private int score;
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			score = Scoremanager.instance.killScore;
			Debug.Log(Scoremanager.instance.killScore);
			if (score <= level1Score)
			{
				GameObject clone1 = Instantiate(bullet);
				clone1.transform.position = shootPoint1.transform.position;
				clone1.transform.rotation = shootPoint1.transform.rotation;
				// shootingParticlePrefab.transform.position = shootPoint1.transform.position;
				// shootingParticlePrefab.transform.rotation = shootPoint1.transform.rotation;
				// shootingParticlePrefab.Play();
			}
			else if (score <= level2Score)
			{
				GameObject clone1 = Instantiate(missile);
				clone1.transform.position = shootPoint1.transform.position;
				clone1.transform.rotation = shootPoint1.transform.rotation;
				GameObject clone2 = Instantiate(missile);
				clone2.transform.position = shootPoint2.transform.position;
				clone2.transform.rotation = shootPoint2.transform.rotation;
				GameObject clone3 = Instantiate(missile);
				clone3.transform.position = shootPoint3.transform.position;
				clone3.transform.rotation = shootPoint3.transform.rotation;
			}
			else
			{
				GameObject clone1 = Instantiate(BFM);
				clone1.transform.position = shootPoint1.transform.position;
				clone1.transform.rotation = shootPoint1.transform.rotation;
			}

		}
    }
}
