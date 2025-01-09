using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


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

	public Dictionary<int, PlayerEntry> playerListDic = new Dictionary<int, PlayerEntry>();

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
			Dropdown.OptionData option = new Dropdown.OptionData(difficulty.ToString());
			difficultyDropdown.options.Add(option);
		}

		difficultyDropdown.onValueChanged.AddListener(DifficultyValueChange);
	}

	private void OnEnable()
	{
		// 플레이어 리스트에 다른 객체가 있으면 일단 모두 삭제
		foreach (Transform child in playerList)
		{
			Destroy(child.gameObject);
		}

		// 유효성 검사(방에 입장한 상태인가?==InRoom)
		if (false == PhotonNetwork.InRoom) return;

		roomTitleText.text = PhotonNetwork.CurrentRoom.Name;

		// photon의 Player 클래스 가져오기
		foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
		{
			// 플레이어 정보 객체 생성
		}

		// 방장인 지 여부를 확인하여 활성 비활성
		difficultyDropdown.gameObject.SetActive(PhotonNetwork.IsMasterClient);
		startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
	}

	private void CancelButtonClick()
	{
		PhotonNetwork.LeaveRoom();
	}

	private void StartButtonClick()
	{
	}

	private void DifficultyValueChange(int arg0)
	{
	}
}