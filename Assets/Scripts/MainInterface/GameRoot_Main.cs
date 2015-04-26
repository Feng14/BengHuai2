using UnityEngine;
using System.Collections;

public class GameRoot_Main {
    public static GameRoot_Main instance;
    GameObject rootObj = null;

    private GameRoot_Main()
    {
        rootObj = new GameObject("GameRoot");
        instance = this;
    }

    public static T addSingleton<T>() where T : Singleton<T>
    {
        if (instance == null)
            instance = new GameRoot_Main();

        T t = instance.rootObj.AddComponent<T>();
        t.Init();
        return t;
    }

    public static T getSingleton<T>() where T : Singleton<T>
    {
        if (instance == null)
            instance = new GameRoot_Main();

        return instance.rootObj.GetComponent<T>();
    }
}
