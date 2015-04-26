using UnityEngine;
using System.Collections;

public class PlayerManager_InBattle : Singleton<PlayerManager_InBattle>
{
    public enum State{Stand, Run, Die};
    public GameObject player;

    public float movintSpeed = 10f;
    private bool moving;
    private float orientAngle;
    private bool orientLeft = false;

    public override void Init()
    {
        moving = false;
        player = GameObject.Instantiate((Resources.Load("Prefabs/InBattle/Player") as GameObject)) as GameObject;
        player.GetComponent<CharacterData>().HP_Current = 4000;
        player.SetActive(true);
    }

	// Use this for initialization
	void Start () {
        registerEvent();
	}
	
	// Update is called once per frame
	void Update () {
        Moving();
	}

    void OnDestroy()
    {
        UnRegisterEvent();
    }

    // 注册事件监听
    private void registerEvent()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().rockerEvent
            += new MessageManager_InBattle.RockedDelegrate(RockerEvent);

        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().shotItemPressEvent
            += new MessageManager_InBattle.ShotItemDelegrate(PressWeapon);
    }

    // 反注册事件
    private void UnRegisterEvent()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().rockerEvent
            -= new MessageManager_InBattle.RockedDelegrate(RockerEvent);

        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().shotItemPressEvent
            -= new MessageManager_InBattle.ShotItemDelegrate(PressWeapon);
    }

    // 设置状态
    public void setState(State state)
    {
        switch (state)
        {
            case State.Stand:
                //player.transform.FindChild("Kiana").GetComponent<SkeletonAnimation>().animation = ;
                player.transform.FindChild("Kiana").GetComponent<SkeletonAnimation>().AnimationName = "Stand";
                //player.transform.FindChild("Kiana").GetComponent<SkeletonAnimation>().
                break;
            case State.Run:
                player.transform.FindChild("Kiana").GetComponent<SkeletonAnimation>().AnimationName = "Run";
                break;
            case State.Die:
                //player.transform.FindChild("Kiana").GetComponent<SkeletonAnimation>().animation = ;
                break;
        }
    }

    // 移动
    private void Moving()
    {
        if (moving)
        {
            if (player != null)
            {
                float x = -movintSpeed * Mathf.Sin(orientAngle),
                    y = movintSpeed * Mathf.Cos(orientAngle);

                // 动画、行走 方向相反
                if ((x < 0 && !orientLeft) || (x > 0 && orientLeft))
                {
                    Vector3 playerScale = player.transform.localScale;
                    player.transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);

                    orientLeft = x < 0 ? true : false;
                }

                player.GetComponent<Rigidbody>().velocity = new Vector3(x, 0.1f, y);
            }
            GameRoot_InBattle.getSingleton<MessageManager_InBattle>().SendMessage_PlayerMoveEvent(player.transform.position);
        }
        else
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void RockerEvent(float angle, bool active)
    {
        moving = active;
        if (moving)
            setState(State.Run);
        else
            setState(State.Stand);

        // 将360°进制的角改为π进制的角
        orientAngle = angle / 180 * Mathf.PI;
    }


    // 按下“开枪”按键事件
    private void PressWeapon(int index, Weapon weapon)
    {
        //Debug.Log("Shot");
        if (weapon == null)
            return;

        GameObject bulletGO = Pool.Instance.getObjFromPool(Pool.PoolType.Bullet);
        Bullets bullet = bulletGO.GetComponent<Bullets>();

        // 设置子弹与武器相关的属性
        switch (weapon.Type)
        {
            case EquipmentManager.WeaponType.Rifle :
                //bullet.bulletGameObject.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Effect/Ammo_001_mip_1") as Texture;
                // 发射者
                bullet.sender = Bullets.Sender.Player;
                // 位置
                bulletGO.transform.position = player.transform.FindChild("Point_Rifle").position;
                // 方向
                bulletGO.transform.rotation = new Quaternion(0, 0, orientLeft ? 0 : 180, 0);
                //bullet.bulletGameObject.transform.LookAt(orientLeft ? GameRoot_InBattle.getSingleton<BulletsManager>().go_Left.transform.position
                //    : GameRoot_InBattle.getSingleton<BulletsManager>().go_Right.transform.position);
                // 速度
                bulletGO.GetComponent<Rigidbody>().velocity = new Vector3(weapon.BulletSpeed * (orientLeft ? -1 : 1),
                    0, Random.Range(-weapon.BulletAngle, weapon.BulletAngle));
                // 自动销毁协程
                StartCoroutine(DestroyBulletCoroutine(3, bullet));
                
                break;
        }
        // 子弹图片
        bulletGO.GetComponent<MeshRenderer>().material = Resources.Load(weapon.BulletPath) as Material;

        bullet.weapon = weapon;
        // 发送“子弹发射成功”消息
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().SendMessage_ShotSuccessfulEvent(index, weapon);
    }

    IEnumerator DestroyBulletCoroutine(float second, Bullets bullet)
    {
        yield return new WaitForSeconds(second);

        bullet.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.gameObject.SetActive(false);
    }
}
