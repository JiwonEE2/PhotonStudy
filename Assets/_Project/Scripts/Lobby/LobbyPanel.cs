using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviour
{
	public RectTransform roomListRect;
	public GameObject roomButtonPrefab;
	public Button cancelButton;

	private void Awake()
	{
		cancelButton.onClick.AddListener(CancelButtonClick);
	}

	private void CancelButtonClick()
	{
		PhotonNetwork.LeaveLobby();
	}
}