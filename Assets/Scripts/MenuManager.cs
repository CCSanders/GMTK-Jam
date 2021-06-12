using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] AudioClip SelectBlip;
    [SerializeField] AudioClip HoverBlip;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlay()
    {
        audio.PlayOneShot(SelectBlip);
        MusicPlayer._Instance.ChangeSong(1);
        SceneManager.LoadScene(1);
    }

    public void OnOptions()
    {
        audio.PlayOneShot(SelectBlip);
        OptionsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void OnCredits()
    {
        audio.PlayOneShot(SelectBlip);
        CreditsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void OnBackToMenuOptions()
    {
        audio.PlayOneShot(SelectBlip);
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void OnBackToMenuCredits()
    {
        audio.PlayOneShot(SelectBlip);
        CreditsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void OnQuit()
    {
        audio.PlayOneShot(SelectBlip);
        Application.Quit();
    }

    public void OnHover()
    {
        audio.clip = HoverBlip;
        audio.Play();
    }

    public void MusicVolume(float value)
    {
        MusicPlayer._Instance.Volume(value);
    }
}
