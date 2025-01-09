using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PanelManager : MonoBehaviourPunCallbacks
{
	public static PanelManager Instance;

	public LoginPanel login;
	public MenuPanel menu;
	public LobbyPanel lobby;
	public RoomPanel room;


	Dictionary<string, GameObject> panelDic;

	private void Awake()
	{
		Instance = this;
		panelDic = new Dictionary<string, GameObject>()
		{
			{ "Login", login.gameObject },
			{ "Menu", menu.gameObject },
			{ "Lobby", lobby.gameObject },
			{ "Room", room.gameObject }
		};

		PanelOpen("Login");
	}

	public void PanelOpen(string panelName)
	{
		foreach (var row in panelDic)
		{
			row.Value.SetActive(row.Key == panelName);
		}
	}

	// photon server에 접속되었을 때 호출
	public override void OnConnected()
	{
		PanelOpen("Menu");
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		LogManager.Log($"로그아웃: {cause}");
		PanelOpen("Login");
	}

	// 방을 생성하였을 때 호출
	public override void OnCreatedRoom()
	{
		PanelOpen("Room");
	}

	// 방에서 떠났을 때 호출
	public override void OnLeftRoom()
	{
		PanelOpen("Menu");
	}
}