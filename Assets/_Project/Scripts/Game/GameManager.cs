using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public Transform playerPositions;
	public static bool isGameReady;

	private void Awake()
	{
		Instance = this;
	}

	// photon에서 컨트롤 동기화하는 방법
	// 1. 프리팹에 PhotonView 컴포넌트를 붙이고, PhotonNetwork.Instantiate를 통해 원격 클라이언트들에게도 동기화된 오브젝트를 생성하도록 함
	// 2. PhotonView가 Observing할 수 있도록 View 컴포넌트를 부착
	// 3. 내 View가 부착되지 않은 오브젝트는 내가 제어하지 않도록 예외처리를 반드시 할 것
	private IEnumerator Start()
	{
		yield return new WaitUntil(() => isGameReady);
		yield return new WaitForSeconds(1f);

		// 랜덤 위치에 플레이어 생성
		// Vector3 spawnPos = playerPositions.GetChild(Random.Range(0, playerPositions
		// 	.childCount)).position;
		// PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity)
		// 	.name = PhotonNetwork.NickName;

		// Player Numbering을 통해 특정 위치에 플레이어 생성
		// GetPlayerNumber 확장메서드 : 포톤 네트워크에 연결된 다른 플레이어들 사이에서 동기화된 플레이어 번호.
		// Actor Number와 다름. (Scene마다 선착순으로 0~플레이어 수만큼 부여됨)
		// GetPlayerNumber 확장메서드가 동작하기 위해서는 Scene에 PlayerNumbering 컴포넌트 필요
		int playerNumber = PhotonNetwork.LocalPlayer.GetPlayerNumber();
		Vector3 playerPos = playerPositions.GetChild(playerNumber).position;
		GameObject playerObj =
			PhotonNetwork.Instantiate("Player", playerPos, Quaternion.identity);
		playerObj.name = $"Player {playerNumber}";

		// 이 밑에서는 내가 MasterClient가 아니면 동작하지 않음
		if (false == PhotonNetwork.IsMasterClient)
		{
			yield break;
		}

		// Master Client만 5초마다 Pill을 PhotonNetwork를 통해 Instantiate
		while (true)
		{
			// PhotonNetwork.Instantiate를 통해 생성할 경우, position과 rotation이 반드시 필요
			Vector3 spawnPos = Random.insideUnitSphere * 15;
			spawnPos.y = 0;
			Quaternion spawnRot = Quaternion.Euler(0, Random.Range(0, 180f), 0);

			// 각 pill마다 random color(Color)와 random healAmount(float)를 주입하고 싶으면?
			// photon에서는 Color 직렬화를 지원하지 않기 때문에 Vector3로 하면 된다.
			Vector3 color = new Vector3(Random.value, Random.value, Random.value);
			float healAmount = Random.Range(10f, 30f);

			PhotonNetwork.Instantiate("Pill", spawnPos, spawnRot,
				data: new object[] { color, healAmount });

			yield return new WaitForSeconds(5f);
		}
	}
}