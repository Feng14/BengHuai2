using UnityEngine;
using System.Collections;

// 战斗模块的初始化
public class Start_InBattle : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if (PlayerManager.instance == null)
            PlayerManager.init();

        GameRoot_InBattle.addSingleton<MessageManager_InBattle>();
	    GameRoot_InBattle.addSingleton<Mapcreater_2>();
        GameRoot_InBattle.addSingleton<GameManager_InBattle>();
        GameRoot_InBattle.addSingleton<PlayerManager_InBattle>();
        GameRoot_InBattle.addSingleton<MonsterManager>();
        GameRoot_InBattle.addSingleton<BulletsManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
