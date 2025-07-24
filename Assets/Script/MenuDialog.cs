using TMPro;
using UnityEngine;

public class MenuDialog : MonoBehaviour
{
    public TextMeshProUGUI titleDialogText;
    public TextMeshProUGUI buttonDialogText;

    public void DisplayDialog(string _titleDialogText, string _buttonDialogText)
    {
        titleDialogText.text = _titleDialogText;
        buttonDialogText.text = _buttonDialogText;
    }

    public void OnClickBtn()
    {
        GameManager.Ins.PlayGame();
        this.gameObject.SetActive(false);
    }
}
