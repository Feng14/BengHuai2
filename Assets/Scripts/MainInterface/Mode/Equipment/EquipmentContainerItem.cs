using UnityEngine;
using System.Collections;

// 仓库里面每个装备位
public class EquipmentContainerItem : MonoBehaviour {
    public EquipmentManager.EquipmentType equipmentType;
    public object equipment = null;
    public GameObject selectedIcon;
    public UITexture imageTexture;
    public UILabel label_Weight, label_LV;

    private EquipmentItem_ModeInterface mode;
    public EquipmentItem_ModeInterface mode_Null;
    public EquipmentItem_ModeInterface mode_View;
    public EquipmentItem_ModeInterface mode_ChangeWeapon;


	// Use this for initialization
    void Awake()
    {
        //selectedIcon.SetActive(false);
        
        mode_Null = new EquipmentItem_Mode_Null(this);
        mode_View = new EquipmentItem_Mode_View(this);
        mode_ChangeWeapon = new EquipmentItem_Mode_ChnageWeapon(this);
        mode = mode_View;
    }

	void Start () {
        //imageTexture.mainTexture = Resources.Load("Weapons/StorehouseItem/Rifle_1_1") as Texture;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 设置图标

    public void setWeaponIcon(Weapon weapon)
    {
        setEquipmentIcon(weapon.ItemPath);
    }

    // 设置图标
    public void setEquipmentIcon(string path)
    {
        //Debug.Log("setEquipmentIcon : " + path);
        imageTexture.mainTexture = Resources.Load(path) as Texture;
        //imageTexture.mainTexture = Resources.Load("Weapons/StorehouseItem/Rifle_1_1") as Texture;
    }

    // 是否被选中
    public void SetSelected(bool selected){
        selectedIcon.SetActive(selected);
    }

    // 修改点击事件模式
    public void setClickEventMode(EquipmentItem_ModeInterface mode)
    {
        //Debug.Log(mode.GetType());
        this.mode = mode;
        //Debug.Log(this.mode.GetType());

    }

    // 点击事件
    public void OnClicked()
    {
        //Debug.Log(mode.GetType());
        mode.Click();
    }
}

// 模式接口
public interface EquipmentItem_ModeInterface
{
    void Click();
}

// 模式父类
public abstract class EquipmentItem_Mode : EquipmentItem_ModeInterface
{
    public EquipmentContainerItem manager;

    public EquipmentItem_Mode(EquipmentContainerItem manager)
    {
        this.manager = manager;
    }

    public abstract void Click();
}

// 没反应
public class EquipmentItem_Mode_Null : EquipmentItem_Mode
{

    public EquipmentItem_Mode_Null(EquipmentContainerItem manager) : base(manager) { }

    public override void Click() { }
}

// 进入查看装备详情
public class EquipmentItem_Mode_View : EquipmentItem_Mode
{

    public EquipmentItem_Mode_View(EquipmentContainerItem manager) : base(manager) {}

    public override void Click()
    {
        GameRoot_Main.getSingleton<MessageManager_Equipment>().sendMessage_ShowEquipmentInfo(EquipmentInfo_Manager.Mode.View, manager.equipment);
    }
}

// 进入修改武器模式
public class EquipmentItem_Mode_ChnageWeapon : EquipmentItem_Mode
{

    public EquipmentItem_Mode_ChnageWeapon(EquipmentContainerItem manager) : base(manager) { }

    public override void Click()
    {
        GameRoot_Main.getSingleton<MessageManager_Equipment>().sendMessage_ShowEquipmentInfo(EquipmentInfo_Manager.Mode.ChangeWeapon, manager.equipment);
    }
}

