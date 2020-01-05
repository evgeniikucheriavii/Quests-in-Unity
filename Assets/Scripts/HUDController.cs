using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
	public Canvas canvas;

	public TextMeshProUGUI cursorText;

	public void SetCursorText(string text)
	{
		cursorText.SetText(text);
	}
}
