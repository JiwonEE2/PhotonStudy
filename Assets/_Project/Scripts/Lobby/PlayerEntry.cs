using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntry : MonoBehaviour
{
	public ToggleGroup characterSelectToggleGroup;
	public Text playerNameText;
	public Toggle readyToggle;
	public GameObject ready;

	private List<Toggle> selectToggles = new List<Toggle>();

	private void Awake()
	{
		foreach (Transform toggleTransform in characterSelectToggleGroup.transform)
		{
			selectToggles.Add(toggleTransform.GetComponent<Toggle>());
		}
	}
}

