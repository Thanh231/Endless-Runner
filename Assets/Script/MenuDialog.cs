
using TMPro;
using UnityEngine;

public class MenuDialog : MonoBehaviour
{
    public PlayerInput input;
    public TextMeshProUGUI titleDialogText;
    public TextMeshProUGUI buttonDialogText;

    public GameObject tutorialDialogDesktop;
    public GameObject tutorialDialogMobile;

    public void DisplayDialog(string _titleDialogText, string _buttonDialogText, bool isShowHighScore)
    {
        titleDialogText.text = _titleDialogText;
        buttonDialogText.text = _buttonDialogText;
        EventManager.ShowHighScore?.Invoke();
    }

    public void OnClickBtn()
    {
        // tutorialDialog.SetActive(true);
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            tutorialDialogMobile.SetActive(true);
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            tutorialDialogDesktop.SetActive(true);
        }

        EventManager.TurnOffHighScore?.Invoke();
        this.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        tutorialDialogDesktop.SetActive(false);
        tutorialDialogMobile.SetActive(false);
        // tutorialDialog.SetActive(false);
        GameManager.Ins.PlayGame();
    }
}
