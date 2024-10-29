using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	public float bulletSpeed;
    void Update()
    {
		transform.Translate(0, 0, bulletSpeed *Time.deltaTime);
    }
}
