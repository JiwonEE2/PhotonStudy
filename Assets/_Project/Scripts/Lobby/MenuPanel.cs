using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
	const int MAX_PLAYERS = 8;
	public Text playerName;

	public InputField nicknameInput;
	public Button nicknameUpdateButton;

	[Header("Main Menu")]

	#region Main Menu

	public RectTransform mainMenuPanel;

	public Button createRoomButton;
	public Button findRoomButton;
	public Button randomRoomButton;
	public Button logoutButton;

	#endregion

	[Space(20)] [Header("Create Room Menu")]

	#region Create Room Menu

	public RectTransform createRoomMenuPanel;

	public InputField roomNameInput;
	public InputField playerNumInput;
	public Button createButton;
	public Button cancelButton;

	#endregion

	private void Awake()
	{
		createRoomButton.onClick.AddListener(CreateRoomButtonClick);
		findRoomButton.onClick.AddListener(FindRoomButtonClick);
		randomRoomButton.onClick.AddListener(RandomRoomButtonClick);
		logoutButton.onClick.AddListener(LogoutButtonClick);
		createButton.onClick.AddListener(CreateButtonClick);
		cancelButton.onClick.AddListener(CancelButtonClick);
		nicknameUpdateButton.onClick.AddListener(NicknameUpdateButtonClick);
	}

	private void OnEnable()
	{
		playerName.text = $"안녕하세요, {PhotonNetwork.LocalPlayer.NickName}";
		CancelButtonClick();
	}

	private void CreateRoomButtonClick()
	{
		mainMenuPanel.gameObject.SetActive(false);
		createRoomMenuPanel.gameObject.SetActive(true);
	}

	private void FindRoomButtonClick()
	{
		PhotonNetwork.JoinLobby();
	}

	private void RandomRoomButtonClick()
	{
	}

	private void LogoutButtonClick()
	{
		// 연결 해제
		PhotonNetwork.Disconnect();
	}

	private void CreateButtonClick()
	{
		string roomName = roomNameInput.text;
		if (false == int.TryParse(playerNumInput.text, out int maxPlayer))
		{
			maxPlayer = MAX_PLAYERS;
		}

		if (string.IsNullOrEmpty(roomName))
		{
			roomName = $"Room {Random.Range(0, 1000)}";
		}

		if (maxPlayer <= 0)
		{
			maxPlayer = MAX_PLAYERS;
		}

		RoomOptions option = new RoomOptions
		{
			MaxPlayers = MAX_PLAYERS
		};

		PhotonNetwork.CreateRoom(roomName, option);
	}

	private void CancelButtonClick()
	{
		mainMenuPanel.gameObject.SetActive(true);
		createRoomMenuPanel.gameObject.SetActive(false);
	}

	public void NicknameUpdateButtonClick()
	{
		PhotonNetwork.NickName = nicknameInput.text;
		playerName.text = $"안녕하세요, {PhotonNetwork.NickName}";
	}
}