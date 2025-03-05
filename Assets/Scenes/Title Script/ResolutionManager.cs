using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

//public class ResolutionManager : MonoBehaviour
//{
//    public TMP_Dropdown resolutionDropdown; // �ػ� ������ ���� TMP ��Ӵٿ� �޴�
//    public int labelFontSize = 24; // ������ ���� �ؽ�Ʈ ũ��

//    private Resolution[] resolutions; // ��� ������ �ػ� �迭

//    private void Start()
//    {
//        resolutions = Screen.resolutions; // ��� ������ �ػ� ��� ��������

//        // ��Ӵٿ� �޴��� �ػ� �ɼ� �߰�
//        resolutionDropdown.ClearOptions();

//        List<string> resolutionOptions = new List<string>();
//        int currentResolutionIndex = -1;

//        for (int i = 0; i < resolutions.Length; i++)
//        {
//            string option = resolutions[i].width + " x " + resolutions[i].height;
//            resolutionOptions.Add(option);

//            // ���� �ػ󵵿� ��ġ�ϴ� �ɼ� �ε��� ����
//            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
//            {
//                currentResolutionIndex = i;
//            }
//        }

//        resolutionDropdown.AddOptions(resolutionOptions);

//        // ��Ӵٿ� �޴��� ������ �� �ؽ�Ʈ ũ�� ����
//        var label = resolutionDropdown.GetComponentInChildren<TextMeshProUGUI>();
//        if (label != null)
//        {
//            label.fontSize = labelFontSize;
//        }

//        // ����� �ػ� �ҷ�����
//        int savedWidth = PlayerPrefs.GetInt("ResolutionWidth", 1920);
//        int savedHeight = PlayerPrefs.GetInt("ResolutionHeight", 1080);

//        // ����� �ػ󵵿� ��ġ�ϴ� �ε��� ã��
//        int savedResolutionIndex = -1;
//        for (int i = 0; i < resolutions.Length; i++)
//        {
//            if (resolutions[i].width == savedWidth && resolutions[i].height == savedHeight)
//            {
//                savedResolutionIndex = i;
//                break;
//            }
//        }

//        if (savedResolutionIndex != -1)
//        {
//            resolutionDropdown.value = savedResolutionIndex;
//        }

//        ApplyResolution(); // ����� �ػ� ����
//    }

//    // ��Ӵٿ� �޴����� ������ �ػ󵵷� ȭ�� ����
//    public void ApplyResolution()
//    {
//        Resolution selectedResolution = resolutions[resolutionDropdown.value];

//        // ������ �ػ󵵸� PlayerPrefs�� ����
//        PlayerPrefs.SetInt("ResolutionWidth", selectedResolution.width);
//        PlayerPrefs.SetInt("ResolutionHeight", selectedResolution.height);
//        PlayerPrefs.Save();

//        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
//    }
//}

public class ResolutionManager : MonoBehaviour
{
    FullScreenMode m_screen_mode;
    public TMP_Dropdown m_resolution_dropdown;
    public Toggle m_full_screen_button;
    List<Resolution> m_resloutions = new List<Resolution>();
    int m_resolution_num;

    private void Start()
    {
        InitUI();
    }
    void InitUI()
    {
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
                m_resloutions.Add(Screen.resolutions[i]);
        }

        m_resolution_dropdown.options.Clear();

        int option_num = 0;
        foreach (Resolution item in m_resloutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz";
            m_resolution_dropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                m_resolution_dropdown.value = option_num;
            option_num++;
        }
        m_resolution_dropdown.RefreshShownValue();

        m_full_screen_button.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        m_resolution_num = x;
    }

    public void FullScreenButton(bool is_full)
    {
        m_screen_mode = is_full ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OkButtonClick()
    {
        Screen.SetResolution(m_resloutions[m_resolution_num].width, m_resloutions[m_resolution_num].height, m_screen_mode);
    }
}
