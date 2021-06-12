using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Transform bulletSpawn;
    public GameObject bullet;
    public float ShadowTime = 5f;

    public AudioClip fireSound;

    private Rigidbody2D rigidBody;
    private bool isActive = false;
    private Renderer rend;
    [SerializeField] float shadowCurrentCooldown = 5f;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<Renderer>();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            HandleRotation();
            //HandleMovement();

            shadowCurrentCooldown -= Time.deltaTime;

            if (shadowCurrentCooldown <= 0)
            {
                DisableClone();
            }
        }
        else
        {
            if(shadowCurrentCooldown <= ShadowTime)
                shadowCurrentCooldown = Mathf.Min(Time.deltaTime + shadowCurrentCooldown, ShadowTime);
        }
    }

    public void EnableClone(Vector3 pos)
    {
        if(isActive)
        {
            DisableClone();
            return;
        }
        if (shadowCurrentCooldown >= ShadowTime)
        {
            transform.position = pos;
            isActive = true;
            rend.enabled = true;
        }
    }

    public void DisableClone()
    {
        isActive = false;
        rend.enabled = false;
    }


    public void ShadowFire()
    {
        if (isActive)
        {
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            MusicPlayer._Instance.PlayOneShot(fireSound);
        }
    }

    void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        float angle = Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x) * Mathf.Rad2Deg;
        //print("Mouse position: " + mousePosition + ", angle: " + angle);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
    }
}
