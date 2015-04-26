using System.Collections;
using UnityEngine;

public class Monster {
	private int id;
	private string name;
	private string type;
	private string skeletonPath;
	private float hp;
	private float attack;
	private float speed;
	private int aiMode;
	private bool dependOnTime;
	private int time;
	private bool dependOnPosition;
	private int playerArrive;
	private Vector3 position;

	public int Id{
		get{return id;}
		set {id = value;}
	}
	public string Name{
		get{return name;}
		set {name = value;}
	}
	public string Type{
		get{return type;}
		set {type = value;}
	}
	public string SkeletonPath{
		get{return skeletonPath;}
		set {skeletonPath = value;}
	}
	public float Hp{
		get{return hp;}
		set {hp = value;}
	}
	public float Attack{
		get{return attack;}
		set {attack = value;}
	}
	public float Speed{
		get{return speed;}
		set {speed = value;}
	}
	public int AiMode{
		get{return aiMode;}
		set {aiMode = value;}
	}
	public bool DependOnTime{
		get{return dependOnTime;}
		set {dependOnTime = value;}
	}
	public int Time{
		get{return time;}
		set {time = value;}
	}
	public bool DependOnPosition{
		get{return dependOnPosition;}
		set {dependOnPosition = value;}
	}
	public int PlayerArrive{
		get{return playerArrive;}
		set {playerArrive = value;}
	}
	public Vector3 Position{
		get{return position;}
		set {position = value;}
	}
}