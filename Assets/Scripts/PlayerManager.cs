using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

// 管理用户信息
public class PlayerManager : MonoBehaviour {
    public const string Key_Weapon1 = "Weapon1";
    public const string Key_Weapon2 = "Weapon2";
    public const string Key_Weapon3 = "Weapon3";

    public const string Key_Skill1 = "Skill1";
    public const string Key_Skill2 = "Skill2";
    public const string Key_Skill3 = "Skill3";


    //public const string PlayerWeaponPath = "D:/Unity/BengHuai2/Assets/Resources/Data/WeaponsPossess.xml";
    //public const string PlayerWeaponPath = Application.dataPath + "/Resources/Data/WeaponsPossess";

    public static PlayerManager instance;
    public static GameObject playerManagerObj;

    public static void init(){
        playerManagerObj = new GameObject("PlayerManager");
        instance = playerManagerObj.AddComponent<PlayerManager>();
        DontDestroyOnLoad(instance);
    }

    private PlayerManager(){

    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    // 玩家拥有的武器文件地址
    public static string getPlayerWeaponPossessPath()
    {
        return Application.dataPath + "/Resources/Data/WeaponsPossess.data";
    }

    // 仓库大小
    public int getStoreSize()
    {
        if (!PlayerPrefs.HasKey("StoreSize"))
            PlayerPrefs.SetInt("StoreSize", 30);

        //PlayerPrefs.SetInt("StoreSize", 20);
        return PlayerPrefs.GetInt("StoreSize");
    }

    // 拥有所有武器
    public List<Weapon> getWeaponsPossess()
    {
        WeaponsStore weaponStore = xmlToObject <WeaponsStore> (getPlayerWeaponPossessPath());
        return weaponStore.weaponList;
    }

    // 将xml转化为对象
    public T xmlToObject<T>(string path)
    {
        //Debug.Log(path);
        FileStream fs = new FileStream(path, FileMode.Open);
        XmlSerializer formatter = new XmlSerializer(typeof(T));
        T t = (T)formatter.Deserialize(fs);
        fs.Close();
        return t;
    }
}
