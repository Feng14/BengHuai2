using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    //public static GameManager instance;

    public GameObject uiRoot;
    public GameObject mainInterface_Top;
    public GameObject mainInterface_Bottom;

    public delegate void SceneMessageDelegate();
    public event SceneMessageDelegate disRegisterMessage;

    //static GameManager()
    //{
    //    GameObject go = new GameObject("GameManager");
    //    instance = go.AddComponent<GameManager>();
    //}

	// Use this for initialization
    public override void Init() {
        uiRoot = GameObject.Find("UI Root");
        mainInterface_Top = Resources.Load("Prefabs/MainInterface/Panel_Top_Information") as GameObject;
        mainInterface_Bottom = Resources.Load("Prefabs/MainInterface/Panel_Bottom") as GameObject;
    }

	void Start () {
        GameObject topInterface = GameObject.Instantiate(mainInterface_Top) as GameObject;
        topInterface.transform.parent = uiRoot.transform;
        topInterface.transform.localScale = Vector3.one;
        topInterface.GetComponent<UIPanel>().baseClipRegion = new Vector4(0, Screen.height / 2 - (Screen.height / 10), Screen.width, Screen.height / 5);
        topInterface.SetActive(true);

        GameObject bottomInterface = GameObject.Instantiate(mainInterface_Bottom) as GameObject;
        bottomInterface.transform.parent = uiRoot.transform;
        bottomInterface.transform.localScale = Vector3.one;
        bottomInterface.GetComponent<UIPanel>().baseClipRegion = new Vector4(0, Screen.height / 2 - (Screen.height / 5 + Screen.height / 5 * 2) + 8, Screen.width, Screen.height / 5 * 4);
        bottomInterface.SetActive(true);


        // 监听“开始战斗”消息
        //Debug.Log("监听“开始战斗”消息   " + GameRoot_Main.getSingleton<MessageManager_BattleMap>() == null);
        GameRoot_Main.getSingleton<MessageManager_BattleMap>().startBattle
            += new MessageManager_BattleMap.BattleStageMessageDelegate(startBattle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 发送让所有消息监听类取消监听的消息
    public void sendMessage_DisRegister(){
        if (disRegisterMessage != null)
            disRegisterMessage();
    }

    // “开始战斗”事件处理
    private void startBattle(BattleStage stage)
    {
        //Debug.Log("GameManager : Deal with startBattle");
        // 取消所有监听
        sendMessage_DisRegister();

        // 清理内存
        Transform uiRoot = GameObject.Find("UI Root").transform;
        //Debug.Log(uiRoot.FindChild("Panel_Top_Information(Clone)") == null);
        Destroy(uiRoot.FindChild("Panel_Top_Information(Clone)").gameObject);
        //Destroy(GameObject.Find("Panel_Top_Information"));
        Destroy(GameObject.Find("Panel_Bottom(Clone)").gameObject);
        Resources.UnloadUnusedAssets();

        // 保留一个关卡信息对象
        GameObject go = new GameObject("BattleStage Information");
        //go.AddComponent<BattleStage>();
        BattleStageObj battleStageObj = go.AddComponent<BattleStageObj>();
        battleStageObj.battleStage = stage;
        //BattleStage.copy(stage, battleStageObj.battleStage);
        GameObject.DontDestroyOnLoad(go);

        //Debug.Log("Application.LoadLevel : Scenes/InBattle");
        Application.LoadLevel(1);
    }
}
