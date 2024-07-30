using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] float cameraOffset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cameraMovement.yOffset += cameraOffset;
            cameraMovement.shadowYOffset += cameraOffset;
        }
    }
}
