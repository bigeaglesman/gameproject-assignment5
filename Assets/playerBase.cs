using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBase : MonoBehaviour
{
	public GameObject shield;
	int shieldUse = 0;

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.E) && shieldUse == 0)
		{
			shieldUse = 1;
			StartCoroutine(ActivateShield());
		}
    }

	IEnumerator ActivateShield()
    {
        // 오브젝트 활성화
        shield.SetActive(true);

        // 2초 동안 대기
        yield return new WaitForSeconds(2f);

        // 오브젝트 비활성화
        shield.SetActive(false);
		StartCoroutine(ShieldCooltime());
    }

	IEnumerator ShieldCooltime()
	{
		yield return new WaitForSeconds(5f);

		shieldUse = 0;
	}
}
