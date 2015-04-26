using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Excel;
using System.Data;

public class ExcelReverse : MonoBehaviour {
    public static ExcelReverse Instance;
    // 存数据
    private static string[,] excelData;
    // 存变量名
    private string[] paramNames;

    public UIInput input, output;
    public string dataType = "Weapon";

	// Use this for initialization
	void Start () {
        Instance = this;

        if (!PlayerPrefs.HasKey("ExcelPath"))
            PlayerPrefs.SetString("ExcelPath", getExcelPath());
        input.value = PlayerPrefs.GetString("ExcelPath");

        if (!PlayerPrefs.HasKey("DataPath"))
            PlayerPrefs.SetString("DataPath", getOutputPath());
        output.value = PlayerPrefs.GetString("DataPath");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 修改数据模式（武器，怪物）
    public void changeDataType(string type)
    {
        dataType = type;
    }

    // 获取Excel地址
    public string getExcelPath()
    {
        return input.value;
    }

    // 获取输出地址
    public string getOutputPath()
    {
        return output.value;
    }

    // 获取数据
    public void loadDataFromExcel()
    {
        Debug.Log(getExcelPath());
        FileStream stream = File.Open(getExcelPath(), FileMode.Open, FileAccess.Read);

        //FileStream stream = File.Open("D:/3/1.xlsx", FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();

        //Debug.Log(result == null);
        int columns = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;

        excelData = new string[rows - 1, columns];
        paramNames = new string[columns];

        string s;

        for (int i = 0; i < columns; i++)
        {
            paramNames[i] = result.Tables[0].Rows[0][i].ToString();
        }

        for (int i = 1; i < rows; i++)
        {
            s = "";
            for (int j = 0; j < columns; j++)
            {
                excelData[i-1, j] = result.Tables[0].Rows[i][j].ToString();
                s += result.Tables[0].Rows[i][j].ToString() + "   ";
            }
            //Debug.Log(s);
        }
        Debug.Log("导入数据完毕！！");
    }

    // 开始转换
    public void reverseData()
    {
        PlayerPrefs.SetString("ExcelPath", getExcelPath());
        PlayerPrefs.SetString("DataPath", getOutputPath());

        loadDataFromExcel();

        string[] s1 = getOutputPath().Split('/');
        string className = s1[s1.Length-1].Split('.')[0];

        Debug.Log(EquipmentCreater.createClass(className, paramNames, getOutputPath()));
        Debug.Log("创建类： " + className + " 完毕！");
        Debug.Log("地址为 ： " + getOutputPath());
        EquipmentCreater.createLoadToSystemFile(className, paramNames);
    }

    // 导入武器数据
    public void saveWeaponData()
    {
        if (excelData == null || excelData.Length == 0)
            reverseData();

        string[] s1 = getOutputPath().Split('/');
        string className = s1[s1.Length-1].Split('.')[0];

        Debug.Log("模式 ：" + dataType);
        switch (dataType)
        {
            case "Weapon" :
                Debug.Log("Fuck Weapon");
                WeaponLoader.exchangeData(className, excelData);
                break;
            case "Monster":
                MonsterLoader.exchangeData(className, excelData);
                break;
        }
    }
}
