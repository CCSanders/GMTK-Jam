using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] GameObject sparksVFX;
    private Vector2 lastPoint;
    private Vector2 lastPointDirection;
    private RaycastHit2D hit;
    private ContactFilter2D filter;
    private int mask;

    // Start is called before the first frame update
    void Start()
    {
        mask = 1 << 6;
        lastPoint = transform.position;
       //LayerMask mask = ~LayerMask.GetMask("Camera Collider");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        lastPointDirection = pos - lastPoint;
        Debug.DrawRay(transform.position, lastPointDirection, Color.cyan, Vector2.Distance(transform.position, lastPoint));
        hit = Physics2D.Raycast(lastPoint, lastPointDirection, Vector2.Distance(transform.position, lastPoint), ~mask);
        if (hit.collider != null)
        {
            print("Hit: " + hit.transform.gameObject);
            if (!hit.collider.isTrigger)
            {
                //print("Hit: " + hit);
                //shouldMove = false;
                if (hit.transform.GetComponentInParent<IDamageable>() != null)
                {
                    hit.transform.GetComponentInParent<IDamageable>().TakeDamage();
                }
                else
                {
                    //print("Normal: " + hit.normal + " arcTan: " + Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x));
                    Quaternion rot =  Quaternion.Euler(0, 0,  Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x));
                    Destroy(Instantiate(sparksVFX, transform.position, rot), .5f);  
                }
                Destroy(gameObject);
            }
        }
        lastPoint = transform.position;
    }
}
