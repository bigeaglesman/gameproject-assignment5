using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bomb : MonoBehaviour
{

	public GameObject explosionEffectPrefab;

	private void OnTriggerEnter(Collider other) {
		Destroy(gameObject);
		if (other.tag=="Player" && !other.transform.Find("shield").gameObject.activeSelf)
		{
			SceneManager.LoadScene(1);
		}
		Die();
	}

	    void Die()
    {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

		var renderers = GetComponentsInChildren<Renderer>();
	    foreach (var renderer in renderers)
		{
			renderer.enabled = false;
    	}
		GetComponent<CapsuleCollider>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;

        Destroy(gameObject);
    }
}
