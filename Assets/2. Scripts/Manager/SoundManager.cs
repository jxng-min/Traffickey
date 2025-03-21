using System.Collections;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource m_background_source;
    public AudioSource BGM
    {
        get { return m_background_source; }
    }

    [SerializeField] private AudioClip[] m_background_clips;
    [SerializeField] private AudioClip[] m_effect_clips;

    private string m_last_background_name;
    public string LastBGM
    {
        get { return m_last_background_name; }
    }

    private new void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        m_background_source.volume = SettingManager.Instance.Setting.m_sound_setting.m_background_value;
    }

    public void PlayBGM(string background_name)
    {
        StartCoroutine(ChangeBGM(background_name));
    }

    public IEnumerator ChangeBGM(string background_name)
    {
        int target_index = -1;
        for(int i = 0; i < m_background_clips.Length; i++)
        {
            if(m_background_clips[i].name == background_name)
            {
                target_index = i;
                break;
            }
        }

        if(target_index != -1)
        {
            if(m_background_source.isPlaying)
            {
                if(m_background_source.clip is not null)
                {
                    m_last_background_name = m_background_source.clip.name;
                }

                yield return StartCoroutine(Fade(m_background_source, true, true));
                yield return new WaitForSeconds(0.3f);
            }

            m_background_source.clip = m_background_clips[target_index]; 
            m_background_source.Play();

            yield return StartCoroutine(Fade(m_background_source, false, true));
        }
    }

    public void PlayEffect(string effect_name)
    {
        if(!SettingManager.Instance.Setting.m_sound_setting.m_effect_is_on)
        {
            return;
        }

        int target_index = -1;
        for(int i = 0; i < m_effect_clips.Length; i++)
        {
            if(m_effect_clips[i].name == effect_name)
            {
                target_index = i;
                break;
            }
        }

        if(target_index != -1)
        {
            AudioSource effect_source = ObjectManager.Instance.GetObject(ObjectType.EFFECTSOURCE).GetComponent<AudioSource>();

            effect_source.volume = SettingManager.Instance.Setting.m_sound_setting.m_effect_value;
            effect_source.clip = m_effect_clips[target_index]; 
            effect_source.Play();

            StartCoroutine(ReturnEffect(effect_source));
        }
    }

    public void UpdateBackgroundVolume()
    {
        m_background_source.volume = SettingManager.Instance.Setting.m_sound_setting.m_background_value;
    }

    private IEnumerator ReturnEffect(AudioSource target_source)
    {
        float elapsed_time = 0f;
        float target_time = target_source.clip.length;

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        ObjectManager.Instance.ReturnObject(target_source.gameObject, ObjectType.EFFECTSOURCE);
    }

    private IEnumerator Fade(AudioSource target_source, bool is_out, bool is_background)
    {
        float elapsed_time = 0f;
        float target_time = 1f;

        while(elapsed_time < target_time)
        {
            float f = elapsed_time / target_time;

            if(is_background)
            {
                if(is_out)
                {
                    target_source.volume = Mathf.Lerp(SettingManager.Instance.Setting.m_sound_setting.m_background_value, 0f, f);
                }
                else
                {
                    target_source.volume = Mathf.Lerp(0f, SettingManager.Instance.Setting.m_sound_setting.m_background_value, f);
                }
            }
            else
            {
                if(is_out)
                {
                    target_source.volume = Mathf.Lerp(SettingManager.Instance.Setting.m_sound_setting.m_effect_value, 0f, f);
                }
                else
                {
                    target_source.volume = Mathf.Lerp(0f, SettingManager.Instance.Setting.m_sound_setting.m_effect_value, f);
                }
            }

            elapsed_time += Time.deltaTime;

            yield return null;
        }

        if(is_background)
        {
            if(is_out)
            {
                target_source.volume = 0f;
            }
            else
            {
                target_source.volume = SettingManager.Instance.Setting.m_sound_setting.m_background_value;
            }
        }
        else
        {
            if(is_out)
            {
                target_source.volume = 0f;
            }
            else
            {
                target_source.volume = SettingManager.Instance.Setting.m_sound_setting.m_effect_value;
            }
        }
    }
}