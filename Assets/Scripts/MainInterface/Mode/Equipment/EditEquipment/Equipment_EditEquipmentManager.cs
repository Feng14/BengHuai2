using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 附着在“编辑装备”界面的管理器
public class Equipment_EditEquipmentManager : MonoBehaviour, Interface_Stack_Layer
{
    private List<Weapon> weaponList;

    public GameObject cloth, weapon1, weapon2, weapon3, skill1, skill2, skill3;
    public UITexture clothTexture, weaponTexture1, weaponTexture2, weaponTexture3, skillTexture1, skillTexture2, skillTexture3;
    public UILabel weightLabel, forceLabel, HPLabel;

    private string equipmentChanged;

	// Use this for initialization
	void Start () {

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
        GameRoot_Main.getSingleton<MessageManager_Equipment>().selectEquipment
            += new MessageManager_Equipment.EquipmentInfoDelegate(changeEquipmentOver);
    }

    private void UnRegisterEvent()
    {
        GameRoot_Main.getSingleton<MessageManager_Equipment>().selectEquipment
            -= new MessageManager_Equipment.EquipmentInfoDelegate(changeEquipmentOver);
    }

    // 在创建时调用
    public void OnCreate()
    {
        RegisterEvent();
        gameObject.SetActive(true);
        InitEquipment();
    }

    // 塞入时要调用的方法
    public void OnPush()
    {
        UnRegisterEvent();
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
        gameObject.SetActive(false);
        //UnRigisterEvent();
    }

    // 在重新被使用的时候调用
    public void OnReUse()
    {
        gameObject.SetActive(true);
        //RegisterEvent();
    }

    // 初始化装备
    private void InitEquipment()
    {
        //PlayerPrefs.SetInt("Weapon1", 3);
        // 衣服

        // 武器
        weaponList = PlayerManager.instance.getWeaponsPossess();
        if (PlayerPrefs.HasKey("Weapon1"))
        {
            Weapon weapon1 = WeaponsStore.getWeaponFromStoreId(weaponList, PlayerPrefs.GetInt("Weapon1"));
            //print(weapon1 == null);
            if (weapon1 != null)
            {
                //print(WeaponsStore.getWeaponItemPath(weapon1.ItemPath));
                //print(Resources.Load(WeaponsStore.getWeaponItemPath(weapon1.ItemPath)) as Texture);
                weaponTexture1.mainTexture = Resources.Load(weapon1.ItemPath) as Texture;
            }
        }
        if (PlayerPrefs.HasKey("Weapon2"))
        {
            Weapon weapon2 = WeaponsStore.getWeaponFromStoreId(weaponList, PlayerPrefs.GetInt("Weapon2"));
            if (weapon2 != null)
            {
                weaponTexture2.mainTexture = Resources.Load(weapon2.ItemPath) as Texture;
            }
        }
        if (PlayerPrefs.HasKey("Weapon3"))
        {
            Weapon weapon3 = WeaponsStore.getWeaponFromStoreId(weaponList, PlayerPrefs.GetInt("Weapon3"));
            if (weapon3 != null)
            {
                weaponTexture3.mainTexture = Resources.Load(weapon3.ItemPath) as Texture;
            }
        }

        // 技能
    }

    // 点击装备图标--》进入更换装备模式
    public void changeWeapon1()
    {
        equipmentChanged = PlayerManager.Key_Weapon1;
        GameRoot_Main.getSingleton<MessageManager_Equipment>().sendMessage_ChangeWeapon(1);
    }
    public void changeWeapon2()
    {
        equipmentChanged = PlayerManager.Key_Weapon2;
        GameRoot_Main.getSingleton<MessageManager_Equipment>().sendMessage_ChangeWeapon(2);
    }
    public void changeWeapon3()
    {
        equipmentChanged = PlayerManager.Key_Weapon3;
        GameRoot_Main.getSingleton<MessageManager_Equipment>().sendMessage_ChangeWeapon(3);
    }

    // 在选好要更换的武器时调用（在装备详情界面发出消息），保存数据
    private void changeEquipmentOver(object equipment)
    {
        if (equipment.GetType() == new Weapon().GetType())
            PlayerPrefs.SetInt(equipmentChanged, (equipment as Weapon).StorehouseId);

        InitEquipment();
    }
}
