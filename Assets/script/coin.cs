using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
private void OnTriggerEnter(Collider other) {
	if (other.tag == "Player")
	{
		other.GetComponent<PlayerMovement>().speed += 10;
		Destroy(gameObject);
	}

}
}
