using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFSM : MonoBehaviour
{
	private GameObject playerBase;
	private GameObject player;
	public float baseAttackDistance;
	public float playerAttackDistance;
	public float moveSpeed;
	private Vector3 randomDirection;
	private float changeDirectionInterval = 2f; // 무작위 방향 변경 주기
	private float directionChangeTimer;
	public GameObject explosionEffectPrefab;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
		playerBase = GameObject.FindWithTag("Base");

		UpdateRandomDirection();
	}

	void Update()
	{
		transform.LookAt(player.transform);
		float distanceToBase = Vector3.Distance(
			transform.position, playerBase.transform.position
		);
		float distanceToPlayer = Vector3.Distance(
			transform.position, player.transform.position
		);

		// 2초마다 무작위 방향을 갱신
		directionChangeTimer += Time.deltaTime;
		if (directionChangeTimer >= changeDirectionInterval)
		{
			UpdateRandomDirection();
			directionChangeTimer = 0f;
		}

		if (distanceToBase < baseAttackDistance)
			bombAttackBase();
		else if (distanceToPlayer < playerAttackDistance)
			bombAttackPlayer();
		else
			shootPlayer();
	}

	void shootPlayer()
	{
		// Player 방향을 바라보도록 회전
		Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

		// 무작위 방향으로 이동
		transform.Translate(randomDirection * moveSpeed * Time.deltaTime, Space.World);
	}

	void UpdateRandomDirection()
	{
		// 무작위 방향을 설정, y축 이동 제거
		randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
	}

	void bombAttackBase()
	{
		// Base 방향으로 이동
		Vector3 direction = (playerBase.transform.position - transform.position).normalized;
		transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

		// Base와의 충돌 확인
		if (Vector3.Distance(transform.position, playerBase.transform.position) < 3f)
		{
			Die();
			if (!player.transform.Find("shield").gameObject.activeSelf)
				SceneManager.LoadScene(1);
		}
	}

	void bombAttackPlayer()
	{
		// Player 방향으로 이동
		Vector3 direction = (player.transform.position - transform.position).normalized;
		transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

		// Player와의 충돌 확인
		if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
		{
			Die();
			if (!player.transform.Find("shield").gameObject.activeSelf)
				SceneManager.LoadScene(1);
		}
	}
    void Die()
    {
        // 폭발 이펙트를 현재 위치에 생성하고 파티클이 재생되도록 함
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

		var renderers = GetComponentsInChildren<Renderer>();
	    foreach (var renderer in renderers)
		{
			renderer.enabled = false; // 모든 렌더러를 비활성화
    	}
		GetComponent<BoxCollider>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;

        // 적 오브젝트 제거
        Destroy(gameObject);
    }

}
