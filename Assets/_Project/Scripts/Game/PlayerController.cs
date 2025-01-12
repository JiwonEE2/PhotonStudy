using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private Animator anim;

	private Rigidbody rb;

	// 캐릭터가 쳐다볼 곳
	private Transform pointer;

	// 투사체가 생성될 곳
	private Transform shotPoint;

	private float hp = 100;
	private int shotCount = 0;

	// 이동 속도
	public float moveSpeed;

	// 투사체 발사 파워
	public float shotPower;

	// 체력을 표시할 text
	public Text hpText;

	// 발사 횟수를 표시할 text
	public Text shotText;
	public Bomb bombPrefab;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		pointer = transform.Find("PlayerPointer");
		shotPoint = transform.Find("ShotPoint");
	}

	private void Update()
	{
		Move();
	}

	private void FixedUpdate()
	{
		Rotate();
	}

	private void Move()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		rb.velocity = new Vector3(x, 0, z) * moveSpeed;
	}

	private void Rotate()
	{
		Vector3 pos = rb.position;
		pos.y = 0;
		Vector3 forward = pointer.position - pos;

		rb.rotation = Quaternion.LookRotation(forward, Vector3.up);
	}
}