using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] bool flickering;
    [SerializeField] float onTime;
    [SerializeField] float offTime;
    bool off;
    float timer;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shadow") && PlayerInfo.Instance.IsInShadowMode)
        {
            Actions.OnPlayerAttacked.Invoke(damage);

        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        timer = onTime;
    }

    private void Update()
    {
        if (flickering)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (off)
                {
                    timer = onTime;
                    spriteRenderer.enabled = true;
                    polygonCollider.enabled = true;
                    off = false;
                }
                else
                {
                    timer = offTime;
                    spriteRenderer.enabled = false;
                    polygonCollider.enabled = false;
                    off = true;
                }
            }
        }
        

    }
}
