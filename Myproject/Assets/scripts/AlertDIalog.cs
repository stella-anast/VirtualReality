using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class AlertDialog : MonoBehaviour
{
	public GameObject dialogBox;
	public TextMeshProUGUI messageText;
	public Button okButton;
	public Button cancelButton;

	private System.Action<bool> responseCallback;



	void Start()
	{
		dialogBox.SetActive(false);

		okButton.onClick.AddListener(() => HandleResponse(true));
        cancelButton.onClick.AddListener(() => HandleResponse(false));
    }

	public void ShowDialog(string message, System.Action<bool> callback)
	{
		responseCallback = callback;
		messageText.text = message;
		dialogBox.SetActive(true);
	}


    private void HandleResponse(bool response)
    {
		dialogBox.SetActive(false);
		responseCallback?.Invoke(response);	
	}

}

