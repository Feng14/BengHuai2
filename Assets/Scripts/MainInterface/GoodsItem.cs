using UnityEngine;
using System.Collections;

// 物品（武器，衣服，技能……）
public class GoodsItem {
    public string atlas, sprite;

    public GoodsItem() { }

    public GoodsItem(string atlas, string sprite)
    {
        this.atlas = atlas;
        this.sprite = sprite;
    }
}
