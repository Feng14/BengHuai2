using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 怪物管理器
public class MonsterManager : Singleton<MonsterManager> {
    private GameObject prefab;
    private List<Monster> monstersDependOnPosition;

    // 怪物ID
    private int monsterIndex = 0;

	// Use this for initialization
    public override void Init()
    {
        prefab = Resources.Load("Prefabs/InBattle/Monster_Kiana") as GameObject;
        Pool.Instance.createObjPool(prefab, Pool.PoolType.Monster);
    }


	void Start () {
        loadMonterDataForStage(Application.dataPath + "/Resources/Data/Monster.data");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().playerMoveEvent
            -= new MessageManager_InBattle.PlayerMoveDelegrate(checkPlayerPositionForMonsterCreate);
    }

    /// <summary>
    /// 读取某关卡的怪物信息数据
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public List<Monster> loadMonterDataForStage(string path)
    {
        List<Monster> list = PlayerManager.instance.xmlToObject <List<Monster>>(path);
        monstersDependOnPosition = new List<Monster>();

        //StartCoroutine(WaitForCreateMonster(list[0]));
        foreach (Monster monster in list)
        {
            if (monster.DependOnTime)
                StartCoroutine(WaitForCreateMonster(monster));
            else if (monster.DependOnPosition)
                monstersDependOnPosition.Add(monster);
        }
        if (monstersDependOnPosition.Count > 0)
            GameRoot_InBattle.getSingleton<MessageManager_InBattle>().playerMoveEvent
                += new MessageManager_InBattle.PlayerMoveDelegrate(checkPlayerPositionForMonsterCreate);

        return list;
    }

    /// <summary>
    /// 等到一定时间自动出来的怪放在这个协程中创建
    /// </summary>
    /// <param name="monster"></param>
    /// <returns></returns>
    IEnumerator WaitForCreateMonster(Monster monster)
    {
        if (!monster.DependOnTime)
            yield break;

        //Debug.Log("Time : " + monster.Time);
        yield return new WaitForSeconds(monster.Time);
        //Debug.Log("Create");
        createMonsterOnScene(monster);
    }

    /// <summary>
    /// 玩家移动了，检查是否到了怪应该出现的位置
    /// </summary>
    /// <param name="position"></param>
    private void checkPlayerPositionForMonsterCreate(Vector3 position)
    {
        //Debug.Log("Check Position  " + position.x + "   " + monstersDependOnPosition[0].PlayerArrive);

        int index = 0;
        Monster monster;
        while (index < monstersDependOnPosition.Count)
        {
            monster = monstersDependOnPosition[index++];
            if (position.x > monster.PlayerArrive)
            {
                Debug.Log("Create DependOn Postion");
                createMonsterOnScene(monster);
                monstersDependOnPosition.Remove(monster);
            }
        }
        if (monstersDependOnPosition.Count == 0)
            GameRoot_InBattle.getSingleton<MessageManager_InBattle>().playerMoveEvent
            -= new MessageManager_InBattle.PlayerMoveDelegrate(checkPlayerPositionForMonsterCreate);
    }

    /// <summary>
    /// 在场上创建怪物
    /// </summary>
    /// <param name="monster"></param>
    private void createMonsterOnScene(Monster monster)
    {
        GameObject monsterGO = Pool.Instance.getObjFromPool(Pool.PoolType.Monster);
        float x = monster.Position.x,
            z = monster.Position.z;
        z = z > 6.4 ? 6.4f : z;
        z = z < -3 ? -3f : z;
        monsterGO.transform.position = new Vector3(x, 4.3f, z);
        monsterGO.SendMessage("SetMonster", monster);
        monsterGO.SetActive(true);
        monsterGO.AddComponent<AI_1>();

        CharacterData data = monsterGO.GetComponent<CharacterData>();
        data.monster = monster;
        data.HP_Current = monster.Hp;
        data.ID = ++monsterIndex;

        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().SendMessage_CreateMonsterEvent(monsterGO, monster, data);
    }
}
