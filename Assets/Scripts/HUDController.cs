using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
	public Canvas canvas;
	public GameObject inventoryPanel;

	public TextMeshProUGUI cursorText;

	public void SetCursorText(string text)
	{
		cursorText.SetText(text);
	}

	public void ShowInventory()
	{
		inventoryPanel.SetActive(true);
	}

	public void HideInventory()
	{
		inventoryPanel.SetActive(false);
	}
}
