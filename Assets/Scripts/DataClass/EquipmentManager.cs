using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using LitJson;
using System.Collections.Generic;
using System.Xml.Serialization;

// 装备管理
public class EquipmentManager
{
    public enum EquipmentType { Weapon, Skill, close };
    public static EquipmentManager Instance = new EquipmentManager();

    //public const string weaponPath = Application.dataPath + "/Resources/Weapons.data";

    public enum WeaponType { Rifle, RPG, ShotGun, Pistol, Put, Sword};

    public static string getWeaponPath()
    {
        return Application.dataPath + "/Resources/Weapons.data";
    }

    public static string getPlayerWeaponPath()
    {
        return Application.dataPath + "/Resources/PlayerWeapons.data";
    }

    private EquipmentManager() { }

    // 保存武器列表文件
    //public void saveWeaponsManager(WeaponsStore weaponsManager, string path)
    //{
    //    List<Weapon> list = new List<Weapon>();
    //    FileStream fs = new FileStream(path, FileMode.Create);
    //    XmlSerializer formatter = new XmlSerializer(typeof(WeaponsStore));
    //    formatter.Serialize(fs, list);
    //}

    // 从文件获取武器列表
    //public WeaponsStore getWeaponManager(string path)
    //{
    //    FileStream fs = new FileStream(path, FileMode.Open);
    //    XmlSerializer formatter = new XmlSerializer(typeof(WeaponsStore));
    //    return (WeaponsStore)formatter.Deserialize(fs);

    //    //FileStream weaponFile = new FileStream(weaponPath, FileMode.Open);
    //    //StreamReader sr = new StreamReader(weaponFile);
    //    //string data = sr.ReadToEnd();

    //    //WeaponsManager weaponManager = JsonMapper.ToObject <WeaponsManager>(data);
    //    //sr.Close();
    //    //sr.Dispose();
    //    //return weaponManager;
    //}
}
