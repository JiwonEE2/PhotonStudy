using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
// System.Collections와 모호한 참조 발생 가능
using Hashtable = ExitGames.Client.Photon.Hashtable;

public enum Difficulty
{
	Easy = 0,
	Normal,
	Hard,
}

public class RoomPanel : MonoBehaviour
{
	public Text roomTitleText;

	public Difficulty roomDifficulty;

	public Dropdown difficultyDropdown;

	public Text difficultyText;

	public RectTransform playerList;
	public GameObject playerTextPrefab;

	public Dictionary<int, PlayerEntry> playerListDic =
		new Dictionary<int, PlayerEntry>();

	public Button startButton;
	public Button cancelButton;

	private Dictionary<int, bool> playersReady;

	private void Awake()
	{
		startButton.onClick.AddListener(StartButtonClick);
		cancelButton.onClick.AddListener(CancelButtonClick);
		difficultyDropdown.ClearOptions();
		foreach (object difficulty in Enum.GetValues(typeof(Difficulty)))
		{
			Dropdown.OptionData option =
				new Dropdown.OptionData(difficulty.ToString());
			difficultyDropdown.options.Add(option);
		}

		difficultyDropdown.onValueChanged.AddListener(DifficultyValueChange);
	}

	private void OnEnable()
	{
		// 유효성 검사(방에 입장한 상태인가?==InRoom)
		if (false == PhotonNetwork.InRoom) return;

		roomTitleText.text = PhotonNetwork.CurrentRoom.Name;

		// photon의 Player 클래스 가져오기
		foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
		{
			// 플레이어 정보 객체 생성
			JoinPlayer(player);
		}

		// 방장인 지 여부를 확인하여 활성 비활성
		difficultyDropdown.gameObject.SetActive(PhotonNetwork.IsMasterClient);
		startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
		// host 와 guest 씬 동기화. 어디서 선언하든 상관 없음
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	private void OnDisable()
	{
		// 플레이어 리스트에 다른 객체가 있으면 일단 모두 삭제
		foreach (Transform child in playerList)
		{
			Destroy(child.gameObject);
		}
	}

	public void JoinPlayer(Player newPlayer)
	{
		PlayerEntry playerEntry = Instantiate(playerTextPrefab, playerList, false)
			.GetComponent<PlayerEntry>();
		playerEntry.playerNameText.text = newPlayer.NickName;
		playerEntry.player = newPlayer;
		// LocalPlayer : 현재 플레이어
		// 현재 클라이언트 플레이어와 새 플레이어가 다를 때. 새로운 플레이어가 입장했을 때
		// 내 엔트리에만 레디토글 활성화. 다른사람거는 비활성화하겠다는 의미
		if (PhotonNetwork.LocalPlayer.ActorNumber != newPlayer.ActorNumber)
		{
			playerEntry.readyToggle.gameObject.SetActive(false);
		}
	}

	public void LeavePlayer(Player gonePlayer)
	{
		foreach (Transform child in playerList)
		{
			Player player = child.GetComponent<PlayerEntry>().player;
			if (player.ActorNumber == gonePlayer.ActorNumber)
			{
				Destroy(child.gameObject);
			}
		}
	}

	private void CancelButtonClick()
	{
		PhotonNetwork.LeaveRoom();
	}

	private void StartButtonClick()
	{
		PhotonNetwork.LoadLevel("GameScene");
	}

	private void DifficultyValueChange(int value)
	{
		Hashtable customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
		customProperties["Difficulty"] = value;
		PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
	}

	public void OnDifficultyChange(Difficulty value)
	{
		roomDifficulty = value;
		difficultyText.text = value.ToString();
	}
}