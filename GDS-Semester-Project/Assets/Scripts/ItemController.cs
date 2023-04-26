using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public enum ItemType
    {
        Item0,
        Item1,
        Item2
    }

    public ItemType itemType;
    public float healthAmount = 30f;
    public float boostDuration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 在这里可以添加玩家获得道具的逻辑（例如增加生命值、攻击力等）
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                switch (itemType)
                {
                    case ItemType.Item0:
                        Debug.Log("Player picked up Item0");
                        other.GetComponent<Player>().AddHealth(healthAmount);
                        break;
                    case ItemType.Item1:
                        Debug.Log("Player picked up Item1");
                        other.GetComponent<Player>().BoostFireRate(boostDuration);
                        break;
                    case ItemType.Item2:
                        Debug.Log("Player picked up Item2");
                        other.GetComponent<Player>().BoostSpeed(boostDuration);
                        break;
                    // 处理其他道具类型的逻辑（如Item1和Item3）
                }
            }
            Destroy(gameObject);
        }
    }
}
