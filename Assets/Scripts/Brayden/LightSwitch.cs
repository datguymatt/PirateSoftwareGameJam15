using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] List<GameObject> lights = new List<GameObject>();

    public void Interact()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(!light.activeInHierarchy);
        }
    }
}
