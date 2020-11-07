using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NameInput : MonoBehaviour
{
    [SerializeField] private InputField nameInputField = null;
    [SerializeField] private Button continueButton = null;

    private const string playerprefsNameKey = "PlayerName";

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        SetupInputField();
    }

    private void SetupInputField()
    {
        if (!PlayerPrefs.HasKey(playerprefsNameKey))
        {
            return;
        }

        string defaultName = PlayerPrefs.GetString(playerprefsNameKey);
        nameInputField.text = defaultName;
        SetPlayerName(defaultName);
    }

    public void SetPlayerName(string name)
    {
        // only press button when you input someting.
        continueButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePLayerName()
    {
        string playerName = nameInputField.text;
        PhotonNetwork.NickName = playerName;

        PlayerPrefs.SetString(playerprefsNameKey, playerName);
    }
}
