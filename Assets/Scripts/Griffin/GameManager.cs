using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Vector3 lastCheckpoint;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void SetLastCheckpoint(Vector3 checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    public void RespawnAtCheckpoint(GameObject player)
    {
        player.transform.position = lastCheckpoint;
        Actions.OnPlayerSpawned?.Invoke();
    }
}
