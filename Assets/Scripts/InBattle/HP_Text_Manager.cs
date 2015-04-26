using UnityEngine;
using System.Collections;

// 负责显示伤害（回复）的数字（在玩家，怪物身上）
public class HP_Text_Manager : MonoBehaviour {
    public HUDText hudText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 显示受伤数字
    public void showHurt(int hurt)
    {
        hudText.Add(hurt, Color.red, 1f);
    }

    public void showRecover(int hurt)
    {
        hudText.Add(hurt, Color.green, 1f);
    }
}
