using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1;
    private Vector2 lastPoint;
    private Vector2 lastPointDirection;
    private RaycastHit2D hit;
    private ContactFilter2D filter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        lastPointDirection = pos - lastPoint;
        //Debug.DrawRay(transform.position, lastPointDirection, Color.cyan, Vector2.Distance(transform.position, lastPoint));
        hit = Physics2D.Raycast(lastPoint, lastPointDirection, Vector2.Distance(transform.position, lastPoint));
        if (hit.collider != null)
        {
            print("Hit: " + hit);
            if (hit.transform.tag != "Player" && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                //shouldMove = false;
                if (hit.transform.GetComponentInParent<IDamageable>() != null)
                {
                    hit.transform.GetComponentInParent<IDamageable>().TakeDamage();
                }
                else
                {

                }
                Destroy(gameObject);
            }
        }
        lastPoint = transform.position;
    }
}
