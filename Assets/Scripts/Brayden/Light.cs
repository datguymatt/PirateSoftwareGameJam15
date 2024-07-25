using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shadow") && PlayerInfo.Instance.IsInShadowMode)
        {
            Actions.OnPlayerAttacked.Invoke(damage);
        }
    }
}
