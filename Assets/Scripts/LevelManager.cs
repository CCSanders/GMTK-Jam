using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] Player player;

    private Vector3[] enemyPositions;
    private Vector3 playerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemyPositions = new Vector3[enemies.Length];
        for(int x = 0; x < enemies.Length; x++)
        {
            enemyPositions[x] = enemies[x].transform.position;
        }
        playerSpawn = player.transform.position;
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
    }
}
