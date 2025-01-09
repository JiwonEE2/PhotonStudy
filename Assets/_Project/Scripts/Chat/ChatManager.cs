using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using UnityEngine;

// Photon Chat 사용
// 1. IChatClientListner 인터페이스 구현
public class ChatManager : MonoBehaviour, IChatClientListener
{
	public static ChatManager Instance { get; private set; }

	public JoinUI joinUI;
	public ChatUI chatUI;

	private ChatClient client;

	private void Awake()
	{
		Instance = this;
	}

	// 2. ChatClient 생성
	private void Start()
	{
		client = new ChatClient(this);
	}

	// 3. Update에서 Service 호출
	private void Update()
	{
		client.Service();
	}

	public void DebugReturn(DebugLevel level, string message)
	{
	}

	public void OnDisconnected()
	{
	}

	public void OnConnected()
	{
	}

	public void OnChatStateChange(ChatState state)
	{
	}

	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
	}

	public void OnPrivateMessage(string sender, object message, string channelName)
	{
	}

	public void OnSubscribed(string[] channels, bool[] results)
	{
	}

	public void OnUnsubscribed(string[] channels)
	{
	}

	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
	}

	public void OnUserSubscribed(string channel, string user)
	{
	}

	public void OnUserUnsubscribed(string channel, string user)
	{
	}
}