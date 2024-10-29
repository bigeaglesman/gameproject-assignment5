using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleControler : MonoBehaviour
{
    public int obstacleHP = 5;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bullet")
		{
			obstacleHP--;
		}
		if (obstacleHP <= 0)
			Destroy(gameObject);
	}
}
