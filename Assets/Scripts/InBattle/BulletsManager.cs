using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 子弹管理器，负责管理子弹的碰撞事件
public class BulletsManager : Singleton<BulletsManager>
{


    public override void Init()
    {
    }

    void Start()
    {
        registerEvent();
    }

    void OnDestroy()
    {
        UnRegierterEvent();
    }

    private void registerEvent()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().bulletCollisionEvent
            += new MessageManager_InBattle.BulletCollisionDelegrate(BulletCollision);
    }

    private void UnRegierterEvent()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().bulletCollisionEvent
            -= new MessageManager_InBattle.BulletCollisionDelegrate(BulletCollision);
    }

    /// <summary>
    /// 处理子弹碰撞Monster事件
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="monster"></param>
    /// <param name="charaterData"></param>
    public void BulletCollision(Bullets bullet, GameObject monster, CharacterData charaterData)
    {
        Debug.Log("Manager BulletCollision");
        DealWithHurt(bullet, monster, charaterData);
        DealWithBulletCollision(bullet, monster, charaterData);
    }

    /// <summary>
    /// 处理子弹碰撞Player事件
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="player"></param>
    /// <param name="charaterData"></param>
    public void BulletCollision(Bullets bullet, Player player, CharacterData charaterData)
    {

    }

    /// <summary>
    /// (被打者）处理子弹伤害
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="monster"></param>
    /// <param name="charaterData"></param>
    private void DealWithHurt(Bullets bullet, GameObject monster, CharacterData charaterData)
    {
        float hurt = bullet.weapon.Attack;
        //Debug.Log(monster.name);
        //Debug.Log(monster.GetComponent<HP_Text_Manager>() == null);
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().SendMessage_MonsterHurtEvent(bullet, monster, charaterData, hurt);
        charaterData.HP_Current -= hurt;
        //monster.GetComponent<HP_Text_Manager>().showHurt((int)hurt);
    }

    /// <summary>
    /// 处理子弹碰撞事件
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="monster"></param>
    /// <param name="charaterData"></param>
    private void DealWithBulletCollision(Bullets bullet, GameObject monster, CharacterData charaterData)
    {
        switch (bullet.weapon.Type)
        {
            case EquipmentManager.WeaponType.Rifle:
                RifleBulletCollision(bullet, monster, charaterData);
                break;
        }
    }

    /// <summary>
    /// 处理子弹碰撞效果
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="monster"></param>
    /// <param name="charaterData"></param>
    private void RifleBulletCollision(Bullets bullet, GameObject monster, CharacterData charaterData)
    {
        bullet.gameObject.SetActive(false);
    }
}
