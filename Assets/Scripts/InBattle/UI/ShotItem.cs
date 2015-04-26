using UnityEngine;
using System.Collections;

// 附着在战斗界面的武器按钮 上的脚本
public class ShotItem : MonoBehaviour {
    public int index;
    private Weapon weapon;
    public UITexture image;

    private bool pressed, CDOver, hasShoot;

	// Use this for initialization
	void Start () {
        pressed = false;
        CDOver = true;
        hasShoot = false;

        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().shotSuccessfulEvent
            += new MessageManager_InBattle.ShotItemDelegrate(ShotSuccessful);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().shotSuccessfulEvent
            -= new MessageManager_InBattle.ShotItemDelegrate(ShotSuccessful);
    }

    public void setWeapon(Weapon w)
    {
        weapon = w;
        image.mainTexture = Resources.Load(w.ShadowPath) as Texture;
    }

    // “子弹发射成功” 事件
    public void ShotSuccessful(int index, Weapon weapon)
    {
        if (index == this.index)
        {
            CDOver = false;
            StartCoroutine(WaitCDCoroutine(1f / weapon.BulletSpeed));

            // 若武器不能连射，则需要等到放开才能重新发射
            if (!weapon.ContinueShoot)
                hasShoot = true;
        }
    }
    IEnumerator WaitCDCoroutine(float second)
    {
        yield return new WaitForSeconds(second);
        CDOver = true;

        //print("Press  " + pressed.ToString());
        //print("Continue  : " + weapon.ContinueShoot.ToString());
        // 一直按着没放，而且武器能连射，所以，射吧……
        //Debug.Log("ReShoot");
        if (pressed && weapon.ContinueShoot)
        {
            shoot();
        }
    }

    public void OnPress(bool press)
    {
        if (press)
        {
            pressed = true;
            //print("Press");

            if (!CDOver)
                return;

            if (hasShoot && !weapon.ContinueShoot)
                return;

            gameObject.GetComponent<UISprite>().spriteName = "WeaponContainer_2";
            shoot();
        }
        else
        {
            pressed = false;
            //print("UnPress");

            hasShoot = false;

            gameObject.GetComponent<UISprite>().spriteName = "WeaponContainer_1";
            GameRoot_InBattle.getSingleton<MessageManager_InBattle>().SendMessage_ShotItemReleaseEvent(index, weapon);
        }
    }

    // 开枪
    private void shoot()
    {
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().SendMessage_ShotItemPressEvent(index, weapon);
    }


}
