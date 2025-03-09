using UnityEngine;

public class CreditCtrl : MonoBehaviour
{
    public void BTN_Back()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        LoadingManager.Instance.LoadScene("Title");
    }
}
