using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

// 附着在主界面下方战斗模块容器上
public class Main_Mode_Battle : MonoBehaviour, Interface_Stack_Layer
{
    private MessageManager_BattleMap messageManager_BattleMap;

    // 地图左右两个位置的点，用于挪入（出）地图的位置设定
    public GameObject mapPosition_Left;
    public GameObject mapPosition_Right;

    // 页码
    public UILabel pageLabel;

	// Use this for initialization
	void Awake () {
        // 初始化
        messageManager_BattleMap = GameRoot_Main.getSingleton<MessageManager_BattleMap>();

        //Debug.Log("Event_ModeBattle  Register event : showBattleMap");

        registerEvent();
    }

    // 注册事件监听
    private void registerEvent()
    {
        // 注册接收显示战斗关卡地图消息
        messageManager_BattleMap.showBattleMap += new MessageManager_BattleMap.BattleMapDelegate(showBattleMap);

        //GameObject.Find("MainInterface_BottomContainer").GetComponent<Event_Main_PanelBottom>().showBattleMap
        //    += new Event_Main_PanelBottom.BattleMapDelegate(showBattleMap);

        // 注册接受往左翻页消息
        messageManager_BattleMap.pageLeftEvent += new MessageManager_BattleMap.ModeBattleMessageDelegate(this.pageLeft);
        //pageLeftEvent += new ModeBattleMessageDelegate(pageLeft);

        // 注册接受往右翻页消息
        messageManager_BattleMap.pageRightEvent += new MessageManager_BattleMap.ModeBattleMessageDelegate(pageRight);
        //pageRightEvent += new ModeBattleMessageDelegate(pageRight);

        // 注册地图移出结束消息
        messageManager_BattleMap.mapMoveOutOver += new MessageManager_BattleMap.MapMoveMessageDelegate(moveOutOver);

        // 注册点击战斗关卡消息
        messageManager_BattleMap.clickStage += new MessageManager_BattleMap.BattleStageMessageDelegate(battleStage_Ready);

        // 注册点击"闯关"消息
        messageManager_BattleMap.changeToBreakMode += new MessageManager_BattleMap.BattleStageMessageDelegate(changeToBreakMode);

        // 注册点击"生存"消息
        messageManager_BattleMap.changeToLiveMode += new MessageManager_BattleMap.BattleStageMessageDelegate(changeToLiveMode);

        // 注册“取消监听”消息
        //GameRoot_Main.getSingleton<GameManager>().disRegisterMessage += new GameManager.SceneMessageDelegate(disRegisterMessage);
    }

    // 取消监听事件
    private void UnRigisterEvent()
    {
        //Debug.Log("111111111111111111111111");
        // 取消注册接收显示战斗关卡地图消息
        messageManager_BattleMap.showBattleMap -= new MessageManager_BattleMap.BattleMapDelegate(showBattleMap);

        // 取消注册接受往左翻页消息
        messageManager_BattleMap.pageLeftEvent -= new MessageManager_BattleMap.ModeBattleMessageDelegate(this.pageLeft);

        // 取消注册接受往右翻页消息
        messageManager_BattleMap.pageRightEvent -= new MessageManager_BattleMap.ModeBattleMessageDelegate(pageRight);

        // 取消注册地图移出结束消息
        messageManager_BattleMap.mapMoveOutOver -= new MessageManager_BattleMap.MapMoveMessageDelegate(moveOutOver);

        // 取消注册点击战斗关卡消息
        messageManager_BattleMap.clickStage -= new MessageManager_BattleMap.BattleStageMessageDelegate(battleStage_Ready);

        // 取消注册点击"闯关"消息
        messageManager_BattleMap.changeToBreakMode -= new MessageManager_BattleMap.BattleStageMessageDelegate(changeToBreakMode);

        // 取消注册点击"生存"消息
        messageManager_BattleMap.changeToLiveMode -= new MessageManager_BattleMap.BattleStageMessageDelegate(changeToLiveMode);
    }

    void OnDestroy()
    {
        UnRigisterEvent();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // 在创建时调用
    public void OnCreate()
    {
        gameObject.SetActive(true);
    }

    // 塞入时要调用的方法
    public void OnPush()
    {
        gameObject.SetActive(false);
    }

    // 弹出时要调用的方法（清除）
    public void OnPop()
    {
    }

    // 被暂停隐藏起来时调用
    public void OnPause()
    {
        UnRigisterEvent();
    }
    
    // 在重新被使用的时候调用
    public void OnReUse()
    {
        gameObject.SetActive(true);
        registerEvent();
    }

    // 在本脚本所在对象被Destroy之前，要把所有注册的消息接受取消掉时调用
    //public void beforeDestory()
    //{
        //messageManager_BattleMap.showBattleMap -= new MessageManager_BattleMap.BattleMapDelegate(showBattleMap);

        //GameObject.Find("MainInterface_BottomContainer").GetComponent<Event_Main_PanelBottom>().showBattleMap
        //    -= new Event_Main_PanelBottom.BattleMapDelegate(showBattleMap);
    //}

    // 显示关卡地图（刚打开此界面时）
    private void showBattleMap()
    {
        //Debug.Log("showBattleMap222222222222222");

        StartCoroutine(loadMapCoroutine(GameRoot_Main.getSingleton<Manager_BattleMap>().currentMap));
        //loadMap(GameRoot_Main.getSingleton<Manager_BattleMap>().currentMap);
    }

    // 创建载入地图协程
    IEnumerator loadMapCoroutine(int mapNumber)
    //private void loadMap(int mapNumber)
    {
        //Debug.Log("coroutine");
        // 删除已有子地图
        try
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "BattleMap")
                    Destroy(transform.GetChild(i));
            }
        } catch(Exception e){
            Debug.Log(e);
        }

        // 挪入关卡地图
        Manager_BattleMap battleMapManager = GameRoot_Main.getSingleton<Manager_BattleMap>();


        GameObject map = battleMapManager.getCurrentBattleMap();
        initialiseBattleMap(map);
        yield return map;
        //Debug.Log("Load Map Over");
        map.SetActive(true);
        //Debug.Log("1:" + map.transform.localPosition + "     " + mapPosition_Right.transform.localPosition);
        //map.transform.localPosition = mapPosition_Right.transform.localPosition;
        //map.transform.localScale = Vector3.one;
        //Debug.Log("2:" + map.transform.localPosition);

        moveMap_Into(map, false);

        yield break;
    }


    // 初始化关卡地图
    private GameObject initialiseBattleMap(GameObject map)
    {
        map.transform.parent = this.transform;
        map.transform.localScale = Vector3.one;
        map.transform.localPosition = Vector3.zero;
        map.GetComponent<UISprite>().width = (int)gameObject.GetComponent<UISprite>().localSize.x;
        map.GetComponent<UISprite>().height = (int)gameObject.GetComponent<UISprite>().localSize.y;

        // 在地图内设置关卡
        Manager_BattleMap battleMapManager = GameRoot_Main.getSingleton<Manager_BattleMap>();
        BattleStage[] stages = battleMapManager.getBattleMapStages(battleMapManager.currentMap);
        battleMapManager.setStageToMap(map, stages);

        return map;
    }

    // 挪入战斗关卡地图
    private void moveMap_Into(GameObject map, bool fromLeft)
    {
        map.transform.localPosition = (fromLeft ? mapPosition_Left : mapPosition_Right).transform.localPosition;
        map.transform.DOMoveX(this.transform.position.x, 0.5f).onComplete = delegate()
        {
            messageManager_BattleMap.sendMessage_MapMoveInOver(map);

            // 改变下面翻页部分显示的页码
            pageLabel.text = (GameRoot_Main.getSingleton<Manager_BattleMap>().currentMap + 1) + " / " + GameRoot_Main.getSingleton<Manager_BattleMap>().getBattleMapCount();
        };
        //map.transform.DOMoveX((fromLeft ? mapPosition_Left : mapPosition_Right).transform.localPosition.x, 0.5f);

        /**
        TweenPosition tween = GameObject.Instantiate(fromLeft ? mapMoveIn_FromLeft : mapMoveIn_FromRight) as TweenPosition;
        map.AddComponent(tween);
        map.AddComponent()
        **/
        
        /**
        int x = 943;
        if (fromLeft)
            x = -x;

        //Debug.Log("parent: " + transform.name + "   " + transform.position);
        //map.transform.localPosition = new Vector3(x, 0, 2);
        //map.transform.position = new Vector3(500, 0, 2);
        //Debug.Log("map : " + map.transform.position);
        //map.transform.SetParent(transform);
        //mapCurrent = map;
        //iTween.MoveTo(map, new Vector3(0, 0, 2), 2.0f);
        //iTween.MoveTo(gameObject, new Vector3(100, 0, 1), 1.0f);
        //iTween.moveTo(map, 2.0f, 0f, new Vector3(0, 0, 2), iTween.EasingType.easeInCubic, "destoryMap", "destoryMap");
        //iTween.moveTo(map, 1.0f, 0f, 0, 0, 2, iTween.EasingType.easeInCirc, "destoryMap", map);
        //iTween.MoveTo(map, iTween.Hash("position", new Vector3(0, 0, 2), "time", 1.0f, "oncomplete", "destoryMap", "concompletetarget", this));
        //Debug.Log("map : " + map.transform.position);
         **/
    }

    // 挪出战斗关卡地图
    private void moveMap_Out(GameObject map, bool toLeft)
    {
        Debug.Log("Move Out");
        map.transform.DOMoveX((toLeft ? mapPosition_Left : mapPosition_Right).transform.position.x, 0.5f).onComplete = delegate()
        {
            messageManager_BattleMap.sendMessage_MapMoveOutOver(map);
        };
        /**
        TweenPosition tween = map.GetComponent<TweenPosition>();
        tween.ResetToBeginning();
        tween.from = mapContainer_Center.transform.localPosition;
        tween.to = (toLeft ? mapContainer_Left : mapContainer_Right).transform.localPosition;
        tween.enabled = true;
        **/
        
        /**
        int x = 943;
        if (toLeft)
            x = -x;

        map.transform.localPosition = new Vector3(0, 0, 2);
        //iTween.moveTo(map, 1.0f, 0f, 0, 0, map.layer);
        //iTween.moveTo(map, 1.0f, 0f, new Vector3(x, 0, 2), iTween.EasingType.easeInCubic, "destoryMap", "destoryMap");
        **/
    }

    // 移出动画结束
    public void moveOutOver(GameObject map)
    {
        Debug.Log("Move Out Over");
        Destroy(map);
    }

    private void destoryMap(GameObject map)
    {
        Debug.Log("DestroyMap");
        Destroy(map);
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    if (transform.GetChild(i).tag == "BattleMap" && transform.GetChild(i) != mapCurrent)
        //        Destroy(transform.GetChild(i));
        //}
    }

    // 往左翻页事件
    private void pageLeft()
    {
        // 移出
        GameObject obj;
        for (int i = 0; i < transform.childCount; i++)
        {
            obj = transform.GetChild(i).gameObject;
            if (obj.tag == "BattleMap")
                moveMap_Out(obj, true);
        }

        // 移入
        GameObject newMap = GameRoot_Main.getSingleton<Manager_BattleMap>().getNextBattleMap();
        initialiseBattleMap(newMap);
        moveMap_Into(newMap, false);
        //battlePage = battlePage / pageCount + 1;
        //moveIntoMap(createBattleMap(battlePage), false);
    }

    // 往右翻页事件
    private void pageRight(){
        // 移出
        GameObject obj;
        for (int i = 0; i < transform.childCount; i++)
        {
            obj = transform.GetChild(i).gameObject;
            if (obj.tag == "BattleMap")
                moveMap_Out(obj, false);
        }
        // 移入
        GameObject newMap = GameRoot_Main.getSingleton<Manager_BattleMap>().getPreviousBattleMap();
        initialiseBattleMap(newMap);
        moveMap_Into(newMap, true);
    }

    // 往左翻页按钮被按
    public void pageLeftButtonDown()
    {
        Debug.Log("pageLeftButtonDown");
        messageManager_BattleMap.sendMessage_PageLeft();
    }

    // 往右翻页按钮被按
    public void pageRightButtonDown()
    {
        messageManager_BattleMap.sendMessage_PageRight();
    }

    // 打开战斗关卡信息界面
    private void battleStage_Ready(BattleStage battleStage)
    {
        //Debug.Log("battleStage_Ready");
        // 关掉同类窗口
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    if (transform.GetChild(i).name == "battleStage_Ready")
        //        Destroy(transform.GetChild(i));
        //}

        // 添加战斗准备界面
        GameObject readyInterface = GameObject.Instantiate(Resources.Load("Prefabs/MainInterface/Battle_Ready")) as GameObject;
        readyInterface.transform.parent = transform;
        readyInterface.transform.localScale = Vector3.one;
        readyInterface.transform.localPosition = Vector3.zero;
        readyInterface.GetComponent<UISprite>().width = gameObject.GetComponent<UISprite>().width;
        readyInterface.GetComponent<UISprite>().height = gameObject.GetComponent<UISprite>().height;

        //print(11111);
        //print(readyInterface.GetComponent<BattleInfoManager>().battleStage == null);
        readyInterface.GetComponent<BattleInfoManager>().battleStage = battleStage;
        //BattleStage.copy(battleStage, readyInterface.GetComponent<BattleInfoManager>().battleStage);

        readyInterface.transform.FindChild("Name").GetComponent<UILabel>().text = battleStage.stageName;
        readyInterface.transform.FindChild("Information").GetComponent<UILabel>().text = battleStage.information;
        readyInterface.transform.FindChild("Goal").GetComponent<UILabel>().text = battleStage.goal;
        readyInterface.transform.FindChild("Use Power").transform.FindChild("Power Consume").GetComponent<UILabel>().text = battleStage.consumePower+"";
        //readyInterface.transform.FindChild("Infrmation").GetComponent<UILabel>().text = battleStage.information;
        //readyInterface.transform.FindChild("Goal").GetComponent<UILabel>().text = battleStage.goal;



        //Debug.Log(2);
        readyInterface.SetActive(true);
    }

    // 修改为“闯关”模式
    private void changeToBreakMode(BattleStage battleStage)
    {
        Transform t = transform.FindChild("Battle_Ready").FindChild("Mode");
        t.FindChild("Break").GetComponent<UISprite>().spriteName = "ButtonDown_mip_0";
        t.FindChild("Live").GetComponent<UISprite>().spriteName = "ButtonUp_mip_0";

        battleStage.mode = BattleStage.Mode.Break;
    }

    // 修改为“生存”模式
    private void changeToLiveMode(BattleStage battleStage)
    {
        Transform t = transform.FindChild("Battle_Ready").FindChild("Mode");
        t.FindChild("Break").GetComponent<UISprite>().spriteName = "ButtonUp_mip_0";
        t.FindChild("Live").GetComponent<UISprite>().spriteName = "ButtonDown_mip_0";

        battleStage.mode = BattleStage.Mode.Live;
    }
}
