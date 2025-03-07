using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : Singleton<StaminaManager>
{
    [Header("스테미너 Slider UI")]
    [SerializeField] private Slider m_stamina_slider;

    [Header("최대 스테미너")]
    [Range(0f, 100f)][SerializeField] private float m_max_stamina;
    private float m_current_stamina;
    public float Current
    {
        get { return m_current_stamina; }
    }

    [Header("초당 스테미너 회복량")]
    [Range(0f, 10f)][SerializeField] private float m_regen_rate;

    [Header("초당 스테미너 소모량")]
    [Range(0f, 10f)][SerializeField] private float m_drain_rate;

    private bool IsUsing = false;

    private void Start()
    {
        m_current_stamina = m_max_stamina;
        m_stamina_slider.maxValue = m_max_stamina;
        m_stamina_slider.value = m_current_stamina;
    }

    private void Update()
    {
        m_stamina_slider.value = m_current_stamina;

        Debug.Log($"{m_stamina_slider.value}, {m_stamina_slider.maxValue}");

        if(m_stamina_slider.value >= m_stamina_slider.maxValue)
        {
            Debug.Log("들어옴");
            m_stamina_slider.gameObject.SetActive(false);
        }
    }

    public void UseStamina()
    {
        m_stamina_slider.gameObject.SetActive(true);

        m_current_stamina -= m_drain_rate * Time.deltaTime;
        m_current_stamina = Mathf.Clamp(m_current_stamina, 0f, m_max_stamina);
    }

    public void UseStamina(float amount)
    {
        m_stamina_slider.gameObject.SetActive(true);

        m_current_stamina -= amount;
        m_current_stamina = Mathf.Clamp(m_current_stamina, 0f, m_max_stamina);
    }

    public void RegenStamina()
    {
        if(m_stamina_slider.value == m_stamina_slider.maxValue)
        {
            return;
        }

        m_stamina_slider.gameObject.SetActive(true);

        m_current_stamina += m_regen_rate * Time.deltaTime;
        m_current_stamina = Mathf.Clamp(m_current_stamina, 0f, m_max_stamina);
    }

    public void RegenStamina(float amount)
    {
        if(m_stamina_slider.value == m_stamina_slider.maxValue)
        {
            return;
        }
        
        m_stamina_slider.gameObject.SetActive(true);

        m_current_stamina += amount;
        m_current_stamina = Mathf.Clamp(m_current_stamina, 0f, m_max_stamina);
    }
}
