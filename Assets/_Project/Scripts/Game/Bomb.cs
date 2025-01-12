using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public Rigidbody rb;
	public ParticleSystem particlePrefab;
	public Player owner;

	private void OnTriggerEnter(Collider other)
	{
		// 나중에 Object Pool 사용하면 좋다.
		ParticleSystem particle =
			Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
		particle.Play();
		Destroy(particle.gameObject, 3);

		// 충돌 즉시 renderer와 collider 비활성화하여 실질적인 역할을 하지 않도록
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;

		Destroy(gameObject, 0.1f);

		// 폭발이 일어나면 조금 더 큰 범위의 원 내에 있는 모든 콜라이더에 데미지
		Collider[] contactedColliders = Physics.OverlapSphere(transform.position, 1.5f);

		foreach (Collider collider in contactedColliders)
		{
			if (collider.tag == "Player")
			{
				collider.SendMessage("Hit", 1);
				PhotonView target = collider.GetComponent<PhotonView>();
				print($"{owner.NickName}이 던진 폭탄에 {target.Owner.NickName}가 맞음");
			}
		}
	}

	private void Reset()
	{
		rb = GetComponent<Rigidbody>();
		particlePrefab = Resources.Load<ParticleSystem>("BombParticle");
	}
}