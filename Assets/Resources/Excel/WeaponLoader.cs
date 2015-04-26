using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// 利用自动创建的基类，将Excel中的装备数据导入Resource
public class WeaponLoader
{
	public static void exchangeData(string fileName, string[,] data)
	{
		ExcelReverse.Instance.loadDataFromExcel();
		List<Weapon> objList = new List<Weapon>();
		Weapon obj;
		for (int i = 0; i < data.GetLength(0); i++)
		{
			obj = new Weapon();
			obj.Id = int.Parse(data[i, 0]);
			obj.Name = data[i, 1];
			obj.Type = (EquipmentManager.WeaponType)Enum.Parse(typeof(EquipmentManager.WeaponType), data[i, 2]);
			obj.ImagePath = data[i, 3];
			obj.ItemPath = data[i, 4];
			obj.ShadowPath = data[i, 5];
			obj.Star = int.Parse(data[i, 6]);
			obj.Attack = float.Parse(data[i, 7]);
			obj.PeerTime = float.Parse(data[i, 8]);
			obj.StorehouseId = int.Parse(data[i, 9]);
			obj.Bullets = float.Parse(data[i, 10]);
			obj.BulletSpeed = float.Parse(data[i, 11]);
			obj.BulletPath = data[i, 12];
			obj.BulletAngle = float.Parse(data[i, 13]);
			obj.ContinueShoot = bool.Parse(data[i, 14]);
			objList.Add(obj);
		}
		string path = Application.dataPath + "/" + "Resources/Data/Weapon.data";
		DataFileManager.saveDataList<Weapon>(objList, path);
	}
}