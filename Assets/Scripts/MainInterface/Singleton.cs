using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : Component {
    //public static GameObject gameRoot;

    //public static T getInstance()
    //{
    //    if (gameRoot == null)
    //        gameRoot = GameObject.Find("GameRoot");

    //    if (gameRoot == null)
    //        gameRoot = new GameObject("GameRoot");

    //    return gameRoot.GetComponent<T>();
    //    return null;
    //}

    public abstract void Init();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
