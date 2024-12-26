using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
	public Text roomNameLabel;
	public InputField messageInput;
	public Button sendButton;
	public RectTransform messageContent;
	public GameObject messageEntryPrefab;

	public string myNickname = "������ ����";

	private void Awake()
	{
		messageInput.onEndEdit.AddListener(x=>SendChatMessage());
		sendButton.onClick.AddListener(SendChatMessage);
	}
	
	//�޽����� ������ ȣ��
	public void SendChatMessage()
	{
		messageInput.text = "";
	}

	//�޽����� ������ ȣ��
	public void ReceiveChatMessage(string nickname, string message)
	{
		var entry = Instantiate(messageEntryPrefab, messageContent);
		entry.transform.Find("Nickname").GetComponent<Text>().text = nickname;
		entry.transform.Find("Message").GetComponent<Text>().text = message;
	}


}

