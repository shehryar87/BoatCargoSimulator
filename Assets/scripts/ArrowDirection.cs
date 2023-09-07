using UnityEngine;
using System.Collections;

public class ArrowDirection : MonoBehaviour {

	public Transform _1;
	public Transform _2;
	public Transform _3;
	public Transform _4;
	public Transform _5;
	public Transform _6;
	public Transform _7;
	public Transform _8;
	public Transform _9;
	public Transform _10;
	public Transform _11;
	public Transform _12;
	public Transform _13;
	public Transform _14;
	public Transform _15;
	public Transform _16;
	public Transform _17;
	public Transform _18;
	public Transform _19;
	public Transform _20;

	private int n;
	private Transform _currentWaypoint = null;
	private Vector3 temp;

	void Start(){
		n = PlayerPrefs.GetInt ("StartLevel",1);

		switch (n) {
		case 1:
			SetCurrentWaypoint(_1);
			break;
		case 2:
			SetCurrentWaypoint(_2);
			break;
		case 3:
			SetCurrentWaypoint(_3);
			break;
		case 4:
			SetCurrentWaypoint(_4);
			break;
		case 5:
			SetCurrentWaypoint(_5);
			break;
		case 6:
			SetCurrentWaypoint(_6);
			break;
		case 7:
			SetCurrentWaypoint(_7);
			break;
		case 8:
			SetCurrentWaypoint(_8);
			break;
		case 9:
			SetCurrentWaypoint(_9);
			break;
		case 10:
			SetCurrentWaypoint(_10);
			break;
		case 11:
			SetCurrentWaypoint(_11);
			break;
		case 12:
			SetCurrentWaypoint(_12);
			break;
		case 13:
			SetCurrentWaypoint(_13);
			break;
		case 14:
			SetCurrentWaypoint(_14);
			break;
		case 15:
			SetCurrentWaypoint(_15);
			break;
		case 16:
			SetCurrentWaypoint(_16);
			break;
		case 17:
			SetCurrentWaypoint(_17);
			break;
		case 18:
			SetCurrentWaypoint(_18);
			break;
		case 19:
			SetCurrentWaypoint(_19);
			break;
		case 20:
			SetCurrentWaypoint(_20);
			break;

		}
	}
	public void SetCurrentWaypoint(Transform Waypoint)
	{
		_currentWaypoint = Waypoint;
	}
	
	
	void Update()
	{
		if (_currentWaypoint != null)
		{
			transform.LookAt(_currentWaypoint);
			temp = transform.eulerAngles;
			temp.x = 0;//-14.0f;
			transform.eulerAngles = temp;

//			Vector3 relativePos = _currentWaypoint.transform.position  - transform.position;
//			Quaternion rotation = Quaternion.LookRotation(relativePos);
//			rotation.x = 0;
//			transform.rotation = rotation;

		}  
	}
}
