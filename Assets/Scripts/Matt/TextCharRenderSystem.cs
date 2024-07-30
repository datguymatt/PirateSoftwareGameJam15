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
    public float fadeAudioTime = 0.5f;

    //events
    private MenuManager menuManager;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        //subscribe to events
        menuManager.StartGameClicked += StartGameClicked;
    }

    public void StartGameClicked()
    {
        textAudioSource.Play();
        textAudioSource.volume = 0f;
        //fade in audio
        textAudioSource.DOFade(1, fadeAudioTime);
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
        //fade out audio
        textAudioSource.DOFade(0, fadeAudioTime);
        textAudioSource.Stop();
    }
}
