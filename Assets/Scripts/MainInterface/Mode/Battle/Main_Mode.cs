using UnityEngine;
using System.Collections;

// 主界面模式（战斗，装备……）
public abstract class Main_Mode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 修改成(不)选中状态
    public void setSelected(bool ifSelect)
    {
        //Debug.Log("Set " + ifSelect);
        transform.GetChild(0).GetComponent<UISprite>().spriteName = ifSelect ? getSelectedSpriteName() : getUnSelectedSpriteName();
    }

    public abstract string getSelectedSpriteName();
    public abstract string getUnSelectedSpriteName();
}
