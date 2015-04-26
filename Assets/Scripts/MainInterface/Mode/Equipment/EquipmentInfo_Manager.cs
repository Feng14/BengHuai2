using UnityEngine;
using System.Collections;

// 附着在“装备详情”界面的管理器
public class EquipmentInfo_Manager : MonoBehaviour, Interface_Stack_Layer
{
    public enum Mode{View, ChangeWeapon};

    public object equipment;
    public UISprite[] stars;
    public UILabel equipmentName, weight, LV_Now, LV_Max, EXP_Next, type, attack, bullets, speed, introduce;
    public UISlider EXP_Slider;
    public UITexture image;
    public GameObject button_Selected;

    private EquipmentInfo_ModeInterface mode;
    public EquipmentInfo_ModeInterface mode_View;
    public EquipmentInfo_ModeInterface mode_ChangeEquipment;

    void Awake()
    {
        mode_View = new EquipmentInfo_Mode_View(this);
        mode_ChangeEquipment = new EquipmentInfo_Mode_ChangeEquipment(this);
        mode = mode_View;
    }

	// Use this for initialization
	void Start () {
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
        gameObject.SetActive(false);
        //UnRigisterEvent();
    }

    // 在重新被使用的时候调用
    public void OnReUse()
    {
        gameObject.SetActive(true);
        //RegisterEvent();
    }

    // 设置显示模式
    public void setMode(EquipmentInfo_ModeInterface mode)
    {
        this.mode = mode;
        this.mode.Init();
    }

    public void setMode(Mode newMode)
    {
        switch (newMode)
        {
            case Mode.View:
                mode = mode_View;
                //print("View");
                break;
            case Mode.ChangeWeapon:
                mode = mode_ChangeEquipment;
                //print("ChangeWeapon");
                break;
            default:
                mode = mode_View;
                //print("Default");
                break;
        }
        mode.Init();
    }

    // 显示装备信息
    public void showEquipmentInfo(object equipment)
    {
        this.equipment = equipment;
        // 是武器
        if (equipment.GetType() == new Weapon().GetType())
            showEquipmentInfo(equipment as Weapon);
    }
    public void showEquipmentInfo(Weapon weapon)
    {
        // 星星
        for (int i = 0; i < weapon.Star; i++)
            stars[i].spriteName = "Star_1";

        for (int i=weapon.Star-1; i<stars.Length; i++)
            stars[i].spriteName = "Star_2";

        // 装备名
        equipmentName.text = weapon.Name;
        // 负重
        //weight.text = weapon.Weight;
        // 等级
        //LV_Now.text = weapon.level;
        //LV_Max.text = weapon.maxLevel;
        // 类型
        type.text = weapon.Type.ToString();
        // 攻击力
        attack.text = weapon.Attack.ToString();
        // 子弹
        bullets.text = weapon.Bullets.ToString();
        // 速度
        speed.text = weapon.PeerTime.ToString();
        // 介绍
        introduce.text = "";
        // 升级所需经验
        //EXP_Slider
        //EXP_Next
        // 图片
        print(weapon.ImagePath);
        print(image == null);
        print(image.mainTexture);
        image.mainTexture = Resources.Load(weapon.ImagePath) as Texture;
        print(image.mainTexture);

    }

    // 点击“选择”装备事件
    public void selectEquipmentEvent()
    {
        GameRoot_Main.getSingleton<MessageManager_Equipment>().sendMessage_EquipmentInfo_Select(equipment);
        // 返回
        GameRoot_Main.getSingleton<MessageManager_Main>().sendMessage_TurnBack();
    }
}

// 模式接口
public interface EquipmentInfo_ModeInterface
{
    void Init();
}

// 模式父类
public abstract class EquipmentInfo_Mode : EquipmentInfo_ModeInterface
{
    public EquipmentInfo_Manager manager;

    public EquipmentInfo_Mode(EquipmentInfo_Manager manager)
    {
        this.manager = manager;
    }

    public abstract void Init();
}

// 模式查看
public class EquipmentInfo_Mode_View : EquipmentInfo_Mode
{

    public EquipmentInfo_Mode_View(EquipmentInfo_Manager manager) : base(manager){}

    public override void Init()
    {
        //Debug.Log("Mode_View");
        manager.button_Selected.SetActive(false);
    }
    
}

// 模式更换装备
public class EquipmentInfo_Mode_ChangeEquipment : EquipmentInfo_Mode
{

    public EquipmentInfo_Mode_ChangeEquipment(EquipmentInfo_Manager manager) : base(manager) { }

    public override void Init()
    {
        manager.button_Selected.SetActive(true);
    }

}