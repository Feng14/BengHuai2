using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
    public HUDText hudTest;

	// Use this for initialization
	void Start () {
        //Texture t = Resources.Load("Weapons/StorehouseItem/Rifle_2_5") as Texture;
        //gameObject.GetComponent<UITexture>().drawCall = Resources.Load("Weapons/StorehouseItem/Rifle_2_5") as UIDrawCall;
        //gameObject.GetComponent<UITexture>().drawCall = dc;
        //gameObject.GetComponent<UITexture>().mainTexture = Resources.Load("Weapons/StorehouseItem/Rifle_2_5") as Texture;
	}
	
	// Update is called once per frame
	void Update () {
        print(hudTest == null);
	    if (Input.GetMouseButtonDown(0))
            hudTest.Add(123, Color.red, 2f);
	}
    //public void onclick()
    //{
    //    transform.GetChild(0).GetComponent<UISprite>().spriteName = "Function_Fight2";
    //}
}
