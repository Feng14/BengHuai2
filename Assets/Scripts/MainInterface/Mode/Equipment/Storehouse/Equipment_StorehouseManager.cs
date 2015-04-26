using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 仓库管理器（附着在Equipment_Storehouse预制上）
public class Equipment_StorehouseManager : MonoBehaviour, Interface_Stack_Layer
{
    public GameObject itemContainer;
    public List<GameObject> equipmentContainerItems = null;
    public UILabel countInformation;

    private List<Weapon> weaponList;

	// Use this for initialization
    void Awake()
    {
        //Debug.Log("register");
        RegisterEvent();
    }

    void OnDestroy()
    {
        UnRigisterEvent();
    }

    // 注册事件监听
    private void RegisterEvent()
    {
        //GameRoot_Main.getSingleton<MessageManager_Equipment>().storehouseLoadOver
        //    += new MessageManager_Equipment.EquipmentDelegate(addEquipemnt);
    }

    private void UnRigisterEvent()
    {
        //GameRoot_Main.getSingleton<MessageManager_Equipment>().storehouseLoadOver
        //    -= new MessageManager_Equipment.EquipmentDelegate(addEquipemnt);
    }

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
        UnRigisterEvent();
    }

    // 在重新被使用的时候调用
    public void OnReUse()
    {
        gameObject.SetActive(true);
        RegisterEvent();
    }

    // 添加装备
    public void addEquipemnt()
    {
        //Debug.Log("addEquipemnt");
        int count = PlayerManager.instance.getStoreSize();
        addEquipmentContainerItem(count);
        addEquipemntToConatiner();
    }

    // 在仓库容器中添加一个个位置
    private void addEquipmentContainerItem(int count)
    {
        // 清除旧的
        foreach (GameObject g in equipmentContainerItems)
            GameObject.Destroy(g);

        equipmentContainerItems = new List<GameObject>();
        //Debug.Log("StoreSize: " + count);
        GameObject prefab = Resources.Load("Prefabs/MainInterface/Main_Equipment/EquipmentElement") as GameObject;
        GameObject go;

        for (int i = 0; i < count; i++)
        {
            go = GameObject.Instantiate(prefab) as GameObject;
            go.AddComponent<UIDragScrollView>();
            equipmentContainerItems.Add(go);
            //print(itemContainer == null);
            go.transform.parent = itemContainer.transform;
            //go.transform.localScale = Vector3.one;
            //go.transform.localPosition = Vector3.zero;
        }


        //Debug.Log(itemContainer.GetComponent<BoxCollider>().size);
        //int overflow = count / 6 + (count % 6 > 0 ? 1 : 0);
        //Debug.Log(overflow * 130);
        //itemContainer.GetComponent<BoxCollider>().center = new Vector3(0, -56*(overflow-3), 0);
        //Vector3 size = itemContainer.GetComponent<BoxCollider>().size;
        //itemContainer.GetComponent<BoxCollider>().size = new Vector3(size.x, overflow * 130, size.z);
        //Debug.Log(size);
        //Debug.Log(itemContainer.GetComponent<BoxCollider>().size);
    }

    // 往每个位置添加玩家拥有的装备
    private void addEquipemntToConatiner()
    {
        // 添加武器
        addWeaponToConatiner();
        // 添加技能

        // 添加衣服

    }

    // 将有武器的位置的点击事件设置为“观察”模式
    private void addItemClickMode()
    {
        EquipmentContainerItem itemManager;
        foreach (GameObject go in equipmentContainerItems)
        {
            itemManager = go.GetComponent<EquipmentContainerItem>();
            if (itemManager.equipment != null)
                itemManager.setClickEventMode(itemManager.mode_View);
        }
    }

    // 往每个位置添加玩家拥有的武器
    private void addWeaponToConatiner()
    {
        weaponList = PlayerManager.instance.getWeaponsPossess();
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (equipmentContainerItems.Count < i)
                break;

            equipmentContainerItems[i].GetComponent<EquipmentContainerItem>().equipment = weaponList[i];
            equipmentContainerItems[i].GetComponent<EquipmentContainerItem>().setWeaponIcon(weaponList[i]);
        }
        countInformation.text = weaponList.Count + "/" + equipmentContainerItems.Count;
    }

    // 进入更换武器模式
    public void changeWeaponMode(int weaponIndex)
    {
        foreach (GameObject go in equipmentContainerItems)
            Destroy(go);

        equipmentContainerItems = new List<GameObject>();

        if (weaponList == null)
            weaponList = PlayerManager.instance.getWeaponsPossess();

        addEquipmentContainerItem(weaponList.Count);
        addWeaponToConatiner();

        int weaponIndex1 = PlayerPrefs.HasKey(PlayerManager.Key_Weapon1) ? PlayerPrefs.GetInt(PlayerManager.Key_Weapon1) : -1,
            weaponIndex2 = PlayerPrefs.HasKey(PlayerManager.Key_Weapon2) ? PlayerPrefs.GetInt(PlayerManager.Key_Weapon2) : -1,
            weaponIndex3 = PlayerPrefs.HasKey(PlayerManager.Key_Weapon3) ? PlayerPrefs.GetInt(PlayerManager.Key_Weapon3) : -1;

        int storeIndex;
        EquipmentContainerItem itemManager;
        // 对每个装备Item进行处理
        foreach (GameObject go in equipmentContainerItems){
            itemManager = go.GetComponent<EquipmentContainerItem>();
            storeIndex = (go.GetComponent<EquipmentContainerItem>().equipment as Weapon).StorehouseId;

            if (storeIndex == weaponIndex1 || storeIndex == weaponIndex2 || storeIndex == weaponIndex3)
                // 是已装备的武器，就把小勾勾打上
                itemManager.SetSelected(true);

            // 添加点击事件（打开武器详情界面，并设置模式）
            //Debug.Log("changeMode");
            itemManager.setClickEventMode(itemManager.mode_ChangeWeapon);
        }
    }
}
