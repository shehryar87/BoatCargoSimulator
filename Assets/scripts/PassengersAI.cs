using UnityEngine;
using System.Collections;

public class PassengersAI : MonoBehaviour {
	
	public float rotaionSpeed;
	public Transform target;
	public static bool move;
	
	public float moveSpeed;
	public Vector3 center;
	public GameObject passengers;
	private Animator anim;
	
	private Vector3 direction;
	private Quaternion lokRotation;
	
	void Start () {
		gameObject.GetComponent<Rigidbody>().centerOfMass = center;
		anim = gameObject.GetComponent<Animator> ();
		anim.enabled = true;
		move = false;
		
	}
	
	void Update () {
		
		if (move) {
			anim.SetBool ("move", true);
			transform.position = Vector3.MoveTowards (transform.position, target.position, moveSpeed);
			direction = (target.position - transform.position).normalized;
			lokRotation = Quaternion.LookRotation (direction);
			transform.rotation = Quaternion.Slerp (transform.rotation, lokRotation, rotaionSpeed * Time.deltaTime);
		} else {
			anim.SetBool ("move", false);
		}
		
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "PassengerTarget"){
			passengers.SetActive(true);
			PassengerTarget.count++;
			Destroy(gameObject);
		}
	}
	
	
}
