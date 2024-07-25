using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool checkpointHit;

    private void Start()
    {
        checkpointHit = false;
    }

    public void SetCheckpoint()
    {
        if (!checkpointHit)
        {
            GameManager.Instance.SetLastCheckpoint(gameObject.transform.position);
            checkpointHit = true;
        }
    }
}
