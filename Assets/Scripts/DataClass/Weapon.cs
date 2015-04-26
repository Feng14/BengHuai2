using System.Collections;
using UnityEngine;

public class Weapon {
	private int id;
	private string name;
	private EquipmentManager.WeaponType type;
	private string imagePath;
	private string itemPath;
	private string shadowPath;
	private int star;
	private float attack;
	private float peerTime;
	private int storehouseId;
	private float bullets;
	private float bulletSpeed;
	private string bulletPath;
	private float bulletAngle;
	private bool continueShoot;

	public int Id{
		get{return id;}
		set {id = value;}
	}
	public string Name{
		get{return name;}
		set {name = value;}
	}
	public EquipmentManager.WeaponType Type{
		get{return type;}
		set {type = value;}
	}
	public string ImagePath{
		get{return imagePath;}
		set {imagePath = value;}
	}
	public string ItemPath{
		get{return itemPath;}
		set {itemPath = value;}
	}
	public string ShadowPath{
		get{return shadowPath;}
		set {shadowPath = value;}
	}
	public int Star{
		get{return star;}
		set {star = value;}
	}
	public float Attack{
		get{return attack;}
		set {attack = value;}
	}
	public float PeerTime{
		get{return peerTime;}
		set {peerTime = value;}
	}
	public int StorehouseId{
		get{return storehouseId;}
		set {storehouseId = value;}
	}
	public float Bullets{
		get{return bullets;}
		set {bullets = value;}
	}
	public float BulletSpeed{
		get{return bulletSpeed;}
		set {bulletSpeed = value;}
	}
	public string BulletPath{
		get{return bulletPath;}
		set {bulletPath = value;}
	}
	public float BulletAngle{
		get{return bulletAngle;}
		set {bulletAngle = value;}
	}
	public bool ContinueShoot{
		get{return continueShoot;}
		set {continueShoot = value;}
	}
}