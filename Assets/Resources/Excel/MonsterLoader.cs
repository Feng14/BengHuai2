using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// 利用自动创建的基类，将Excel中的装备数据导入Resource
public class MonsterLoader
{
	public static void exchangeData(string fileName, string[,] data)
	{
		ExcelReverse.Instance.loadDataFromExcel();
		List<Monster> objList = new List<Monster>();
		Monster obj;
		for (int i = 0; i < data.GetLength(0); i++)
		{
			obj = new Monster();
			obj.Id = int.Parse(data[i, 0]);
			obj.Name = data[i, 1];
			obj.Type = data[i, 2];
			obj.SkeletonPath = data[i, 3];
			obj.Hp = float.Parse(data[i, 4]);
			obj.Attack = float.Parse(data[i, 5]);
			obj.Speed = float.Parse(data[i, 6]);
			obj.AiMode = int.Parse(data[i, 7]);
			obj.DependOnTime = bool.Parse(data[i, 8]);
			obj.Time = int.Parse(data[i, 9]);
			obj.DependOnPosition = bool.Parse(data[i, 10]);
			obj.PlayerArrive = int.Parse(data[i, 11]);
			obj.Position = new Vector3(float.Parse((data[i, 12].Split(','))[0]), float.Parse((data[i, 12].Split(','))[1]), float.Parse((data[i, 12].Split(','))[2]));
			objList.Add(obj);
		}
		string path = Application.dataPath + "/" + "Resources/Data/Monster.data";
		DataFileManager.saveDataList<Monster>(objList, path);
	}
}