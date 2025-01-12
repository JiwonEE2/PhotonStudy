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

	private void Start()
	{
		Vector3 spawnPos = playerPositions.GetChild(Random.Range(0, playerPositions
			.childCount)).position;
		PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity)
			.name = PhotonNetwork.NickName;
	}
}