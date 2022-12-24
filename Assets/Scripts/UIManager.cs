using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject wavePanel;
    [SerializeField] private GameObject waveText;
    [SerializeField] private GameObject waveTimer;
    [SerializeField] private Healthbar healthBar;
    private WaveManager waveManager;

    public Healthbar HealthBar
    {
        get { return healthBar; }
    }

    void Start()
    {
        waveManager = GetComponent<WaveManager>();
    }

    public void ShowWaveText()
    {
        waveText.SetActive(true);
    }

    public void HideWaveText()
    {
        waveText.SetActive(false);
    }

    public void SetWaveText(int wave)
    {
        waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave " + wave;
    }

    public void SetWaveTimerText(int wave, float time)
    {
        waveTimer.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave " + (wave + 1) + " starting in " + time + " seconds";
    }
}
