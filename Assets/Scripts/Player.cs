using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour, IDamageable
{
    public Transform crosshair;
    public float moveSpeed = 5.0f;
    public Transform bulletSpawn;
    public GameObject bullet;
    public int ammoMax = 50;
    public int ammoCount;
    public float fireCooldown = .5f;
    public ShadowClone clone;
    public GameObject deathAnimation;

    public Animator animator;
    public Animator legsAnimator;

    public AudioClip fireSound;
    public AudioClip meleeSound;
    public AudioClip DeathSound;

    private Rigidbody2D rigidBody;
    private float currentCooldown;
    private bool isMoving = false;
    private int currentWeapon = 0; //0 is unarmed, 1 is melee, 2 is gun
    private bool weaponOnRight = true; //false is left
    private SwordHitboxManager swordHitboxManager;

    private CinemachineVirtualCamera vCam;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        swordHitboxManager = GetComponentInChildren<SwordHitboxManager>();

        vCam = FindObjectOfType<CinemachineVirtualCamera>();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleAbilities();
    }

    void HandleMovement()
    {
        isMoving = true;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
            legsAnimator.gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
            //rigidBody.MovePosition(transform.position + (Vector3.up * moveSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
            legsAnimator.gameObject.transform.eulerAngles = new Vector3(0, 0, 270);
            //rigidBody.MovePosition(transform.position + (Vector3.down * moveSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
            legsAnimator.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            //rigidBody.MovePosition(transform.position + (Vector3.left * moveSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
            legsAnimator.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
            //rigidBody.MovePosition(transform.position + (Vector3.right * moveSpeed) * Time.deltaTime);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            isMoving = false;
        }

        //send values to animators
        animator.SetBool("isMoving", isMoving);
        legsAnimator.SetBool("isMoving", isMoving);
    }

    void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        crosshair.transform.position = mousePosition;

        float angle = Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x) * Mathf.Rad2Deg;
        //print("Mouse position: " + mousePosition + ", angle: " + angle);

        //set rotation and undo leg rotation with equal and opposite amount
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
        legsAnimator.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
    }

    void HandleAbilities()
    {
        //weapon switching debug
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentWeapon = 0;
        }

        animator.SetInteger("weaponIndex", currentWeapon);

        //weapon firing and abilities
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentWeapon == 1)
            {
                FireMelee();
            }
            else if (currentWeapon == 2 && fireCooldown + currentCooldown < Time.time && ammoCount > 0)
            {
                FireGun();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            clone.EnableClone(transform.position);
        }
    }

    void FireMelee()
    {
        if (fireCooldown + currentCooldown < Time.time)
        {
            clone.ShadowFire();
            currentCooldown = Time.time;
        }

        animator.SetTrigger("attack");
        animator.SetBool("weaponOnRight", weaponOnRight);
        weaponOnRight = !weaponOnRight;
        MusicPlayer._Instance.PlayOneShot(meleeSound);
    }

    void FireGun()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        clone.ShadowFire();
        currentCooldown = Time.time;
        ammoCount--;

        MusicPlayer._Instance.PlayOneShot(fireSound);
        animator.SetTrigger("attack");
    }

    public void SetCurrentFrameData(SwordHitboxManager.Frames current)
    {
        swordHitboxManager.SetCurrentFrameData(current);
    }
    public void TakeDamage()
    {
        Destroy(Instantiate(deathAnimation, transform.position, transform.rotation), 1);
        MusicPlayer._Instance.PlayOneShot(DeathSound);
        gameObject.SetActive(false);
        FindObjectOfType<UI>().SetResetText(true);
    }

    public void ResetPlayer()
    {
        ammoCount = ammoMax;
        clone.CloneReset();
    }

}
