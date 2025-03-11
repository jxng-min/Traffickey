using UnityEngine;

public class CreditCtrl : MonoBehaviour
{
    private void Start()
    {
        GameEventBus.Publish(GameEventType.None);
    }
    
    public void BTN_Back()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        LoadingManager.Instance.LoadScene("Title");
    }
}
