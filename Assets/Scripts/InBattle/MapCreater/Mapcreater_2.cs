using UnityEngine;
using System.Collections;

// Scene2地图创建器
public class Mapcreater_2 : Singleton<Mapcreater_2>
{

    public GameObject mapContainer;
    public GameObject LandContainer;
    public GameObject Background_Fore_Container;
    public GameObject Background_Back_Container;
    public GameObject BarContainer;
    public GameObject SkyContainer;

    // 配件
    public GameObject land;
    public GameObject background_Fore;
    public GameObject background_Back;
    public GameObject sky;
    public GameObject bar;
    //public string name { get; set; }

    public override void Init()
    {
        mapContainer = new GameObject();
        mapContainer.name = "MapContainer";

        LandContainer = new GameObject();
        LandContainer.name = "LandContainer";
        LandContainer.transform.parent = mapContainer.transform;

        Background_Fore_Container = new GameObject();
        Background_Fore_Container.name = "Background_Fore_Container";
        Background_Fore_Container.transform.parent = mapContainer.transform;

        Background_Back_Container = new GameObject();
        Background_Back_Container.name = "Background_Back_Container";
        Background_Back_Container.transform.parent = mapContainer.transform;

        BarContainer = new GameObject();
        BarContainer.name = "BarContainer";
        BarContainer.transform.parent = mapContainer.transform;

        SkyContainer = new GameObject();
        SkyContainer.name = "SkyContainer";
        SkyContainer.transform.parent = mapContainer.transform;

        land = Resources.Load("Prefabs/InBattle/Scene2/Land") as GameObject;
        background_Fore = Resources.Load("Prefabs/InBattle/Scene2/Background_Fore") as GameObject;
        background_Back = Resources.Load("Prefabs/InBattle/Scene2/Background_Back") as GameObject;
        sky = Resources.Load("Prefabs/InBattle/Scene2/Sky") as GameObject;
        bar = Resources.Load("Prefabs/InBattle/Scene2/Bar") as GameObject;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 创建地图
    public void createMap(float length)
    {
        //Debug.Log(land.transform.)
        createLandscape(length, BarContainer, bar, 8);
        createLandscape(length, LandContainer, land, 28);
        createLandscape(length, Background_Fore_Container, background_Fore, 20);
        createLandscape(length * 2, Background_Back_Container, background_Back, 30);
        createLandscape(length * 6, SkyContainer, sky, 135);

        //float sizeX = land.GetComponent<MeshFilter>().mesh.bounds.size.x;
        //print(sizeX);

        //GameObject newLand = GameObject.Instantiate(land) as GameObject;
        //newLand.transform.parent = LandContainer.transform;
        //newLand.transform.Translate(offset);
        //newLand.AddComponent<BoxCollider>();
        //newLand.SetActive(true);
    }


    // 添加组件
    private void createLandscape(float length, GameObject parent, GameObject model, float overFlow)
    {
        Transform container = parent.transform;

        float sizeX;
        if (model.GetComponent<Renderer>() != null)
            sizeX = model.GetComponent<Renderer>().bounds.size.x;
        else
            sizeX = model.transform.localScale.x;
        //float sizeX = model.GetComponent<MeshCollider>().mesh.bounds.size.x;
        //Debug.Log(model.name + " SizeX : " + sizeX);
        //print(model.transform.renderer.bounds.size.x);
        if (sizeX == 0)
        {
            Debug.Log("Size Error!!!");
            return;
        }
        length += overFlow;
        GameObject newModel;
        Vector3 offset = new Vector3(-overFlow, 0, 0);

        while (offset.x < length)
        {
            //print(offset.x + "   " + length);
            newModel = GameObject.Instantiate(model) as GameObject;
            newModel.transform.parent = container;
            newModel.transform.Translate(offset);
            newModel.SetActive(true);

            offset.x += sizeX;
            //xLocate += sizeX;
        }
    }
}
