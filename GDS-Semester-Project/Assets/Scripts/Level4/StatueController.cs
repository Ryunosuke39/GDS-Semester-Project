using UnityEngine;

public class StatueController : MonoBehaviour
{
    public DoorController doorController;

    private BoxCollider2D statueCollider;

    private void Start()
    {
        statueCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (doorController.keyAcquired)
        {
            statueCollider.enabled = false;
        }
        else
        {
            statueCollider.enabled = true;
        }
    }
}
