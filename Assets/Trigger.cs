using UnityEngine;

public class Trigger : MonoBehaviour
{
    GameManager gameManager;
    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        gameManager.SingleSpawner(transform);
    }
    
}
