using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
	public bool isTestMode = false;

	private void Start()
	{
		isTestMode = PhotonNetwork.IsConnected == false;
		if (isTestMode)
		{
			PhotonNetwork.ConnectUsingSettings();
		}
		else
		{
			GameManager.isGameReady = true;
		}
	}

	public override void OnConnectedToMaster()
	{
		if (isTestMode)
		{
			RoomOptions option = new()
			{
				IsVisible = false,
				MaxPlayers = 8
			};
			PhotonNetwork.JoinOrCreateRoom("TestRoom", option, TypedLobby.Default);
		}
	}

	public override void OnJoinedRoom()
	{
		if (isTestMode)
		{
			GameObject.Find("Canvas/DebugText").GetComponent<Text>().text =
				PhotonNetwork.CurrentRoom.Name;
			GameManager.isGameReady = true;
		}
	}
}