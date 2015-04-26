using UnityEngine;
using System.Collections;

public class GameRoot_Test {
     public static GameRoot_Test instance;
    //GameObject rootObj = null;

    private GameRoot_Test()
    {
        //rootObj = new GameObject("GameRoot");
        instance = this;
    }

    public static void init()
    {
        if (instance == null)
            instance = new GameRoot_Test();
    }

    public static T addSingleton<T>(GameObject gameObject) where T : Singleton<T>
    {
        if (instance == null)
            instance = new GameRoot_Test();

        T t = gameObject.AddComponent<T>();
        t.Init();
        return t;
    }

    public static T getSingleton<T>() where T : Singleton<T>
    {
        if (instance == null)
            instance = new GameRoot_Test();


        return null;
        //return instance.rootObj.GetComponent<T>();
    }
}
