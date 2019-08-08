using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteUpdater : EnemyBase
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    public SpriteRenderer spriteRenderer;
    public float angle = 0;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (awakened)
        {
            angle = AngleFinder();
            if (angle <= 45 && angle > -45)
            {
                spriteRenderer.sprite = up;
            }
            if (angle > 45 && angle <= 135)
            {
                spriteRenderer.sprite = left;
            }
            if (angle > 135 && angle <= 225)
            {
                spriteRenderer.sprite = down;
            }
            if (angle > 225 || angle <= -45)
            {
                spriteRenderer.sprite = right;
            }
        }
    }
    public override void Backstab()
    {
        //Do Nothing
    }
}
