using UnityEngine;
using System.Collections;

// 附着在主界面-》下方容器上的管理器
public class Main_PanelBottom : MonoBehaviour
{
    private MessageManager_BattleMap messageManager_BattleMap;
    private GameObject mode_Battle;
    private GameObject mode_Equipment;

    // 界面栈
    private Interface_Stack interface_Stack;

	// Use this for initialization
    void Awake()
    {
        messageManager_BattleMap = GameRoot_Main.getSingleton<MessageManager_BattleMap>();
        interface_Stack = new Interface_Stack();

        mode_Battle = Resources.Load("Prefabs/MainInterface/Main_Battle/Mode_Battle") as GameObject;
        mode_Equipment = Resources.Load("Prefabs/MainInterface/Main_Equipment/Interface_Equipment") as GameObject;
    }

    void Start()
    {
        //Debug.Log("Start");
        //MessageManager.registerMessageResive(MessageListener_SelectFunction_Fight.className, this);
       // MessageManager.registerMessageResive(Message_SelectFunction_Fight.sample.GetType().ToString(), this);

        // 注册事件监听
        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_Mode
            += new MessageManager_Main.MessageDelegate_MainInterfaceMode(mainInterface_Mode);

        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_Mode_Battle
            += new MessageManager_Main.MessageDelegate_MainInterfaceMode(mainInterface_Mode_Battle);

        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_Mode_Equipment
            += new MessageManager_Main.MessageDelegate_MainInterfaceMode(mainInterface_Mode_Equipment);

        GameRoot_Main.getSingleton<MessageManager_Equipment>().editEquipment
            += new MessageManager_Equipment.EquipmentDelegate(mainInterface_Mode_Equipment_EditEquipment);

        //GameRoot_Main.getSingleton<MessageManager_Equipment>().storehouse
        //    += new MessageManager_Equipment.EquipmentDelegate(mainInterface_Mode_Equipment_Storehouse);

        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_TurnBack
            += new MessageManager_Main.MessageDelegate_MainInterfaceMode(ButtonEvent_TurnBack);

        GameRoot_Main.getSingleton<MessageManager_Equipment>().equipmentInfoMode
            += new MessageManager_Equipment.EquipmentItemDelegate(showEquipmentInfo);
        
        // 注册“取消监听”消息
        //GameRoot_Main.getSingleton<GameManager>().disRegisterMessage
        //    += new GameManager.SceneMessageDelegate(disRegisterMessage);
	}

    void OnDistroy()
    {
        // 注册事件监听
        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_Mode
            -= new MessageManager_Main.MessageDelegate_MainInterfaceMode(mainInterface_Mode);

        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_Mode_Battle
            -= new MessageManager_Main.MessageDelegate_MainInterfaceMode(mainInterface_Mode_Battle);

        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_Mode_Equipment
            -= new MessageManager_Main.MessageDelegate_MainInterfaceMode(mainInterface_Mode_Equipment);

        GameRoot_Main.getSingleton<MessageManager_Equipment>().editEquipment
            -= new MessageManager_Equipment.EquipmentDelegate(mainInterface_Mode_Equipment_EditEquipment);

        //GameRoot_Main.getSingleton<MessageManager_Equipment>().storehouse
        //    -= new MessageManager_Equipment.EquipmentDelegate(mainInterface_Mode_Equipment_Storehouse);

        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_TurnBack
            -= new MessageManager_Main.MessageDelegate_MainInterfaceMode(ButtonEvent_TurnBack);

        GameRoot_Main.getSingleton<MessageManager_Equipment>().equipmentInfoMode
            -= new MessageManager_Equipment.EquipmentItemDelegate(showEquipmentInfo);

        // 注册“取消监听”消息
        //GameObject.Find("GameRoot").GetComponent<GameManager>().disRegisterMessage
        //    -= new GameManager.SceneMessageDelegate(disRegisterMessage);

    }

	// Update is called once per frame
	void Update () {}

    // 主界面按键事件（战斗，物品，扭蛋……）


    //接收到“切换到某模式”消息后做出的反应
    public void mainInterface_Mode()
    {
        // 关闭所有界面
        //Debug.Log("关闭所有界面");
        interface_Stack.clear();
        //Debug.Log(interface_Stack.getLength());
        //foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MainInterface_Mode"))
        //{
        //    obj.GetComponent<Event_ModeBattle>().beforeDestory();
        //    Destroy(obj);
        //}
    }

    //接收到“战斗”消息后做出的反应
    public void mainInterface_Mode_Battle()
    {
        //Debug.Log("Event_Main_PanelBottom:按键事件：战斗");

        // 在Panel_Bottom载入战斗界面
        addInterface(mode_Battle);
        //Debug.Log("mode_BattleInstance : " + mode_BattleInstance.transform.position);

        // 通知显示一张关卡地图
        //Debug.Log("Send Message: ShowMap");
        messageManager_BattleMap.sendMessage_ShowMap();
    }

    //接收到“装备”消息后做出的反应
    public void mainInterface_Mode_Equipment()
    {
        //Debug.Log("Event_Main_PanelBottom:按键事件：装备");

        // 在Panel_Bottom载入装备界面
        addInterface(mode_Equipment);

        //currentInterface = 
        //Debug.Log("mode_BattleInstance : " + mode_BattleInstance.transform.position);

    }

    //接收到“装备->编辑装备”消息后做出的反应
    public void mainInterface_Mode_Equipment_EditEquipment()
    {
        //print("1111111111111111");
        // 在Panel_Bottom载入编辑装备界面
        GameObject editEquipemtnInterface = Resources.Load("Prefabs/Main_Equipment/Equipment_Edit") as GameObject;
        addInterface(editEquipemtnInterface);
    }

    //接收到“装备->仓库”消息后做出的反应
    //public void mainInterface_Mode_Equipment_Storehouse()
    //{
    //    Debug.Log("Event_Main_PanelBottom:按键事件：装备->仓库");

    //    // 在Panel_Bottom载入仓库界面
    //    GameObject storehouseInterface = Resources.Load("Prefabs/MainInterface/Main_Equipment/Equipment_Storehouse") as GameObject;
    //    addInterface(storehouseInterface);
    //    GameRoot_Main.getSingleton<MessageManager_Equipment>().sendMessage_StorehouseOver();

    //    //currentInterface = 
    //    //Debug.Log("mode_BattleInstance : " + mode_BattleInstance.transform.position);

    //}

    //接收到“装备Item 被点击”消息后做出的反应,显示装备详情界面
    public void showEquipmentInfo(EquipmentInfo_Manager.Mode mode, object equipment)
    {
        //Debug.Log("装备Item 被点击");
        GameObject equipmentInroInterface = Resources.Load("Prefabs/MainInterface/Main_Equipment/Interface_EquipmentInfor") as GameObject;
        
        EquipmentInfo_Manager manager = addInterface(equipmentInroInterface).GetComponent<EquipmentInfo_Manager>();
        manager.setMode(mode);
        manager.showEquipmentInfo(equipment);
    }

    // 在容器中加入新界面
    private GameObject addInterface(GameObject prefab)
    {
        GameObject interfaceObject = GameObject.Instantiate(prefab) as GameObject;
        interfaceObject.transform.parent = this.transform;
        interfaceObject.transform.localScale = Vector3.one;
        interfaceObject.transform.localPosition = Vector3.zero;

        interfaceObject.GetComponent<UISprite>().width = (int)gameObject.GetComponent<UISprite>().localSize.x;
        interfaceObject.GetComponent<UISprite>().height = (int)gameObject.GetComponent<UISprite>().height;

        interfaceObject.SetActive(true);
        //Debug.Log(interfaceObject.name);
        interfaceObject.SendMessage("OnCreate");

        if (!interface_Stack.ifEmpty())
        {
            interface_Stack.getStackTop().SendMessage("OnPause");
            interface_Stack.getStackTop().SetActive(false);
        }
        interface_Stack.push(interfaceObject);
        //Debug.Log("stack: " + interface_Stack.getLength());

        return interfaceObject;
    }

    // 从栈中弹出一个界面到显示窗口
    private GameObject popInterfaceToView()
    {
        //Debug.Log("POP");
        GameObject go = interface_Stack.pop();
        go.SendMessage("OnPop");
        GameObject.Destroy(go);

        go = interface_Stack.getStackTop();
        //Debug.Log(go.name);
        go.SetActive(true);
        go.SendMessage("OnReUse");

        return go;
    }

    // “返回”按钮事件
    private void ButtonEvent_TurnBack()
    {
        popInterfaceToView();
        //Debug.Log(interface_Stack.getStackTop().name);
        //interface_Stack.getStackTop().SetActive(true);
    }

/**
    bool MessageListener.onResiveMessage(Message message)
    

        Debug.Log("MessageListener   receive Message");
        return true;
    }

    bool MessageListener_SelectFunction_Fight.onResiveMessage(Message message)
    {
        //Mode_Battle
        Debug.Log("receive Message");
        //Transform battleInterface = transform.Find("Mode_Battle");
        //battleInterface.active = true;

        return true;
    }
 **/
}
