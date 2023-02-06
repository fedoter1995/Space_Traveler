using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject controlSettingsPanel;
    [SerializeField] private GameObject proceedButton;
    [SerializeField] private Dropdown dropdownController;

    private SpaceshipStats spaceship;
    private static bool gameIsRunning = false;
    private bool settings = false;

    private void Start()
    {
        spaceship = GameObject.FindWithTag("Player").GetComponent<SpaceshipStats>();
        controlSettingsPanel.SetActive(false);

        if (!gameIsRunning)
        {
            Pause();
            proceedButton.SetActive(false);
        }
        else
        {
            menuPanel.SetActive(false);
            UnPause();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
                MainMenu();


    }


    private void Pause()
    {
        Time.timeScale = 0;
    }
    private void UnPause()
    {
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        if (Time.timeScale == 0 && !settings)
        {
            UnPause();
            menuPanel.SetActive(!menuPanel.activeInHierarchy);
        }
        else if (Time.timeScale == 0 && settings)
        {
            controlSettingsPanel.SetActive(false);
            settings = !settings;
        }
        else if (Time.timeScale == 1)
        {
            Pause();
            menuPanel.SetActive(!menuPanel.activeInHierarchy);
        }
    }


    public void NewGameButtonClicked()
    {
        if (gameIsRunning)
        {
            SceneManager.LoadScene("Asteroids");
        }
        else
        {
            UnPause();
            GameIsRunning();
            menuPanel.SetActive(false);
            proceedButton.SetActive(true);
        }


    }
    public void ExitGameButtonClicked()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void ControlSetingsButtonClicked()
    {
        controlSettingsPanel.SetActive(true);
        settings = true;
    }

    public void ControlChanged()
    {
        Debug.Log(dropdownController.value);
    }
    public void ProceedButtonClicked()
    {
        UnPause();
        menuPanel.SetActive(false);
    }

    private void GameIsRunning()
    {
        Menu.gameIsRunning = !gameIsRunning; 
    }

}
