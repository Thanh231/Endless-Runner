using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MenuDialog : MonoBehaviour
{
    public PlayerInput input;
    public TextMeshProUGUI titleDialogText;
    public TextMeshProUGUI buttonDialogText;

    public GameObject tutorialDialog;

    public void DisplayDialog(string _titleDialogText, string _buttonDialogText)
    {
        titleDialogText.text = _titleDialogText;
        buttonDialogText.text = _buttonDialogText;
    }

    public void OnClickBtn()
    {
        tutorialDialog.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        tutorialDialog.SetActive(false);
        GameManager.Ins.PlayGame();
    }
}
