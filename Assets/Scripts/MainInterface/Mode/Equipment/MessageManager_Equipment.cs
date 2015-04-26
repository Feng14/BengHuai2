using UnityEngine;
using System.Collections;

// 主界面-》装备消息管理器
public class MessageManager_Equipment : Singleton<MessageManager_Equipment>
{
    // 装备初始界面事件
    public delegate void EquipmentDelegate();
    // 点击仓库大按钮
    public event EquipmentDelegate editEquipment;
    // 点击仓库大按钮
    public event EquipmentDelegate storehouse;
    // 已载入仓库界面
    public event EquipmentDelegate storehouseLoadOver;
    
    // 编辑装备界面事件
    public delegate void EditEquipmentDelegate(int index);
    // 点击某个武器-》进入切换武器模式
    public event EditEquipmentDelegate changeWeapon;
    public event EditEquipmentDelegate changeWeaponOver;
    // 点击某个技能-》进入切换技能模式
    public event EditEquipmentDelegate changeSkill;
    public event EditEquipmentDelegate changeSkillOver;
    // 点击衣服-》进入切换衣服模式
    public event EditEquipmentDelegate changeCloth;
    public event EditEquipmentDelegate changeClothOver;

    // 仓库中装备位置的点击事件
    public delegate void EquipmentItemDelegate(EquipmentInfo_Manager.Mode mode, object equipment);
    public event EquipmentItemDelegate equipmentInfoMode;

    // 装备详情界面的事件
    public delegate void EquipmentInfoDelegate(object equipment);
    public event EquipmentInfoDelegate selectEquipment;



	// Use this for initialization
    public override void Init() { }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 发送点击"编辑装备”大按钮事件
    public void sendMessage_editEquipment()
    {
        if (editEquipment != null)
            editEquipment();
    }

    // 发送点击仓库大按钮事件
    public void sendMessage_Storehouse()
    {
        if (storehouse != null)
            storehouse();
    }

    // 发送仓库大按钮事件处理完毕事件
    public void sendMessage_StorehouseOver()
    {
        //Debug.Log("sendMessage_StorehouseOver");
        if (storehouseLoadOver != null)
            storehouseLoadOver();
    }

    // 发送修改武器事件
    public void sendMessage_ChangeWeapon(int index)
    {
        //Debug.Log("sendMessage_ChangeWeapon");
        if (changeWeapon != null)
            changeWeapon(index);
    }

    // 发送修改武器完成事件
    public void sendMessage_ChangeWeaponOver(int index)
    {
        //Debug.Log("sendMessage_StorehouseOver");
        if (changeWeaponOver != null)
            changeWeaponOver(index);
    }

    // 发送修改技能事件
    public void sendMessage_ChangeSkill(int index)
    {
        //Debug.Log("sendMessage_StorehouseOver");
        if (changeSkill != null)
            changeSkill(index);
    }

    // 发送修改技能完成事件
    public void sendMessage_ChangeSkillOver(int index)
    {
        //Debug.Log("sendMessage_StorehouseOver");
        if (changeSkillOver != null)
            changeSkillOver(index);
    }

    // 发送修改衣服事件
    public void sendMessage_ChangeCloth(int index)
    {
        //Debug.Log("sendMessage_StorehouseOver");
        if (changeCloth != null)
            changeCloth(index);
    }

    // 发送修改衣服完成事件
    public void sendMessage_ChangeClothOver(int index)
    {
        //Debug.Log("sendMessage_StorehouseOver");
        if (changeClothOver != null)
            changeClothOver(index);
    }


    // 点击仓库装备Item-----》打开装备详情界面
    public void sendMessage_ShowEquipmentInfo(EquipmentInfo_Manager.Mode mode, object equipment)
    {
        if (equipmentInfoMode != null)
            equipmentInfoMode(mode, equipment);
    }


    // 装备详情界面-》选择   事件
    public void sendMessage_EquipmentInfo_Select(object equipment)
    {
        if (selectEquipment != null)
            selectEquipment(equipment);
    }
}
