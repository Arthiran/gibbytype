using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputHandler : MonoBehaviour
{
	private CreateWords createWords;

	private void Awake()
	{
		createWords = GetComponent<CreateWords>();
	}

	void Update()
	{
		foreach (char letter in Input.inputString)
		{
			createWords.HandleLetter(letter);
		}
	}
}
