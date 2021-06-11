using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x) * Mathf.Rad2Deg;
        print("Mouse position: " + mousePosition + ", angle: " + angle);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
    }
}
