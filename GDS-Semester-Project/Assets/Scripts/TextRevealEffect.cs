using System.Collections;
using TMPro;
using UnityEngine;

public class TextRevealEffect : MonoBehaviour
{
    public float delayBetweenCharacters = 0.1f;
    public float startDelay = 0.5f;

    private TMP_Text textMeshPro;
    private string fullText;

    private void Start()
    {
        textMeshPro = GetComponent<TMP_Text>();
        fullText = textMeshPro.text;
        textMeshPro.text = "";
        StartCoroutine(StartRevealTextWithDelay());
    }

    private IEnumerator StartRevealTextWithDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(RevealText());
    }

    private IEnumerator RevealText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            textMeshPro.text += fullText[i];
            yield return new WaitForSeconds(delayBetweenCharacters);
        }
    }
}
