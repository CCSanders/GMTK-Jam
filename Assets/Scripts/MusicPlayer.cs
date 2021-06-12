using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer _Instance;
    public AudioClip[] songs;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if(_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this);
            audioSource = GetComponent<AudioSource>();
            ChangeSong(0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeSong(int trackNum)
    {
        audioSource.Stop();
        audioSource.clip = songs[trackNum];
        audioSource.Play();
    }

    public void Volume(float value)
    {
        audioSource.volume = value;
    }

}
