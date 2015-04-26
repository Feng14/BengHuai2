using UnityEngine;
using System.Collections;

/// <summary>
/// 附着在玩家和怪物身上的战斗时数据
/// </summary>
public class CharacterData : MonoBehaviour {
    public enum Type { Player, Monster };

    public Type type = Type.Monster;
    public Monster monster;

    public GameObject hpContainer;
    public float HP_Current;
    public int ID;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
