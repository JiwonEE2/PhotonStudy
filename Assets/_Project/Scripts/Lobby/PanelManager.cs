using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {
	public static PanelManager Instance;

	public LoginPanel login;
	public MenuPanel menu;
	public LobbyPanel lobby;
	public RoomPanel room;

	
	Dictionary<string, GameObject> panelDic;

	private void Awake() {
		Instance = this;
		panelDic = new Dictionary<string, GameObject>()
		{
			{ "Login", login.gameObject},
			{ "Menu", menu.gameObject},
			{ "Lobby", lobby.gameObject},
			{ "Room", room.gameObject }
		};

		PanelOpen("Login");
	}

	public void PanelOpen(string panelName) {
		foreach (var row in panelDic) {
			row.Value.SetActive(row.Key == panelName);
		}
	}
}
	

	

