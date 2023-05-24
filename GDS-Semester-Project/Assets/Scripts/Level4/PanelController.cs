using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour
{
    // 这个数组应该包含你所有的面板
    public GameObject[] panels;

    // 这个变量用来保存当前显示的面板的隐藏操作
    private Coroutine currentHideCoroutine;

    // 这个函数隐藏一个特定的面板
    private IEnumerator HidePanel(GameObject panel)
    {
        // 等待3秒
        yield return new WaitForSeconds(3f);

        // 隐藏面板
        panel.SetActive(false);
    }

    // 这个函数显示一个特定的面板，并在3秒后隐藏它
    public void ShowPanel(int index)
    {
        // 先停止正在进行的隐藏操作
        if (currentHideCoroutine != null)
        {
            StopCoroutine(currentHideCoroutine);
        }

        // 隐藏所有面板
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }

        // 显示新的面板
        panels[index].SetActive(true);

        // 开始新的隐藏操作
        currentHideCoroutine = StartCoroutine(HidePanel(panels[index]));
    }
}
