//--------------------------------------------
//            NGUI: HUD Text
// Copyright � 2012 Tasharen Entertainment
//--------------------------------------------

using UnityEngine;

/// <summary>
/// Example script that displays text above the collider when the collider is hovered over or clicked.
/// </summary>

[AddComponentMenu("NGUI/Examples/Collider - Display Text")]
public class ColliderDisplayText : MonoBehaviour
{
	// The UI prefab that is going to be instantiated above the player
	public GameObject prefab;
	public Transform target;

	HUDText mText = null;
	bool mHover = false;

	// Use this for initialization
	void Start ()
	{
		// We need the HUD object to know where in the hierarchy to put the element
		if (HUDRoot.go == null)
		{
			GameObject.Destroy(this);
			return;
		}

		GameObject child = NGUITools.AddChild(HUDRoot.go, prefab);
		mText = child.GetComponentInChildren<HUDText>();

		// Make the UI follow the target
		child.AddComponent<UIFollowTarget>().target = target;
	}

	void OnHover (bool isOver)
	{
		if (mText != null && isOver && !mHover)
		{
			mHover = true;
			mText.Add("Left-click, right-click", Color.cyan, 2f);
		}
		else if (!isOver)
		{
			mHover = false;
		}
	}

	void OnClick ()
	{
		if (mText != null)
		{
			if (UICamera.currentTouchID == -1) mText.Add(-10f + Random.value * -10f, Color.red, 0f);
			else if (UICamera.currentTouchID == -2) mText.Add(10f + Random.value * 10f, Color.green, 0f);
		}
	}
}
