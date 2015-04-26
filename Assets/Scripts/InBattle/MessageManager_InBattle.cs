using UnityEngine;
using System.Collections;

// 战斗中的消息中转器
public class MessageManager_InBattle : Singleton<MessageManager_InBattle>
{
    public delegate void RockedDelegrate(float angle, bool active);
    public RockedDelegrate rockerEvent;

    // 创建怪物事件
    public delegate void CreateMonsterDelegrate(GameObject monsterObj, Monster monster, CharacterData characterData);
    public CreateMonsterDelegrate createMonsterEvent;

    // 点击“开枪”按钮事件
    public delegate void ShotItemDelegrate(int index, Weapon weapon);
    public ShotItemDelegrate shotItemPressEvent;
    public ShotItemDelegrate shotItemReleaseEvent;
    public ShotItemDelegrate shotSuccessfulEvent;

    // 玩家移动事件
    public delegate void PlayerMoveDelegrate(Vector3 position);
    public PlayerMoveDelegrate playerMoveEvent;

    // 子弹触碰事件
    public delegate void BulletCollisionDelegrate(Bullets bullet, GameObject monster, CharacterData charaterData);
    public BulletCollisionDelegrate bulletCollisionEvent;

    // 怪物受伤事件
    public delegate void MonsterHurtDelegrate(Bullets bullet, GameObject monster, CharacterData charaterData, float hurt);
    public MonsterHurtDelegrate monsterHurtEvent;

    public override void Init()
    {
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 发送创建怪物事件
    public void SendMessage_CreateMonsterEvent(GameObject monsterObj, Monster monster, CharacterData characterData)
    {
        if (createMonsterEvent != null)
            createMonsterEvent(monsterObj, monster, characterData);
    }

    // 发送摇杆事件
    public void SendMessage_RockedEvent(float angle, bool active)
    {
        if (rockerEvent != null)
            rockerEvent(angle, active);
    }

    // 发送开枪按钮被按事件
    public void SendMessage_ShotItemPressEvent(int index, Weapon weapon)
    {
        if (shotItemPressEvent != null)
            shotItemPressEvent(index, weapon);
    }

    // 发送开枪按钮松开事件
    public void SendMessage_ShotItemReleaseEvent(int index, Weapon weapon)
    {
        if (shotItemReleaseEvent != null)
            shotItemReleaseEvent(index, weapon);
    }

    // 发送开枪成功事件
    public void SendMessage_ShotSuccessfulEvent(int index, Weapon weapon)
    {
        if (shotSuccessfulEvent != null)
            shotSuccessfulEvent(index, weapon);
    }

    // 发送玩家移动事件
    public void SendMessage_PlayerMoveEvent(Vector3 position)
    {
        if (playerMoveEvent != null)
            playerMoveEvent(position);
    }

    // 发送子弹碰撞事件
    public void SendMessage_BulletCollisionEvent(Bullets bullet, GameObject monster, CharacterData charaterData)
    {
        if (bulletCollisionEvent != null)
            bulletCollisionEvent(bullet, monster, charaterData);
    }

    // 发送怪物受伤事件
    public void SendMessage_MonsterHurtEvent(Bullets bullet, GameObject monster, CharacterData charaterData, float hurt)
    {
        if (monsterHurtEvent != null)
            monsterHurtEvent(bullet, monster, charaterData, hurt);
    }
}
