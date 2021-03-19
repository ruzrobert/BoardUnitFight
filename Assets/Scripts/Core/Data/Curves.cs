using UnityEngine;

public static class Curves
{
	public static readonly AnimationCurve straight = CreateStraightCurve();
	public static readonly AnimationCurve linear = CreateLinearCurve();
	public static readonly AnimationCurve smooth = CreateSmoothCurve();
	public static readonly AnimationCurve easeIn = CreateEaseInCurve();
	public static readonly AnimationCurve easeOut = CreateEaseOutCurve();

	public static AnimationCurve CreateStraightCurve(float height = 1)
	{
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, height));
		curve.AddKey(new Keyframe(1, height));
		return curve;
	}

	public static AnimationCurve CreateLinearCurve(float height = 1)
	{
		float tan45 = Mathf.Tan(Mathf.Deg2Rad * 45);

		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, tan45, tan45));
		curve.AddKey(new Keyframe(1, height, tan45, tan45));
		return curve;
	}

	public static AnimationCurve CreateSmoothCurve(float height = 1)
	{
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, 0, 0));
		curve.AddKey(new Keyframe(1, height, 0, 0));
		return curve;
	}

	public static AnimationCurve CreateEaseInCurve(float height = 1)
	{
		float tan45 = Mathf.Tan(Mathf.Deg2Rad * 45);
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, 0, 0));
		curve.AddKey(new Keyframe(1, height, tan45, tan45));
		return curve;
	}

	public static AnimationCurve CreateEaseOutCurve(float height = 1)
	{
		float tan45 = Mathf.Tan(Mathf.Deg2Rad * 45);
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, tan45, tan45));
		curve.AddKey(new Keyframe(1, height, 0, 0));
		return curve;
	}
}