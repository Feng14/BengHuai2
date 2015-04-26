using UnityEngine;
using System.Collections;

// 附着在“装备”界面的管理器
public class Main_Mode_Equipment : MonoBehaviour, Interface_Stack_Layer
{
    public Equipment_StorehouseManager storehouseManager;
    public Equipment_EditEquipmentManager editEquipmentManager;
    public MyButton editEquipmentButton, storehouseButton;

    private bool canChangeMode = true;

	// Use this for initialization
	void Start () {
        editEquipmentButton.selectedImage = "HierachyButtons_Yellow_Down";
        editEquipmentButton.unSelectedImage = "HierachyButtons_Yellow_Up";
        storehouseButton.selectedImage = "HierachyButtons_Blue_Down";
        storehouseButton.unSelectedImage = "HierachyButtons_Blue_Up";

        changeToStorehouseMode();
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    //void OnDestroy()
    //{
    //    UnRegisterEvent();
    //}

    // 注册事件监听
    private void RegisterEvent()
    {
        // 修改武器事件
        GameRoot_Main.getSingleton<MessageManager_Equipment>().changeWeapon
            += new MessageManager_Equipment.EditEquipmentDelegate(ChangeEquipmentMode);

        GameRoot_Main.getSingleton<MessageManager_Equipment>().selectEquipment
            += new MessageManager_Equipment.EquipmentInfoDelegate(changeEquipmentOver);
    }

    // 反注册事件
    private void UnRegisterEvent()
    {
        GameRoot_Main.getSingleton<MessageManager_Equipment>().changeWeapon
            -= new MessageManager_Equipment.EditEquipmentDelegate(ChangeEquipmentMode);

        GameRoot_Main.getSingleton<MessageManager_Equipment>().selectEquipment
            -= new MessageManager_Equipment.EquipmentInfoDelegate(changeEquipmentOver);
    }

    // 在创建时调用
    public void OnCreate()
    {
        gameObject.SetActive(true);
        RegisterEvent();
    }

    // 塞入时要调用的方法
    public void OnPush()
    {
        gameObject.SetActive(false);
    }

    // 弹出时要调用的方法（清除）
    public void OnPop()
    {
        UnRegisterEvent();
    }

    // 被暂停隐藏起来时调用
    public void OnPause()
    {
        //UnRegisterEvent();
    }

    // 在重新被使用的时候调用
    public void OnReUse()
    {
        gameObject.SetActive(true);
        //RegisterEvent();
    }

    // 进入选择 武器/技能 模式（不能跳转到编辑界面）
    private void ChangeEquipmentMode(int index)
    {
        //print("ChangeEquipmentMode");
        changeToStorehouseMode();
        storehouseManager.changeWeaponMode(index);

        canChangeMode = false;
    }

    //选择武器结束
    private void changeEquipmentOver(object equipment)
    {
        // 退出选择 武器/技能 模式
        canChangeMode = true;
        changeToEditEquipmentMode();
        storehouseManager.addEquipemnt();
    }


    // 切换到仓库模式
    public void changeToStorehouseMode()
    {
        if (!canChangeMode)
            return;
        if (storehouseManager == null)
        {
            GameObject storehouse = GameObject.Instantiate(Resources.Load("Prefabs/MainInterface/Main_Equipment/Equipment_Storehouse") as GameObject) as GameObject;
            storehouse.transform.parent = transform;
            storehouse.transform.localScale = Vector3.one;
            storehouse.transform.localPosition = Vector3.zero;
            storehouse.GetComponent<UISprite>().width = gameObject.GetComponent<UISprite>().width;
            storehouse.GetComponent<UISprite>().height = gameObject.GetComponent<UISprite>().height;

            storehouseManager = storehouse.GetComponent<Equipment_StorehouseManager>();

            // 通知添加装备图标到容器中
            storehouseManager.SendMessage("addEquipemnt");
            //GameRoot_Main.getSingleton<MessageManager_Equipment>().send
        }
        storehouseManager.OnReUse();

        if (editEquipmentManager != null)
        {
            editEquipmentManager.SendMessage("OnPause");
        }

        editEquipmentButton.setSelected(false);
        storehouseButton.setSelected(true);

    }

    // 切换到编辑装备模式
    public void changeToEditEquipmentMode()
    {
        if (!canChangeMode)
            return;
        if (editEquipmentManager == null)
        {
            GameObject editEqumpment = GameObject.Instantiate(Resources.Load("Prefabs/MainInterface/Main_Equipment/Equipment_Edit") as GameObject) as GameObject;
            editEqumpment.transform.parent = transform;
            editEqumpment.transform.localScale = Vector3.one;
            editEqumpment.transform.localPosition = new Vector3(50, 0, 0);
            editEqumpment.GetComponent<UISprite>().width = gameObject.GetComponent<UISprite>().width - 100;
            editEqumpment.GetComponent<UISprite>().height = gameObject.GetComponent<UISprite>().height;

            editEquipmentManager = editEqumpment.GetComponent<Equipment_EditEquipmentManager>();

            // 通知更新装备列表
            editEquipmentManager.OnCreate();
            //GameRoot_Main.getSingleton<MessageManager_Equipment>().send
        }
        else
            editEquipmentManager.OnReUse();

        if (storehouseManager != null)
        {
            storehouseManager.OnPause();
        }

        editEquipmentButton.setSelected(true);
        storehouseButton.setSelected(false);

    }
}
