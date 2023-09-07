using UnityEngine;

[RequireComponent(typeof(ImprovedTrail))]
[AddComponentMenu("Exploration/Ship Trail")]
public class ShipTrail : MonoBehaviour
{
	public BoatController control;

	public ImprovedTrail mTrail;

	void Start ()
	{
		mTrail = GetComponent<ImprovedTrail>();
	}

	void Update ()
	{
		if (control != null)
		{
			mTrail.alpha = control.motor;
		}
	}
}