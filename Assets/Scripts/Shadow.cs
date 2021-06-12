using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] ShadowClone clone; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clone.GetIsActive())
        {
            GetComponent<Renderer>().enabled = true;
            transform.position = new Vector3((player.position.x + clone.transform.position.x)/2, (player.position.y + clone.transform.position.y) / 2, 0);
            Vector3 dirVec = player.position - transform.position;
            float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.localScale = new Vector3(Vector3.Distance(player.position, clone.transform.position), transform.localScale.y, 1);
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
