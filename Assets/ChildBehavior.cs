﻿using UnityEngine;
using System.Collections;

public class ChildBehavior : Interactable {

	private Transform cam;
	private Rigidbody heldObj = null;
	public float held_x = 0.5f;
	public float held_y = 0.5f;
	public float held_z = 1.0f;


	void Start () {
        gameObject.GetComponentInChildren<Animation>().wrapMode = WrapMode.Loop;
	}

	private void FixedUpdate()
	{
		// held object physics
		if (heldObj != null)
			heldObj.MovePosition (Camera.main.ViewportToWorldPoint (new Vector3 (held_x, held_y, held_z)));
	}

	public override void OnStart() {}
	
	public override void Interact()
	{
		if (heldObj == null) {
			heldObj = this.gameObject.GetComponent<Rigidbody> ();
			Pickup (heldObj);
		} else {
			Drop ();
		}
	}

	private void Pickup(Rigidbody obj) {
		obj.useGravity = false;
		obj.transform.parent = Camera.main.transform;
		obj.freezeRotation = true;
		obj.GetComponentInChildren<Animation> ().Play ("flying");

		heldObj = obj;
	}

	private void Drop() {
		heldObj.useGravity = true;
		heldObj.transform.parent = null;
		heldObj.freezeRotation = false;
		heldObj.GetComponentInChildren<Animation> ().Play ("idle");
		heldObj = null;
	}
}
