using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public float speed;
	public float rotationSpeed;
	private Vector2 movementValue;
	private float lookValue;
	public GameObject shield;
	int shieldUse = 0;

	private void Awake()
	{
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	public void OnMove(InputValue value)
	{
		movementValue = value.Get<Vector2>() * speed;
	}
	public void OnLook(InputValue value)
	{
		lookValue = value.Get<Vector2>().x *rotationSpeed;
	}
    void Update()
    {
		transform.Translate(
			movementValue.x *Time.deltaTime,
			0,
			movementValue.y * Time.deltaTime
		);

		transform.Rotate(0,lookValue *Time.deltaTime, 0);
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
