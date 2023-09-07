//----------------------------------------------
//            NJG MiniMap (NGUI)
// Copyright � 2014 Ninjutsu Games LTD.
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This is a script used to keep the game object scaled to 2/(Screen.height).
/// </summary>

[ExecuteInEditMode]
public class GUIRoot : MonoBehaviour
{
	public enum Scaling
	{
		PixelPerfect,
		FixedSize,
		FixedSizeOnMobiles,
	}

	/// <summary>
	/// Type of scaling used by the UIRoot.
	/// </summary>

	public Scaling scalingStyle = Scaling.FixedSize;

	/// <summary>
	/// Height of the screen when 'automatic' is turned off.
	/// </summary>

	public int manualHeight = 720;

	/// <summary>
	/// If the screen height goes below this value, it will be as if the scaling style
	/// is set to FixedSize with manualHeight of this value.
	/// </summary>

	public int minimumHeight = 320;

	/// <summary>
	/// If the screen height goes above this value, it will be as if the scaling style
	/// is set to FixedSize with manualHeight of this value.
	/// </summary>

	public int maximumHeight = 1536;

	/// <summary>
	/// UI Root's active height, based on the size of the screen.
	/// </summary>

	public int activeHeight
	{
		get
		{
			int height = Mathf.Max(2, Screen.height);
			if (scalingStyle == Scaling.FixedSize) return manualHeight;

#if UNITY_IPHONE || UNITY_ANDROID
			if (scalingStyle == Scaling.FixedSizeOnMobiles)
				return manualHeight;
#endif
			if (height < minimumHeight) return minimumHeight;
			if (height > maximumHeight) return maximumHeight;
			return height;
		}
	}

	/// <summary>
	/// Pixel size adjustment. Most of the time it's at 1, unless 'automatic' is turned off.
	/// </summary>

	public float pixelSizeAdjustment { get { return GetPixelSizeAdjustment(Screen.height); } }

	/// <summary>
	/// Helper function that figures out the pixel size adjustment for the specified game object.
	/// </summary>

	static public float GetPixelSizeAdjustment(GameObject go)
	{
		GUIRoot root = NJGTools.FindInParents<GUIRoot>(go);
		return (root != null) ? root.pixelSizeAdjustment : 1f;
	}

	/// <summary>
	/// Calculate the pixel size adjustment at the specified screen height value.
	/// </summary>

	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);

		if (scalingStyle == Scaling.FixedSize)
			return (float)manualHeight / height;

#if UNITY_IPHONE || UNITY_ANDROID
		if (scalingStyle == Scaling.FixedSizeOnMobiles)
			return (float)manualHeight / height;
#endif
		if (height < minimumHeight) return (float)minimumHeight / height;
		if (height > maximumHeight) return (float)maximumHeight / height;
		return 1f;
	}

	Transform mTrans;

	void Awake()
	{
		mTrans = transform;
	}

	void Update()
	{
		if (mTrans != null)
		{
			float calcActiveHeight = activeHeight;

			if (calcActiveHeight > 0f)
			{
				float size = 2f / calcActiveHeight;

				Vector3 ls = mTrans.localScale;

				if (!(Mathf.Abs(ls.x - size) <= float.Epsilon) ||
					!(Mathf.Abs(ls.y - size) <= float.Epsilon) ||
					!(Mathf.Abs(ls.z - size) <= float.Epsilon))
				{
					mTrans.localScale = new Vector3(size, size, size);
				}
			}
		}
	}

}
