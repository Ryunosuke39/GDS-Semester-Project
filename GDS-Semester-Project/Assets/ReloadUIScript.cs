using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReloadUIScript : MonoBehaviour
{
    private Image reloadUi;
    public GameObject rotatePoint;
    private Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        reloadUi = GetComponent<Image>();
        shooting = rotatePoint.GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        //reloadUi.fillAmount = 
        reloadUi.fillAmount = shooting.Firetimer/shooting.timeBetweenFiring;
    }
}
