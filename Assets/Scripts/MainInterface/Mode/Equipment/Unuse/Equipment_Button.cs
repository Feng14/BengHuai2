using UnityEngine;
using System.Collections;

// Unuse
// 装备界面的大按钮
public abstract class Equipment_Button : MonoBehaviour
{
    private string imageUp = "HierachyButtons_Yellow_Up",
                   imageDown = "HierachyButtons_Yellow_Down";
    public UISprite buttonBackground;

	// Use this for initialization
	void Start () {
        //gameObject.GetComponent<UIButton>().ad
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeButtonImage(bool selected)
    {
        //buttonBackground.spriteName = selected ? imageDown : imageUp;
        gameObject.GetComponent<UISprite>().spriteName = selected ? imageDown : imageUp;

        //Debug.Log(buttonBackground.spriteName);
        //transform.FindChild("Background").GetComponent<UISprite>().spriteName = selected ? imageDown : imageUp;
    }

    public abstract void OnClick();
}
