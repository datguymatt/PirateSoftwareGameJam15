using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera m_camera;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject shadowSprite;
    [SerializeField] GameObject playerSprite;
    
    // how fast the camera lerps when switching
    [SerializeField] float switchSpeed;

    // the y pos lerps too fast so the switchSpeed will be multiplied by this
    [SerializeField] float ySwitchSpeedMultiplier;

    // how much to shrink the camera by
    [SerializeField] float cameraSizeReductionFactor;

    // stop lerp if within this distance
    [SerializeField] float stopSwitchLerpDist;

    float yStart;
    float zStart;
    float cameraSizeStart;
    float cameraSizeNew;

    void Start()
    {
        m_camera = GetComponent<Camera>();
        cameraSizeStart = m_camera.orthographicSize;

        // get the reduced camera size for later, otherwise it will keep shrinking if done in update
        cameraSizeNew = cameraSizeStart * cameraSizeReductionFactor;

        yStart = transform.position.y;
        zStart = transform.position.z;
    }

    void Update()
    {
        // If not in shadow mode
        if (!PlayerInfo.Instance.IsInShadowMode)
        {
            // LERP the camera size to the start size
            if (cameraSizeStart - m_camera.orthographicSize > stopSwitchLerpDist)
            {
                m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, cameraSizeStart, Time.deltaTime * switchSpeed);
            }
            else
            {
                m_camera.orthographicSize = cameraSizeStart;
            }

            // LERP the camera x pos to the player, keep y and z at the start pos
            if (playerTransform.position.x - transform.position.x > stopSwitchLerpDist)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerTransform.position.x, Time.deltaTime * switchSpeed), yStart, zStart);
            }
            else
            {
                transform.position = new Vector3(playerTransform.position.x, yStart, zStart);
            }
            
            // Set the player sprite to active
            playerSprite.SetActive(true);
        }
        else
        {
            // LERP camera size to new size
            if (m_camera.orthographicSize - cameraSizeNew > stopSwitchLerpDist)
            {
                m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, cameraSizeNew, Time.deltaTime * switchSpeed);
            }
            else
            {
                m_camera.orthographicSize = cameraSizeNew;
            }

            

            // LERP camera x pos to shadow, offset y pos down for camera size change, keep z at start
            if (transform.position.x - shadowSprite.transform.position.x > stopSwitchLerpDist && transform.position.y - (yStart - m_camera.orthographicSize) > stopSwitchLerpDist)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, shadowSprite.transform.position.x, Time.deltaTime * switchSpeed), Mathf.Lerp(transform.position.y, yStart - m_camera.orthographicSize, Time.deltaTime * switchSpeed * ySwitchSpeedMultiplier), zStart);
            }
            else
            {
                transform.position = new Vector3(shadowSprite.transform.position.x, yStart - m_camera.orthographicSize, zStart);
            }

            // Set the player sprite to inactive
            playerSprite.SetActive(false);
        }
        
    }
}
