using UnityEngine;

public class Interactor : MonoBehaviour
{
    IInteractable currentInteractable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gather Interactable object reference if applicable
        Debug.Log($"Current collision: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Interactable"))
        {
            currentInteractable = collision.gameObject.GetComponent<IInteractable>();
        }
        else if (collision.gameObject.CompareTag("CheckPoint"))
        {
            collision.gameObject.GetComponent<Checkpoint>().SetCheckpoint();
        }
        else if (collision.gameObject.CompareTag("Respawn"))
        {
            Actions.OnPlayerDied?.Invoke();
            GameManager.Instance.RespawnAtCheckpoint(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Null Interactable object reference if applicable
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            currentInteractable = null;
            Debug.Log("Current interactable = null");
        }
    }

    private void Update()
    {
        if (currentInteractable != null)
        {
            // Interact with selected Interactable objct
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentInteractable.Interact();
            }
        }
    }
}
