using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class MenuVisualSequencer : MonoBehaviour
{

    public MenuManager _menuManager;
    public Camera mainCamera;
    public TextMeshProUGUI _titleText;
    public Transform _titleTransform;

    //moon intensity
    public Light moonLight;
    public Material moonMaterial;
    public Color moonfadedColor;
    

    //ui elements
    public GameObject afterClickUI;


    [Header("Credit Intro Variables")]
    public Transform tdmCredit;
    public Transform nameCredit;



    [Header("Start Intro Sequence Variables")]
    public float introSeqDuration;

    [Header("Any Button Sequence Variables")]
    public float anyButtonClickedSeqDuration;

    [Header("Start Game Sequence Variables")]
    public float startGameSeqDuration;

    void Awake()
    {
        //subscribe to MenuManager Events
        _menuManager.AnyButtonPrompt += AnyButtonPrompt;
        _menuManager.AnyButtonClicked += AnyButtonClicked;
        _menuManager.StartGameClicked += StartGameIntro;
        moonMaterial.color = Color.white;

    } 
    //any button 
    public void AnyButtonPrompt()
    {
        //start light cycle animation - transition to dusk
   
    }
    public void AnyButtonClicked()
    {
        StartCoroutine(AnyButtonClickedSeq());
    }

    private IEnumerator AnyButtonClickedSeq()
    {
        yield return new WaitForSeconds(anyButtonClickedSeqDuration);
        afterClickUI.SetActive(true);
    }
    //end anybutton

    //start game
    public void StartGameIntro()
    {
        StartCoroutine(StartGameSeq());
    }   

    private IEnumerator StartGameSeq()
    {
        yield return new WaitForSeconds(startGameSeqDuration);
        _menuManager.LoadGame();
    }

}
