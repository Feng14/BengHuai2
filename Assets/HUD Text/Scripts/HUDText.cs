//--------------------------------------------
//            NGUI: HUD Text
// Copyright © 2012 Tasharen Entertainment
//--------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// HUD text creates temporary on-screen text entries that are perfect for damage, effects, and messages.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/HUD Text")]
public class HUDText : MonoBehaviour
{
	protected class Entry
	{
		public float time;			// Timestamp of when this entry was added
		public float stay = 0f;		// How long the text will appear to stay stationary on the screen
		public float offset = 0f;	// How far the object has moved based on time
		public float val = 0f;		// Optional value (used for damage)
		public UILabel label;		// Label on the game object

		public float movementStart { get { return time + stay; } }
	}

	/// <summary>
	/// Sorting comparison function.
	/// </summary>

	static int Comparison (Entry a, Entry b)
	{
		if (a.movementStart < b.movementStart) return -1;
		if (a.movementStart > b.movementStart) return 1;
		return 0;
	}

	// Deprecated, use 'ambigiousFont' instead.
	[HideInInspector][SerializeField] UIFont font;

	/// <summary>
	/// Font used by the labels.
	/// </summary>

	public UIFont bitmapFont;

	/// <summary>
	/// True type font used by the labels. Alternative to specifying a bitmap font ('font').
	/// </summary>

	public Font trueTypeFont;

	/// <summary>
	/// Size of the font to use for the popup list's labels.
	/// </summary>

	public int fontSize = 20;

	/// <summary>
	/// Font style used by the dynamic font.
	/// </summary>

	public FontStyle fontStyle = FontStyle.Normal;

	/// <summary>
	/// Whether the labels will have a gradient.
	/// </summary>

	public bool applyGradient = false;

	/// <summary>
	/// Gradient's top color.
	/// </summary>

	public Color gradientTop = Color.white;

	/// <summary>
	/// Gradient's bottom color.
	/// </summary>

	public Color gradienBottom = new Color(0.7f, 0.7f, 0.7f);

	/// <summary>
	/// Effect applied to the text.
	/// </summary>

	public UILabel.Effect effect = UILabel.Effect.None;

	/// <summary>
	/// Color of the effect, if used.
	/// </summary>

	public Color effectColor = Color.black;

	/// <summary>
	/// Curve used to move entries with time.
	/// </summary>

	public AnimationCurve offsetCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(3f, 40f) });

	/// <summary>
	/// Curve used to fade out entries with time.
	/// </summary>

	public AnimationCurve alphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(1f, 1f), new Keyframe(3f, 0f) });

	/// <summary>
	/// Curve used to scale the entries.
	/// </summary>

	public AnimationCurve scaleCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.25f, 1f) });

	List<Entry> mList = new List<Entry>();
	List<Entry> mUnused = new List<Entry>();

	int counter = 0;

	/// <summary>
	/// Whether some HUD text is visible.
	/// </summary>

	public bool isVisible { get { return mList.Count != 0; } }

	/// <summary>
	/// Font used by the HUD text. Conveniently wraps both dynamic and bitmap fonts into one property.
	/// </summary>

	public Object ambigiousFont
	{
		get
		{
			if (trueTypeFont != null) return trueTypeFont;
			if (bitmapFont != null) return bitmapFont;
			return font;
		}
		set
		{
			if (value is Font)
			{
				trueTypeFont = value as Font;
				bitmapFont = null;
				font = null;
			}
			else if (value is UIFont)
			{
				bitmapFont = value as UIFont;
				trueTypeFont = null;
				font = null;
			}
		}
	}

	/// <summary>
	/// Create a new entry, reusing an old entry if necessary.
	/// </summary>

	Entry Create ()
	{
		// See if an unused entry can be reused
		if (mUnused.Count > 0)
		{
			Entry ent = mUnused[mUnused.Count - 1];
			mUnused.RemoveAt(mUnused.Count - 1);
			ent.time = Time.realtimeSinceStartup;
			ent.label.depth = NGUITools.CalculateNextDepth(gameObject);
			NGUITools.SetActive(ent.label.gameObject, true);
			ent.offset = 0f;
			mList.Add(ent);
			return ent;
		}
		
		// New entry
		Entry ne = new Entry();
		ne.time = Time.realtimeSinceStartup;
		ne.label = NGUITools.AddWidget<UILabel>(gameObject);
		ne.label.name = counter.ToString();
		ne.label.ambigiousFont = ambigiousFont;
		ne.label.fontSize = fontSize;
		ne.label.fontStyle = fontStyle;
		ne.label.applyGradient = applyGradient;
		ne.label.gradientTop = gradientTop;
		ne.label.gradientBottom = gradienBottom;
		ne.label.effectStyle = effect;
		ne.label.effectColor = effectColor;
		ne.label.overflowMethod = UILabel.Overflow.ResizeFreely;

		// Make it small so that it's invisible to start with
		ne.label.cachedTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		mList.Add(ne);
		++counter;
		return ne;
	}

	/// <summary>
	/// Delete the specified entry, adding it to the unused list.
	/// </summary>

	void Delete (Entry ent)
	{
		mList.Remove(ent);
		mUnused.Add(ent);
		NGUITools.SetActive(ent.label.gameObject, false);
	}

	/// <summary>
	/// Add a new scrolling text entry.
	/// </summary>

	public void Add (object obj, Color c, float stayDuration)
	{
		if (!enabled) return;

		float time = Time.realtimeSinceStartup;
		bool isNumeric = false;
		float val = 0f;

		if (obj is float)
		{
			isNumeric = true;
			val = (float)obj;
		}
		else if (obj is int)
		{
			isNumeric = true;
			val = (int)obj;
		}

		if (isNumeric)
		{
			if (val == 0f) return;

			for (int i = mList.Count; i > 0; )
			{
				Entry ent = mList[--i];
				if (ent.time + 1f < time) continue;

				if (ent.val != 0f)
				{
					if (ent.val < 0f && val < 0f)
					{
						ent.val += val;
						ent.label.text = Mathf.RoundToInt(ent.val).ToString();
						return;
					}
					else if (ent.val > 0f && val > 0f)
					{
						ent.val += val;
						ent.label.text = "+" + Mathf.RoundToInt(ent.val);
						return;
					}
				}
			}
		}

		// Create a new entry
		Entry ne = Create();
		ne.stay = stayDuration;
		ne.label.color = c;
		ne.label.alpha = 0f;
		ne.val = val;

		if (isNumeric) ne.label.text = (val < 0f ? Mathf.RoundToInt(ne.val).ToString() : "+" + Mathf.RoundToInt(ne.val));
		else ne.label.text = obj.ToString();

		// Sort the list
		mList.Sort(Comparison);
	}

	/// <summary>
	/// Auto-upgrade legacy font references.
	/// </summary>

	void OnEnable ()
	{
		if (font != null)
		{
			if (font.isDynamic)
			{
				trueTypeFont = font.dynamicFont;
				fontStyle = font.dynamicFontStyle;
				mUseDynamicFont = true;
			}
			else if (bitmapFont == null)
			{
				bitmapFont = font;
				mUseDynamicFont = false;
			}
			font = null;
#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
#endif
		}
	}

	bool mUseDynamicFont = false;

	void OnValidate ()
	{
		Font ttf = trueTypeFont;
		UIFont fnt = bitmapFont;

		bitmapFont = null;
		trueTypeFont = null;

		if (ttf != null && (fnt == null || !mUseDynamicFont))
		{
			bitmapFont = null;
			trueTypeFont = ttf;
			mUseDynamicFont = true;
		}
		else if (fnt != null)
		{
			// Auto-upgrade from 3.0.2 and earlier
			if (fnt.isDynamic)
			{
				trueTypeFont = fnt.dynamicFont;
				fontStyle = fnt.dynamicFontStyle;
				fontSize = fnt.defaultSize;
				mUseDynamicFont = true;
			}
			else
			{
				bitmapFont = fnt;
				mUseDynamicFont = false;
			}
		}
		else
		{
			trueTypeFont = ttf;
			mUseDynamicFont = true;
		}
	}

	/// <summary>
	/// Disable all labels when this script gets disabled.
	/// </summary>

	void OnDisable ()
	{
		for (int i = mList.Count; i > 0; )
		{
			Entry ent = mList[--i];
			if (ent.label != null) ent.label.enabled = false;
			else mList.RemoveAt(i);
		}
	}

	/// <summary>
	/// Update the position of all labels, as well as update their size and alpha.
	/// </summary>

	void Update ()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying) return;
#endif
		float time = RealTime.time;

		Keyframe[] offsets = offsetCurve.keys;
		Keyframe[] alphas = alphaCurve.keys;
		Keyframe[] scales = scaleCurve.keys;

		float offsetEnd = offsets[offsets.Length - 1].time;
		float alphaEnd = alphas[alphas.Length - 1].time;
		float scalesEnd = scales[scales.Length - 1].time;
		float totalEnd = Mathf.Max(scalesEnd, Mathf.Max(offsetEnd, alphaEnd));

		// Adjust alpha and delete old entries
		for (int i = mList.Count; i > 0; )
		{
			Entry ent = mList[--i];
			float currentTime = time - ent.movementStart;
			ent.offset = offsetCurve.Evaluate(currentTime);
			ent.label.alpha = alphaCurve.Evaluate(currentTime);

			// Make the label scale in
			float s = scaleCurve.Evaluate(time - ent.time);
			if (s < 0.001f) s = 0.001f;
			ent.label.cachedTransform.localScale = new Vector3(s, s, s);

			// Delete the entry when needed
			if (currentTime > totalEnd) Delete(ent);
			else ent.label.enabled = true;
		}

		float offset = 0f;

		// Move the entries
		for (int i = mList.Count; i > 0; )
		{
			Entry ent = mList[--i];
			offset = Mathf.Max(offset, ent.offset);
			ent.label.cachedTransform.localPosition = new Vector3(0f, offset, 0f);
			offset += Mathf.Round(ent.label.cachedTransform.localScale.y * ent.label.fontSize);
		}
	}
}
