using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

//public class ResolutionManager : MonoBehaviour
//{
//    public TMP_Dropdown resolutionDropdown; // 해상도 설정을 위한 TMP 드롭다운 메뉴
//    public int labelFontSize = 24; // 아이템 라벨의 텍스트 크기

//    private Resolution[] resolutions; // 사용 가능한 해상도 배열

//    private void Start()
//    {
//        resolutions = Screen.resolutions; // 사용 가능한 해상도 목록 가져오기

//        // 드롭다운 메뉴에 해상도 옵션 추가
//        resolutionDropdown.ClearOptions();

//        List<string> resolutionOptions = new List<string>();
//        int currentResolutionIndex = -1;

//        for (int i = 0; i < resolutions.Length; i++)
//        {
//            string option = resolutions[i].width + " x " + resolutions[i].height;
//            resolutionOptions.Add(option);

//            // 현재 해상도와 일치하는 옵션 인덱스 저장
//            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
//            {
//                currentResolutionIndex = i;
//            }
//        }

//        resolutionDropdown.AddOptions(resolutionOptions);

//        // 드롭다운 메뉴의 아이템 라벨 텍스트 크기 조정
//        var label = resolutionDropdown.GetComponentInChildren<TextMeshProUGUI>();
//        if (label != null)
//        {
//            label.fontSize = labelFontSize;
//        }

//        // 저장된 해상도 불러오기
//        int savedWidth = PlayerPrefs.GetInt("ResolutionWidth", 1920);
//        int savedHeight = PlayerPrefs.GetInt("ResolutionHeight", 1080);

//        // 저장된 해상도와 일치하는 인덱스 찾기
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

//        ApplyResolution(); // 저장된 해상도 적용
//    }

//    // 드롭다운 메뉴에서 선택한 해상도로 화면 조정
//    public void ApplyResolution()
//    {
//        Resolution selectedResolution = resolutions[resolutionDropdown.value];

//        // 선택한 해상도를 PlayerPrefs에 저장
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
