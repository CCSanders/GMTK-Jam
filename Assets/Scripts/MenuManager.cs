using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject CreditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnOptions()
    {
        OptionsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void OnCredits()
    {
        CreditsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void OnBackToMenuOptions()
    {
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void OnBackToMenuCredits()
    {
        CreditsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
