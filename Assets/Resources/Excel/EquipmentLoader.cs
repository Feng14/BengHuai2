//using System.Collections;
//using System.Collections.Generic;
//using System;
//using UnityEngine;

//// 利用自动创建的基类，将Excel中的装备数据导入Resource
//public class EquipmentLoader
//{
//    public static void exchangeData(string fileName, string[,] data)
//    {
//        ExcelReverse.Instance.loadDataFromExcel();
//        List<Weapon> weaponList = new List<Weapon>();
//        Weapon weapon;
//        for (int i = 0; i < data.GetLength(0); i++)
//        {
//            weapon = new Weapon();
//            weapon.Id = int.Parse(data[i, 0]);
//            weapon.Name = data[i, 1];
//            //weapon.Type = data[i, 2];
//            //weapon.SkeletonPath = data[i, 3];
//            //weapon.Hp = float.Parse(data[i, 4]);
//            //weapon.Attack = float.Parse(data[i, 5]);
//            //weapon.Speed = float.Parse(data[i, 6]);
//            //weapon.AiMode = int.Parse(data[i, 7]);
//            //weapon.DependOnTime = bool.Parse(data[i, 8]);
//            //weapon.Time = int.Parse(data[i, 9]);
//            //weapon.DependOnPosition = bool.Parse(data[i, 10]);
//            //weapon.PlayerArive = int.Parse(data[i, 11]);
//            //weapon.Position = (EquipmentManager.WeaponType)Enum.Parse(typeof(EquipmentManager.WeaponType), data[i, 12]);
//            weaponList.Add(weapon);
//        }
//        WeaponsStore weaponsStore = new WeaponsStore();
//        weaponsStore.weaponList = weaponList;

//        string path = Application.dataPath + "/" + "Resources/Data/Monster.data";
//        //EquipmentManager.Instance.saveWeaponsManager(weaponsStore, path);
//    }
//}