using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text messageText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}
