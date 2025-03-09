using System.IO;
using UnityEngine;

[System.Serializable]
public class SoundSettingData
{
    public bool m_background_is_on;
    public float m_background_value;
    public bool m_effect_is_on;
    public float m_effect_value;

    public SoundSettingData()
    {
        m_background_is_on = true;
        m_background_value = 0.5f;
        m_effect_is_on = true;
        m_effect_value = 0.5f;
    }
}

[System.Serializable]
public class GameSettingData
{
    public bool m_camera_shaking_is_on;
    public bool m_camera_light_is_on;

    public GameSettingData()
    {
        m_camera_shaking_is_on = true;
        m_camera_light_is_on = true;
    }
}

[System.Serializable]
public class SettingData
{
    public SoundSettingData m_sound_setting;
    public GameSettingData m_game_setting;

    public SettingData()
    {
        m_sound_setting = new SoundSettingData();
        m_game_setting = new GameSettingData();
    }
}

public class SettingManager : Singleton<SettingManager>
{
    private SettingData m_setting_data;
    public SettingData Setting
    {
        get { return m_setting_data; }
    }

    private string m_setting_data_path;

    private new void Awake()
    {
        base.Awake();

        m_setting_data_path = Path.Combine(Application.persistentDataPath, "SettingData.json");
        m_setting_data = new SettingData();

        LoadData();
    }

    private void LoadData()
    {
        if(File.Exists(m_setting_data_path))
        {
            var json_data = File.ReadAllText(m_setting_data_path);
            var setting_data = JsonUtility.FromJson<SettingData>(json_data);

            m_setting_data = setting_data;
        }
    }

    public void SaveData()
    {
        var json_data = JsonUtility.ToJson(m_setting_data);
        File.WriteAllText(m_setting_data_path, json_data);
    }
}
