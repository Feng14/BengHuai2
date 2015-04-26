using UnityEngine;
using System.Collections;

public class Button_TurnBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        GameRoot_Main.getSingleton<MessageManager_Main>().sendMessage_TurnBack();
    }
}
