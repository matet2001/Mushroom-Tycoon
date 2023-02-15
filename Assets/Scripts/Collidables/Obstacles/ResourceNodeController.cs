using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeController : CollidableBase
{
    [SerializeField] private ResourceTypeSO resourceType;
    [SerializeField] private int resourceAmount;

    private new Collider2D collider;
    private SpriteRenderer spriteRenderer;

    public float alpha, disappeareTime = 2f, timeElapsed;
    public bool shouldDisappeare;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void Collision()
    {
        StartDisappeare();
    }
    private void Update()
    {
        Disappeare();
    }
    private void Disappeare()
    {
        if (!shouldDisappeare) return;

        if (timeElapsed < disappeareTime)
        {
            alpha = Mathf.Lerp(1, 0, timeElapsed / disappeareTime);
            timeElapsed += Time.deltaTime;
            
            Color color = spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, alpha);
            spriteRenderer.color = color;
        }
        else Destroy(gameObject);
    }
    private void StartDisappeare()
    {
        collider.enabled = false;
        shouldDisappeare = true;
        alpha = spriteRenderer.color.a;
    }
}
