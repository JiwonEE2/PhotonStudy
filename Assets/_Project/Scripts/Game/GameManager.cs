using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public Transform playerPositions;

	private void Awake()
	{
		Instance = this;
	}

	// photon에서 컨트롤 동기화하는 방법
	// 1. 프리팹에 PhotonView 컴포넌트를 붙이고, PhotonNetwork.Instantiate를 통해 원격 클라이언트들에게도 동기화된 오브젝트를 생성하도록 함
	// 2. PhotonView가 Observing할 수 있도록 View 컴포넌트를 부착
	// 3. 내 View가 부착되지 않은 오브젝트는 내가 제어하지 않도록 예외처리를 반드시 할 것
	private void Start()
	{
		Vector3 spawnPos = playerPositions.GetChild(Random.Range(0, playerPositions
			.childCount)).position;
		PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity)
			.name = PhotonNetwork.NickName;
	}
}