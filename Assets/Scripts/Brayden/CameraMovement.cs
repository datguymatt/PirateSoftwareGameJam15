using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera m_camera;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform shadowTransform;
    [SerializeField] GameObject playerSprite;
    float yStart;
    float zStart;
    bool shadow;

    void Start()
    {
        m_camera = GetComponent<Camera>();
        yStart = transform.position.y;
        zStart = transform.position.z;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            shadow = !shadow;
        }

        if (!shadow)
        {
            transform.position = new Vector3(playerTransform.position.x, yStart, zStart);
            m_camera.orthographicSize = 2.2f;
            playerSprite.SetActive(true);
        }
        else
        {
            transform.position = new Vector3(shadowTransform.position.x, -1.1f, zStart);
            playerSprite.SetActive(false);
            m_camera.orthographicSize = 1.1f;
        }
        
    }
}
