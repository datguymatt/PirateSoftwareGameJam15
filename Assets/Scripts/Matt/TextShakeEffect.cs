using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextShakeEffect : MonoBehaviour
{
    public float normalShakeDuration;
    public float tensionShakeDuration;
    public float strength;
    public int vibrato;
    public float randomness;

    public bool hasTension;
    // Start is called before the first frame update
    void Start()
    {
        ShakeTextLoop();
    }

    public void ShakeTextLoop()
    {
        if (hasTension)
        {
            transform.DOShakeRotation(tensionShakeDuration, strength, vibrato, randomness, true).SetLoops(10000);
            transform.DOShakePosition(tensionShakeDuration, strength, vibrato, randomness, false, true).SetLoops(10000);
        }
        else
        {
            transform.DOShakeRotation(normalShakeDuration, strength, vibrato, randomness, true).SetLoops(10000);
            transform.DOShakePosition(normalShakeDuration, strength, vibrato, randomness, false, true).SetLoops(10000);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
