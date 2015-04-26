using UnityEngine;
using System.Collections;

// 附着在头顶上血条的脚本
public class HP_Slider : MonoBehaviour {
    // 所属怪物的ID
    public int ID;
    public UISlider slider;
    public HUDText hudText;

	// Use this for initialization
	void Start () {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().monsterHurtEvent
            += new MessageManager_InBattle.MonsterHurtDelegrate(MonsterHurted);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().monsterHurtEvent
            -= new MessageManager_InBattle.MonsterHurtDelegrate(MonsterHurted);
    }

    public void MonsterHurted(Bullets bullet, GameObject monster, CharacterData charaterData, float hurt)
    {
        Debug.Log("Slider : " + hurt);
        if (charaterData.ID != ID)
            return;

        // 减短血条
        float hp = charaterData.HP_Current - hurt;
        hp = hp > 0 ? hp : 0;
        slider.value = hp / charaterData.monster.Hp;

        // 显示减少的数字
        hudText.Add((int)hurt, Color.white, 1f);
    }
}
