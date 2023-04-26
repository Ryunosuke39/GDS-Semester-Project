using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject keyAcquiredPanel;
    private bool isOpened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            isOpened = true;
            other.GetComponent<Player>().keyAcquired = true;
            keyAcquiredPanel.SetActive(true);
            Destroy(gameObject, 0.1f);
        }
    }
}
