using UnityEngine;
using System.Collections;

// 战斗模块管理器
public class GameManager_InBattle : Singleton<GameManager_InBattle>
{

    void Awake()
    {
    }

    public override void Init()
    {
    }

	// Use this for initialization
	void Start () {
        GameRoot_InBattle.getSingleton<Mapcreater_2>().createMap(30);
        Pool.Instance.createObjPool(Resources.Load("Prefabs/InBattle/Bullet") as GameObject, Pool.PoolType.Bullet, 20);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
