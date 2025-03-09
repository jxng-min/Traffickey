using UnityEngine;

public class CreditCtrl : MonoBehaviour
{
    public void BTN_Back()
    {
        LoadingManager.Instance.LoadScene("Title");
    }
}
