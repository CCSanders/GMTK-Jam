using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public float patrolMoveSpeed = 2.0f;
    public float ChaseMoveSpeed = 5.0f;
    public Transform bulletSpawn;
    public GameObject bullet;
    public int ammoCount;
    public float fireCooldown = .5f;
    public Transform[] patrolPoints;
    public float attackDistance = 10f;
    public float stopDistance = 4f;

    public AudioClip fireSound;

    private int currentPoint = 0;
    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    private float currentCooldown;
    private bool isPatroling = true;
    private Player player;
    private int mask;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        mask = 1 << 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatroling)
            Patrol();
        else
            ChasePlayer();
    }

    void Patrol()
    {
        //print(Vector3.Distance(patrolPoints[currentPoint].position, transform.position));
        if(Vector3.Distance(patrolPoints[currentPoint].position, transform.position) <= 2f)
        {
            if (++currentPoint >= patrolPoints.Length)
                currentPoint = 0;
        }

        HandleRotation(patrolPoints[currentPoint].position);
        transform.Translate(transform.up * patrolMoveSpeed * Time.deltaTime, Space.World);

    }

    void ChasePlayer()
    {
        HandleRotation(player.transform.position);
        print("Enemy to player " + Vector3.Distance(patrolPoints[currentPoint].position, transform.position));
        if (Vector3.Distance(player.transform.position, transform.position) >= stopDistance)
        {
            transform.Translate(transform.up * patrolMoveSpeed * Time.deltaTime, Space.World);
        }
        if(Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
            attack();
        }
    }

    void attack()
    {
        if (fireCooldown + currentCooldown < Time.time && ammoCount > 0)
        {
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            currentCooldown = Time.time;
            ammoCount--;
            MusicPlayer._Instance.PlayOneShot(fireSound);
        }
    }

    void HandleRotation(Vector3 targetPos)
    {
        float angle = Mathf.Atan2(transform.position.y - targetPos.y, transform.position.x - targetPos.x) * Mathf.Rad2Deg;
        //print("Mouse position: " + mousePosition + ", angle: " + angle);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
    }

    public void IsInVisionRange()
    {
        //print("Is in vision range");
        if (isPatroling)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            Vector2 posPlayer = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 direc = posPlayer - pos;
            //Debug.DrawRay(bulletSpawn.position, direc);
            RaycastHit2D hit = Physics2D.Raycast(bulletSpawn.position, direc, 50, ~mask);
            if (hit.collider != null)
            {
                //print("Enemy sees " + hit.collider.gameObject);
                if(hit.collider.GetComponent<Player>() != null)
                {
                    isPatroling = false;
                    //print("Player found");
                }
            }
        }
    }
}
