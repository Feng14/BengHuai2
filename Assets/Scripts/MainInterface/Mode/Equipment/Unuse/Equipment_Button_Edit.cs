using UnityEngine;
using System.Collections;

// Unuse
// 装备->编辑物品 按钮脚本
public class Equipment_Button_Edit : Equipment_Button
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnEditButtonClick()
    {
        //Debug.Log("click");
        GameRoot_Main.getSingleton<MessageManager_Equipment>().sendMessage_editEquipment();
    }

    public override void OnClick()
    {
        //changeButtonImage(true);
    }
}
