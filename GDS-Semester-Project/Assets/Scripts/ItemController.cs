using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 在这里可以添加玩家获得道具的逻辑（例如增加生命值、攻击力等）

            Destroy(gameObject);
        }
    }
public enum ItemType
{
    Item0,
    Item1,
    Item2
}

public ItemType itemType;

}
