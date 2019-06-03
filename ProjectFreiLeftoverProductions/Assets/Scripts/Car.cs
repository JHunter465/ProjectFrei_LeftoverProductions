using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour {
	[SerializeField] private float maxSpeed = 10;
	[SerializeField] private float accelerationRate = 50;
	[SerializeField] private float brakeRate = 50;
	[SerializeField] private float decelaration = 3;

	// This is a player car if there is no AiCar component attached to the gameobject;
	public bool IsPlayerCar => GetComponent<AiCar>() == null;

	// Return current velocity or zero if rigidbody has not yet been set (before initialization)
	public Vector3 Velocity => rb != null ? rb.velocity : Vector3.zero;

	// Rigidbody component ref
	private Rigidbody rb;

	private void OnValidate() {
		rb = GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
		rb.drag = decelaration;
	}

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}
	
	// TODO maybe optimize this by clamping the accelerationRate instead of the entire velocity vector.
	public void Accelerate() {
		rb.velocity = Vector3.ClampMagnitude(rb.velocity + accelerationRate * Vector3.forward * Time.fixedDeltaTime, maxSpeed);
	}

	public void Brake() {
		// TODO Warning this assumes position along other axis than just forward is locked.
		rb.velocity -= Vector3.ClampMagnitude(brakeRate * Vector3.forward * Time.fixedDeltaTime, rb.velocity.magnitude);
	}
}