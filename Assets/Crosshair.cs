using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public float maxDistanceFromPlayerX = 10f;
    public float maxDistanceFromPlayerY = 5f;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
         * My attempt
         * 
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosWorld.z = 0;

        float distanceFromPlayer = Vector3.Distance(player.transform.position, mousePosWorld);
        if(distanceFromPlayer > maxDistanceFromPlayerX)
        {
            distanceFromPlayer = maxDistanceFromPlayerX;
        }

        transform.position = player.transform.position + (mousePosWorld - player.transform.position).normalized * distanceFromPlayer;
        */
    }
}
