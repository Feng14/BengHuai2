using UnityEngine;
using System.Collections;

public class MyButton : MonoBehaviour {
    public string selectedImage, unSelectedImage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setSelected(bool selected)
    {
        gameObject.GetComponent<UISprite>().spriteName = selected ? selectedImage : unSelectedImage;
    }
}
