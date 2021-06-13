using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] Player player;
    [SerializeField] GameObject nextLevelTrigger;

    private Vector3[] enemyPositions;
    private Vector3 playerSpawn;
    private int enemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        enemyPositions = new Vector3[enemies.Length];
        for(int x = 0; x < enemies.Length; x++)
        {
            enemyPositions[x] = enemies[x].transform.position;
        }
        playerSpawn = player.transform.position;
        enemiesLeft = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    void Reset()
    {
        for (int x = 0; x < enemies.Length; x++)
        {
            enemies[x].SetActive(true);
            enemies[x].transform.position = enemyPositions[x];
            enemies[x].GetComponent<EnemyAi>().ResetEnemy();
        }
        player.gameObject.SetActive(true);
        player.transform.position = playerSpawn;
        player.ResetPlayer();
        FindObjectOfType<UI>().SetResetText(false);
        enemiesLeft = enemies.Length;
    }

    public void EnemyKilled()
    {
        if(--enemiesLeft <= 0)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        FindObjectOfType<UI>().SetWinText(true);
        nextLevelTrigger.SetActive(true);
    }
}
