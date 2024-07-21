using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    // This script assumes that a button will match to a door 1:1, but we can change this later if needed
    [SerializeField] GameObject door;
    public void Interact()
    {
        door.gameObject.SetActive(false);
        Debug.Log("Door opened");
        // Switch button sprite for visual feedback
        // Play interaction sound for audio feedback
    }
}
