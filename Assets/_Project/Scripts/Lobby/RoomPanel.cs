using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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