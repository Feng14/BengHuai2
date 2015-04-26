using UnityEngine;
using System.Collections;

// 战斗地图关卡信息
public class BattleStage
{
    public enum Mode {Break, Live};

    public UISprite image;
    public Vector3 position;
    public int level1, level2, consumePower;
    public string stageName, information, goal, iconPath;
    public GoodsItem[] profits;
    public Mode mode;


    public static void copy(BattleStage from, BattleStage to)
    {
        if (to.image != null)
            to.image.spriteName = from.iconPath;
        to.position = from.position;
        to.level1 = from.level1;
        to.level2 = from.level2;
        to.stageName = from.stageName;
        to.information = from.information;
        to.goal = from.goal;
        to.iconPath = from.iconPath;
        to.profits = from.profits;
        to.consumePower = from.consumePower;
    }
}
