using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using DG.Tweening;
using System.IO;
using Unity.VisualScripting;

public class TextCharRenderSystem : MonoBehaviour
{
    public string theInputTextLineOne;
    public string theInputTextLineTwo;
    public string theInputTextLineThree;
    public string theInputTextLineFour;
    public string theInputTextLineFive;
    //
    private char[] characterArray;
    public float charRenderTime;
    public Color fadeColor;
    public float fadeColorDuration;
    public TextMeshProUGUI textMeshProUGUI;

    public AudioSource textAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartTextRender();
    }

    public void StartTextRender()
    {
        //parse the input text into individual chars, save into the char array
        characterArray = theInputTextLineOne.ToCharArray();
        //start the coRout
        StartCoroutine(RenderCoRoutine());
    }

    private IEnumerator RenderCoRoutine()
    {
        //add each character to the text field of the TMP, in a for loop
        for (int i = 0; i < characterArray.Length; i++)
        {
            textMeshProUGUI.text += characterArray[i];
            yield return new WaitForSeconds(charRenderTime);
        }
        textAudioSource.Stop();
    }
}
