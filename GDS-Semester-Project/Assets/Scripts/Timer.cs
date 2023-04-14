using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    public float timeLeft = 180.0f; 
    private Text timerText;
    public GameObject timerCanvas;
    public Text addTimeText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timerText = GetComponent<Text>();
        addTimeText.gameObject.SetActive(false);
    }

    void Update()
    {
        
        Vector3 cameraPos = Camera.main.transform.position;
        timerCanvas.transform.position = new Vector3(cameraPos.x, cameraPos.y + 5, timerCanvas.transform.position.z);

        
        addTimeText.transform.position = new Vector3(timerText.transform.position.x + timerText.preferredWidth + 65, timerText.transform.position.y, timerText.transform.position.z);

        timeLeft -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeLeft <= 0)
        {
            GameManager.Instance.TimeIsUp();
            timeLeft = 0f;
        }
    }

    public void AddTime(float seconds)
    {
        timeLeft += seconds;
        addTimeText.gameObject.SetActive(true);
        StartCoroutine(HideAddTimeText(2));
    }

    private IEnumerator HideAddTimeText(float delay)
    {
        yield return new WaitForSeconds(delay);
        addTimeText.gameObject.SetActive(false);
    }
}
