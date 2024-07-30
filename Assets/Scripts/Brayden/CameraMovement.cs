using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera m_camera;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject shadowSprite;
    [SerializeField] GameObject playerSprite;
    [SerializeField] Animator playerAnimator;

    // how fast the camera lerps when switching
    [SerializeField] float switchSpeed;

    // the y pos lerps too fast so the switchSpeed will be multiplied by this
    [SerializeField] float ySwitchSpeedMultiplier;

    // how much to shrink the camera by
    [SerializeField] float cameraSizeReductionFactor;

    // stop lerp if within this distance
    [SerializeField] float stopSwitchLerpDist;

    [HideInInspector] public float yOffset;
    public float shadowYOffset;

    float yStart;
    float zStart;
    float cameraSizeStart;
    float cameraSizeNew;
    bool isInShadowMode;
    bool doneLerping;
    [SerializeField] bool disableShadowSprite;

    private void OnEnable()
    {
        Actions.OnPlayerModeChange += OnPlayerModeChange;
    }

    private void OnDisable()
    {
        Actions.OnPlayerModeChange -= OnPlayerModeChange;
    }

    void Start()
    {
        m_camera = GetComponent<Camera>();
        cameraSizeStart = m_camera.orthographicSize;

        // get the reduced camera size for later, otherwise it will keep shrinking if done in update
        cameraSizeNew = cameraSizeStart * cameraSizeReductionFactor;

        yStart = transform.position.y;
        zStart = transform.position.z;
        yOffset = 0f;
    }

    void OnPlayerModeChange()
    {
        isInShadowMode = !isInShadowMode;
        if (doneLerping)
        {
            doneLerping = false;
        }
    }

    void Update()
    {
        // If not in shadow mode
        if (!isInShadowMode)
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
            if (Vector2.Distance(playerSprite.transform.position, new Vector2(transform.position.x, playerSprite.transform.position.y)) > stopSwitchLerpDist && !doneLerping)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerSprite.transform.position.x, Time.deltaTime * switchSpeed), yStart + yOffset, zStart);
                //Debug.Log("lerping " + Vector2.Distance(playerSprite.transform.position, transform.position));
            }
            else
            {
                //Debug.Log("not");
                transform.position = new Vector3(playerSprite.transform.position.x, yStart + yOffset, zStart);
                doneLerping = true;
            }
            // Play switch fx
            if (PlayerInput.Instance.GetSwitchInput())
            {
                playerAnimator.Play("Base Layer.SwitchEffectReversed");
            }

            // Set the player sprite to active
            playerSprite.SetActive(true);
            if (disableShadowSprite)
            {
                shadowSprite.SetActive(false);
            }
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
            if (Vector2.Distance(new Vector2(transform.position.x, shadowSprite.transform.position.y), shadowSprite.transform.position) > stopSwitchLerpDist && !doneLerping)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, shadowSprite.transform.position.x, Time.deltaTime * switchSpeed), Mathf.Lerp(transform.position.y, yStart - m_camera.orthographicSize + shadowYOffset, Time.deltaTime * switchSpeed * ySwitchSpeedMultiplier), zStart);
            }
            else
            {
                transform.position = new Vector3(shadowSprite.transform.position.x, yStart - m_camera.orthographicSize + shadowYOffset, zStart);
                doneLerping = true;
            }
            // Play switch fx
            if (PlayerInput.Instance.GetSwitchInput())
            {
                playerAnimator.Play("Base Layer.SwitchEffect");
            }

            // Set the player sprite to inactive
            playerSprite.SetActive(false);
            shadowSprite.SetActive(true);
        }

    }
}
