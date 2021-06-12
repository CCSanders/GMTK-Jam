using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5.0f;

    private Rigidbody2D rigidBody;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void FixedUpdate()
    {
        //rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        //rigidBody.velocity = velocity;
    }

    void HandleMovement()
    {
        //Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //velocity = input * moveSpeed;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
            //rigidBody.MovePosition(transform.position + (Vector3.up * moveSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
            //rigidBody.MovePosition(transform.position + (Vector3.down * moveSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
            //rigidBody.MovePosition(transform.position + (Vector3.left * moveSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World); 
            //rigidBody.MovePosition(transform.position + (Vector3.right * moveSpeed) * Time.deltaTime);
        }
    }

    void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x) * Mathf.Rad2Deg;
        //print("Mouse position: " + mousePosition + ", angle: " + angle);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
    }
}
