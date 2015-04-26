using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        List<int> a = new List<int>();
        a.Add(1);
        a.Add(2);
        //GUITexture t = Resources.Load("Weapons/RPG/015_3") as GUITexture;
        //transform.FindChild("Image").GetComponent<UISprite>().sp = t;
        
    }
}
