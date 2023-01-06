using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject waveText;
    [SerializeField] private GameObject waveTimer;
    [SerializeField] private GameObject ammoText;
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private Healthbar staminaBar;
    public GameObject deathScreen;

    [SerializeField] private AmmoConfigScriptableObject ammoConfig;

    private WaveManager waveManager;

    public Healthbar HealthBar
    {
        get { return healthBar; }
    }

    public Healthbar StaminaBar
    {
        get { return staminaBar; }
    }

    void Start()
    {
        waveManager = GetComponent<WaveManager>();
        SetAmmoText();
    }

    void Update()
    {
        SetAmmoText();
    }

    public void SetAmmoText()
    {
        ammoText.GetComponent<TMPro.TextMeshProUGUI>().text = ammoConfig.currentClipAmmo.ToString() + "/" + ammoConfig.currentAmmo.ToString();
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
        if (wave == 0)
        {
            waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Prepare";
        }
        else
        {
            waveText.GetComponent<TMPro.TextMeshProUGUI>().text = "Wave " + wave;
        }
    }

    public void SetWaveTimerText(float time)
    {
        waveTimer.GetComponent<TMPro.TextMeshProUGUI>().text = time.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
