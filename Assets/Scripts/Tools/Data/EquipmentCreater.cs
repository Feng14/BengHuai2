using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;

// 根据Excel创建类
public class EquipmentCreater {
    private int fuck;
    public int Fuck
    {
        get { return fuck; }
        set { fuck = value; }
    }

    // 创建装备基本类
    public static string createClass(string className, string[] p, string filePath){
        string classStr = "using System.Collections;\nusing UnityEngine;\n\n";
        classStr += "public class " + className + " {\n";

        foreach (string s in p)
        {
            classStr += "\tprivate " + getParamType(s) + " " + getParamName(s) + ";\n";
        }
        classStr += "\n";

        foreach (string s in p)
        {
            classStr += "\tpublic " + getParamType(s) + " " + upcaseFirst(s) +  "{\n";
            classStr += "\t\tget{return " + getParamName(s) + ";}\n";
            classStr += "\t\tset {" + getParamName(s) + " = value;}\n\t}\n";
        }
        classStr += "}";

        createClassFile(classStr, filePath);
        return classStr;
    }

    // 获取类型
    private static string getParamType(string s)
    {
        string[] s1 = s.Split(':');
        return s1[s1.Length - 1];
    }

    // 获取变量名
    private static string getParamName(string s)
    {
        return s.Split(':')[0];
    }

    //首字母大写
    private static string upcaseFirst(string s){
        return s.Substring(0, 1).ToUpper() + getParamName(s.Substring(1, s.Length - 1));
    }

    // 建立文件
    public static void createClassFile(string content, string filePath)
    {
        Debug.Log("createClassFile    " + filePath);

        StreamWriter sw;
        FileInfo fi = new FileInfo(filePath);
        if (fi.Exists)
            fi.Delete();

        sw = fi.CreateText();
        sw.Write(content);
        sw.Close();
        sw.Dispose();
    }

    public static void createLoadToSystemFile(string className, string[] paramList)
    {
        string classStr = "using System.Collections;\nusing System.Collections.Generic;\nusing System;\nusing UnityEngine;\n\n";
        classStr += "// 利用自动创建的基类，将Excel中的装备数据导入Resource\n";
        classStr += "public class " + className + "Loader\n{\n";
        classStr += "\tpublic static void exchangeData(string fileName, string[,] data)\n\t{\n";
        classStr += "\t\tExcelReverse.Instance.loadDataFromExcel();\n";
        //classStr += "\t\tstring[,] data = ExcelReverse.Instance.excelData;\n";


        classStr += "\t\tList<" + className + "> objList = new List<" + className + ">();\n";
        classStr += "\t\t" + className + " obj;\n";
        classStr += "\t\tfor (int i = 0; i < data.GetLength(0); i++)\n\t\t{\n";
        classStr += "\t\t\tobj = new " + className + "();\n";
        for (int i = 0; i < paramList.Length; i++)
        {
            classStr += "\t\t\tobj." + upcaseFirst(getParamOnly(paramList[i])) + " = " + createForceExchange("data[i, " + i + "]", getParamTypeOnly(paramList[i])) + ";\n";
        }
        classStr += "\t\t\tobjList.Add(obj);\n";
        classStr += "\t\t}\n";
        classStr += "\t\tstring path = Application.dataPath + \"/\" + \"Resources/Data/" + className + ".data\";\n";
        classStr += "\t\tDataFileManager.saveDataList<" + className + ">(objList, path);\n";

        //classStr += "\t\tWeaponsStore weaponsStore = new WeaponsStore();\n";
        //classStr += "\t\tweaponsStore.weaponList = weaponList;\n\n";
        //classStr += "\t\tstring path = Application.dataPath + \"/\" + \"Resources/Data/" + className + ".data\";\n";
        //classStr += "\t\tEquipmentManager.Instance.saveWeaponsManager(weaponsStore, path);\n";
        classStr += "\t}\n";
        classStr += "}";

        createClassFile(classStr, Application.dataPath + "/Resources/Excel/" + className + "Loader.cs");

        Debug.Log("创建数据导入器" + className + "Loader 完毕！");
        Debug.Log("地址为 ： " + Application.dataPath + "/Resources/Excel/" + className + "Loader.cs");

    }

    // 获取字符串中的变量
    public static string getParamOnly(string str)
    {
        return str.Split(':')[0];
    }

    // 获取字符串中的类型
    public static string getParamTypeOnly(string str)
    {
        //Debug.Log(str);
        return str.Split(':')[1];
    }

    // 编写强制转换
    public static string createForceExchange(string param, string type)
    {
        switch (type)
        {
            case "int":
                return "int.Parse(" + param + ")";
            case "float":
                return "float.Parse(" + param + ")";
            case "bool":
                return "bool.Parse(" + param + ")";
            case "string":
                return param;
            case "Vector3":
                string[] s1 = param.Split(',');
                Debug.Log(s1.Length);
                return "new Vector3(float.Parse((" + param + ".Split(','))[0]), float.Parse((" + param + ".Split(','))[1]), float.Parse((" + param + ".Split(','))[2]))";
            default:
                return "(EquipmentManager.WeaponType)Enum.Parse(typeof(EquipmentManager.WeaponType), " + param + ")";
        }
    }

    //// 导入数据
    //public void loadDate(string[] paramName, string[,] data, string className)
    //{
    //    string loaderClassStr = "using UnityEngine;\n using System.Collections;\n using System;\n using System.Text;\n using System.IO;\n\n";
    //    loaderClassStr += "public class Loader{\n";
    //    loaderClassStr += "\tpublic void load(string[,] data){\n";
    //    loaderClassStr += "\t\t" + className + " \n";


    //}

}