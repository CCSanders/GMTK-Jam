using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] int nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            MusicPlayer._Instance.ChangeSong(nextScene);
            SceneManager.LoadScene(nextScene);
        }
    }
}
