using UnityEngine;
using System.Collections;

// 关卡地图管理器（负责读入地图等）
public class Manager_BattleMap : Singleton<Manager_BattleMap>
{
    //public const string mapBasePath = "Prefabs.Interface_Battle.";
    public string[] mapPath = {"Map1", "Map2"};
    public int currentMap = 0;
    public GameObject battleMapStage;
    public MessageManager_BattleMap battleMapMessageManager;

    public override void Init() {
        battleMapStage = Resources.Load("StageItem") as GameObject;

        battleMapMessageManager = GameObject.Find("GameRoot").GetComponent<MessageManager_BattleMap>();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 动态读入地图
    public GameObject loadBattleMap(int number)
    {
        //Debug.Log("BattleMapManager : Load Map : " + mapPath[number]);
        return GameObject.Instantiate(Resources.Load(mapPath[number])) as GameObject;
    }

    public GameObject getCurrentBattleMap()
    {
        return loadBattleMap(currentMap);
    }

    // 获取上一张战斗关卡地图
    public GameObject getPreviousBattleMap()
    {
        currentMap = (--currentMap + mapPath.Length) % mapPath.Length;
        //Debug.Log("length: " + mapPath.Length + "    current: " + currentMap);
        return loadBattleMap(currentMap);
    }

    // 获取下一张战斗关卡地图
    public GameObject getNextBattleMap()
    {
        currentMap = ++currentMap % mapPath.Length;
        //Debug.Log("length: " + mapPath.Length + "    current: " + currentMap);
        return loadBattleMap(currentMap);
    }

    // 获取地图数量
    public int getBattleMapCount()
    {
        return mapPath.Length;
    }

    // 获取地图的关卡信息
    public BattleStage[] getBattleMapStages(int mapNumber)
    {
        return createSampleBattleMapStages();
    }

    // 建立一个样板关卡
    public BattleStage[] createSampleBattleMapStages()
    {
        BattleStage[] stages = new BattleStage[6];
        BattleStage s;
        for (int i = 0; i < 6; i++)
        {
            s = new BattleStage();
            stages[i] = s;
            s.level1 = 1;
            s.level2 = i + 1;
            s.consumePower = 2;
            s.stageName = "测试关卡 " + 1 + "-" + (i + 1);
            s.information = "尽情的屠杀吧!";
            s.goal = "杀死所有敌人";
            s.iconPath = "AdminIcon";
            s.profits = new GoodsItem[2];
            s.profits[0] = new GoodsItem("Rifle", "025_6");
            s.profits[1] = new GoodsItem("Rifle", "018_17");
        }
        stages[0].position = new Vector3(-250, -37, 3);
        stages[1].position = new Vector3(-227, 50, 3);
        stages[2].position = new Vector3(-126, 21, 3);
        stages[3].position = new Vector3(11, -29, 3);
        stages[4].position = new Vector3(109, 34, 3);
        stages[5].position = new Vector3(270, 13, 3);
        return stages;
    }

    //往地图内添加关卡图标
    public void setStageToMap(GameObject map, BattleStage[] stages)
    {
        GameObject prefab = Resources.Load("Prefabs/MainInterface/Main_Battle/StageItem") as GameObject;
        GameObject stageIcon;
        foreach (BattleStage stage in stages) {
            stageIcon = GameObject.Instantiate(prefab) as GameObject;
            //print(stageIcon.GetComponent<BattleStage>().image == null);
            stageIcon.GetComponent<BattleStageObj>().battleStage = stage;
            //BattleStage.copy(stage, stageIcon.GetComponent<BattleStageObj>().battleStage);
            stageIcon.transform.parent = map.transform;
            stageIcon.transform.localScale = Vector3.one;
            stageIcon.transform.localPosition = stage.position;
            stageIcon.GetComponent<BattleStageObj>().image.spriteName = stage.iconPath;


        }
    }
}

