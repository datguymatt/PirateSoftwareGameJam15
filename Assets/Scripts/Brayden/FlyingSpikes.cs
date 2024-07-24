using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSpikes : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;

    private void Update()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Actions.OnPlayerAttacked?.Invoke(damage);
            Destroy(gameObject);
        }
    }
}
