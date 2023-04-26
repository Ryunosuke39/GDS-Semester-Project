using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    public GameObject keyAcquiredPanel;
    public Button okButton;
    private bool isOpened = false;

    private void Start()
    {
        okButton.onClick.AddListener(CloseKeyAcquiredPanel);
        keyAcquiredPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            isOpened = true;
            DoorController doorController = FindObjectOfType<DoorController>();
            if (doorController != null)
            {
                doorController.keyAcquired = true;
            }
            keyAcquiredPanel.SetActive(true);
        }
    }

    private void CloseKeyAcquiredPanel()
    {
        keyAcquiredPanel.SetActive(false);
        Destroy(gameObject, 0.1f);
    }
}
