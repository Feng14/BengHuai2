using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// UI管理器
public class UIManager_InBattle : MonoBehaviour {
    public RockerManager rockerManager;
    public ShotItem weapon1, weapon2, weapon3;
    public UISlider HPSlider;
    public UILabel HPLabel, bulletLabel;

    public GameObject characterHP_Container;
    public GameObject HP_Prefab;

	// Use this for initialization
	void Start () {
	    // 添加武器
        List<Weapon> list = PlayerManager.instance.getWeaponsPossess();
        //print(PlayerPrefs.GetInt(PlayerManager.Key_Weapon1) + "  " + PlayerPrefs.GetInt(PlayerManager.Key_Weapon2) + "  " + PlayerPrefs.HasKey(PlayerManager.Key_Weapon3));

        if (PlayerPrefs.HasKey(PlayerManager.Key_Weapon1))
            weapon1.setWeapon(WeaponsStore.getWeaponFromStoreId(list, PlayerPrefs.GetInt(PlayerManager.Key_Weapon1)));

        if (PlayerPrefs.HasKey(PlayerManager.Key_Weapon2))
            weapon2.setWeapon(WeaponsStore.getWeaponFromStoreId(list, PlayerPrefs.GetInt(PlayerManager.Key_Weapon2)));

        if (PlayerPrefs.HasKey(PlayerManager.Key_Weapon3))
            weapon3.setWeapon(WeaponsStore.getWeaponFromStoreId(list, PlayerPrefs.GetInt(PlayerManager.Key_Weapon3)));

        RegisterEvent();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        UnRegisterEvent();
    }

    private void RegisterEvent()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().createMonsterEvent
            += new MessageManager_InBattle.CreateMonsterDelegrate(createMonsterEvent);
    }

    private void UnRegisterEvent()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().createMonsterEvent
            -= new MessageManager_InBattle.CreateMonsterDelegrate(createMonsterEvent);
    }

    // 添加新怪物事件（给怪物加上血条）
    private void createMonsterEvent(GameObject monsterObj, Monster monster, CharacterData characterData)
    {
        GameObject newHP = GameObject.Instantiate(HP_Prefab) as GameObject;
        newHP.transform.parent = characterHP_Container.transform;
        newHP.transform.localScale = Vector3.one;
        newHP.transform.localPosition = Vector3.zero;
        newHP.SetActive(true);

        newHP.GetComponent<HP_Slider>().ID = characterData.ID;

        newHP.GetComponent<UIFollowTarget>().target = monsterObj.transform.FindChild("HP");
    }

}
