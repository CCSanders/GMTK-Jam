using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitboxManager : MonoBehaviour
{
    public PolygonCollider2D frame1L2R;
    public PolygonCollider2D frame2L2R;
    public PolygonCollider2D frame3L2R;
    public PolygonCollider2D frame4L2R;
    public PolygonCollider2D frame1R2L;
    public PolygonCollider2D frame2R2L;
    public PolygonCollider2D frame3R2L;
    public PolygonCollider2D frame4R2L;

    // Used for organization
    private PolygonCollider2D[] colliders;

    // Collider on this game object
    private PolygonCollider2D localCollider;
    private Frames lastFrameActivated = Frames.clear;


    public enum Frames
    {
        frame1L2R,
        frame2L2R,
        frame3L2R,
        frame4L2R,
        frame1R2L,
        frame2R2L,
        frame3R2L,
        frame4R2L,
        clear
    }

    // Start is called before the first frame update
    void Start()
    {
        colliders = new PolygonCollider2D[] { frame1L2R, frame2L2R, frame3L2R, frame4L2R, frame1R2L, frame2R2L, frame3R2L, frame4R2L };
        localCollider = gameObject.AddComponent<PolygonCollider2D>();
        localCollider.isTrigger = true;
        localCollider.pathCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("sword collider hit: " + collision.name + " on last frame set: " + lastFrameActivated);
        if(collision.GetComponent<IDamageable>() != null)
        {
            collision.GetComponent<IDamageable>().TakeDamage();
        }
    }

    public void SetCurrentFrameData(Frames current)
    {
        if(current != Frames.clear)
        {
            localCollider.SetPath(0, colliders[(int)current].GetPath(0));
            lastFrameActivated = current;
            return;
        }

        localCollider.pathCount = 0;
    }
}
