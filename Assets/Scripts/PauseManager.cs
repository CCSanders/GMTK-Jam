using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] AudioClip SelectBlip;
    [SerializeField] AudioClip HoverBlip;
    AudioSource audioSource;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                MainMenuPanel.SetActive(false);
                OptionsPanel.SetActive(false);
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                MainMenuPanel.SetActive(true);
                paused = true;
            }
        }
    }

    public void OnPlay()
    {
        audioSource.PlayOneShot(SelectBlip);
        Time.timeScale = 1;
        Cursor.visible = false;
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        paused = false;
    }

    public void OnOptions()
    {
        audioSource.PlayOneShot(SelectBlip);
        OptionsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }


    public void OnBackToMenuOptions()
    {
        audioSource.PlayOneShot(SelectBlip);
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void OnReturnToMenu()
    {
        audioSource.PlayOneShot(SelectBlip);
        MusicPlayer._Instance.ChangeSong(0);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnHover()
    {
        audioSource.clip = HoverBlip;
        audioSource.Play();
    }

    public void MusicVolume(float value)
    {
        MusicPlayer._Instance.Volume(value);
    }

    public void MasterVolume(float value)
    {
        MusicPlayer._Instance.MasterVolume(value - 80);
    }
}
