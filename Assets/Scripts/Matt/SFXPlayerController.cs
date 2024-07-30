using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SFXPlayerController : MonoBehaviour
{

    public bool isWalking = false;
    public bool isRunning = false;


    private AudioSource audioChannel;

    public AudioClip[] leftFootClips;
    public AudioClip[] rightFootClips;
    public AudioClip[] jumpClips;
    public AudioClip[] landClips;

    //parameters for tempo
    public float stepLength = 0.3f;

    public void Start()
    {
        audioChannel = GetComponent<AudioSource>();
    }

    public AudioClip RandomSelect(string typeOfClip)
    {

        switch(typeOfClip)
        {
            case "leftFoot":
                return leftFootClips[Random.Range(0, leftFootClips.Length)];
            case "rightFoot":
                return rightFootClips[Random.Range(0, leftFootClips.Length)];
            case "jump":
                return jumpClips[Random.Range(0, leftFootClips.Length)];
            case "land":
                return landClips[Random.Range(0, leftFootClips.Length)];
            default:
                return null;
        }
    }

    public void StartWalking()
    {
        if (!audioChannel.isPlaying && !isWalking)
        {
            isWalking = true;
            StartCoroutine(WalkSFXPlay());
        }
    }

    public void StopWalking()
    {
        audioChannel.Stop();
        isWalking = false;
        StopCoroutine(WalkSFXPlay());

    }
    public void Jump()
    {
        audioChannel.PlayOneShot(RandomSelect("jump"));
    }

    public void Land()
    {
        audioChannel.PlayOneShot(RandomSelect("land"));
    }

    public IEnumerator WalkSFXPlay()
    {
        while(isWalking)
        {
            if (isWalking)
            {
                audioChannel.PlayOneShot(RandomSelect("leftFoot"));
                yield return new WaitForSeconds(stepLength);
            }
            else
            {
                audioChannel.Stop();
            }

            if (isWalking)
            {
                audioChannel.PlayOneShot(RandomSelect("rightFoot"));
                yield return new WaitForSeconds(stepLength);
            }
            else
            {
                audioChannel.Stop();
            }
        }
        audioChannel.DOFade(0, 0.2f);

        yield return null;
    }
}
