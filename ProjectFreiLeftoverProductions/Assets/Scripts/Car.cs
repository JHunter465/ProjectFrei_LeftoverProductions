using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour {
	[SerializeField] private float maxSpeed = 1;
	[SerializeField] private float accelerationRate = .1f;
	[SerializeField] private float brakeRate = .1f;
	[SerializeField] private float decelaration = 2f;

	// Speed of the car upon scene initialization
	[SerializeField] private float startSpeed = 0;

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
		rb.velocity += Vector3.ClampMagnitude(accelerationRate * Vector3.forward, maxSpeed);
	}

	public void Brake() {
		// TODO Warning this assumes position along other axis than just forward is locked.
		rb.velocity -= Vector3.ClampMagnitude(brakeRate * Vector3.forward, rb.velocity.magnitude);
	}
}