using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// 武器库（如所有武器，个人拥有）
public class WeaponsStore
{
    public List<Weapon> weaponList;

    // 获取仓库武器标志图片地址
    //public static string getWeaponItemPath(string itemPath)
    //{
    //    return "Weapons/" + itemPath;
    //}

    // 根据存储ID获取武器
    public static Weapon getWeaponFromStoreId(int storehouseId)
    {
        return getWeaponFromStoreId(PlayerManager.instance.getWeaponsPossess(), storehouseId);
    }
    public static Weapon getWeaponFromStoreId(List<Weapon> weaponList, int storehouseId)
    {
        foreach (Weapon w in weaponList)
            if (w.StorehouseId == storehouseId)
                return w;

        return null;
    }



    //public static WeaponsManager Instance = new WeaponsManager();

    //private WeaponsManager() { }

    //public void saveData(string[] paramName, string[,] data)
    //{
    //    weaponList = new List<Weapon>();
    //    Weapon weapon;
    //    for (int i = 0; i < data.GetLength(0); i++)
    //    {
    //        weapon = new Weapon();
    //        weapon.Id = int.Parse(data[i, 0]);
    //        weapon.Name = data[i, 1];
    //        weapon.Type = (Equipment.WeaponType)Enum.Parse(typeof(Equipment.WeaponType), data[i, 2]);
    //        weapon.ImagePath = data[i, 3];
    //        weapon.Attack = float.Parse(data[i, 4]);
    //        weapon.Bullets = float.Parse(data[i, 5]);
    //        weapon.PeerTime = float.Parse(data[i, 6]);

    //        weaponList.Add(weapon);
    //    }
    //}
}
