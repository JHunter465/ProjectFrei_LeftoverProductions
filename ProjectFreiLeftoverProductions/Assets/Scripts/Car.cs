using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	[SerializeField] private float maxSpeed = 1;
	[SerializeField] private float accelerationRate = .1f;
	[SerializeField] private float brakeForce = .1f;

	[SerializeField] private float startSpeed = 0;

	private float speed;

	private void Start() {
		speed = startSpeed;
	}

	private void Update() {
		DoMovement();
	}

	private void DoMovement() {
		transform.position += Vector3.forward * speed;
	}
	
	public void Accelerate() {
		Mathf.Clamp(speed += accelerationRate, 0, maxSpeed);
	}

	public void Brake() {
		Mathf.Clamp(speed -= brakeForce, 0, maxSpeed);
	}
}