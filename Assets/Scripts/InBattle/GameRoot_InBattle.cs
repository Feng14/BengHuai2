using UnityEngine;
using System.Collections;

public class GameRoot_InBattle
{
    public static GameRoot_InBattle instance;
    GameObject rootObj = null;

    private GameRoot_InBattle()
    {
        rootObj = new GameObject("GameRoot");
        instance = this;
    }

    public static T addSingleton<T>() where T : Singleton<T>
    {
        if (instance == null)
            instance = new GameRoot_InBattle();

        T t = instance.rootObj.AddComponent<T>();
        t.Init();
        return t;
    }

    public static T getSingleton<T>() where T : Singleton<T>
    {
        if (instance == null)
            instance = new GameRoot_InBattle();

        return instance.rootObj.GetComponent<T>();
    }

}
