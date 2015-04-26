using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


// 管理文件到内存的数据转化读取保存
public class DataFileManager {

    // 存
    public static void saveDataList<T>(List<T> list, string path)
    {
        //Debug.Log((list[1] as Monster).DependOnTime);
        //Debug.Log((list[1] as Monster).DependOnPosition);
        //Debug.Log((list[1] as Monster).PlayerArive);


        //List<Monster> l = new List<Monster>();
        //l.Add(list[1] as Monster);

        //Monster m1 = new Monster();
        //m1.Id = 1;
        //m1.Name = "asdjflofo";
        //m1.Type = "kjdsa";
        //m1.SkeletonPath = "jdsaf";
        //m1.Hp = 123f;
        //m1.Attack = 432f;
        //m1.Speed = 2f;
        //m1.AiMode = 1;
        //m1.DependOnTime = false;
        //m1.DependOnPosition = false;
        //m1.Time = 1;
        //m1.PlayerArive = 1;

        //FuckUnity fuck = new FuckUnity();

        Debug.Log("最终数据输出地址 : " + path);
        FileStream fs = new FileStream(path, FileMode.Create);
        XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
        //formatter.Serialize(fs, fuck);
        formatter.Serialize(fs, list);

    }


    // 取
    public static List<T> loadDataList<T>(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
        return formatter.Deserialize(fs) as List<T>;
    }
}
