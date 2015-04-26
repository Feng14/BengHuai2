using UnityEngine;
using System.Collections;

// 游戏开始时调用的初始化
public class Start_Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerManager.init();
        GameRoot_Main.addSingleton<GameManager>();
        GameRoot_Main.addSingleton<MessageManager_Main>();
        GameRoot_Main.addSingleton<MessageManager_BattleMap>();
        GameRoot_Main.addSingleton<Manager_BattleMap>();
        GameRoot_Main.addSingleton<MessageManager_Equipment>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
