using System;
using UnityEngine;

[DisallowMultipleComponent]
public class InteractableItem : MonoBehaviour {
	[SerializeField] private float moveThreshold = 0.0f;
	
	private Vector3 startPos;
	private ItemWatcher watcher;

	// Current Container the item is in
	public Container Container { get; private set; }

	/* State flags */
	public bool InContainer => Container != null;

	private void Start() {
		// Save the object's position at start of level
		startPos = transform.position;

		// Register to item watcher (necessary to be seen by border guard)
		try {
			watcher = GameObject.FindGameObjectWithTag(ItemWatcher._itemWatcherTag).GetComponent<ItemWatcher>();
			watcher.RegisterItem(this);
		}
		catch (UnityException e) {
			Debug.LogError("No ItemWatcher was found in scene!");
		}
	}

	public bool HasMoved() {
		try {
			return Vector3.Distance(startPos, transform.position) > moveThreshold;
		}
		catch (NullReferenceException e) {
			throw new InvalidOperationException("No start position set");
		}
	}

	private void MoveToStartPos() {
		try {
			transform.position = startPos;
		}
		catch (NullReferenceException e) {
			throw new InvalidOperationException("No start position set");
		}
	}

	private void OnTriggerEnter(Collider other) {
		// Don't update Container when there is already a Container registered (prevents bugs with overlapping containers)
		if (Container) return;
		
		// Load the new Container the item just entered (stays null when the collider was not a Container)
		Container c = other.GetComponent<Container>();
		if (c) EnterContainer(c);
	}

	private void OnTriggerExit(Collider other) {
		// Don't check if currently not in a Container
		if (!Container) return;

		// Check if the Container that was left was the same as the Container it is currently in
		Container otherContainer = other.GetComponent<Container>();
		if (Container == otherContainer) {
			// Reset the Container field to null (the InContainer flag checks for null to show if this item is in a Container)
			Container = null;
			if (Debug.isDebugBuild) Debug.Log("InteractableItem " + name + " left Container " + otherContainer.name);
		}
	}

	private void OnTriggerStay(Collider other) {
		// Handle overlapping containers
		// If the Container is null but the object is currently still in a Container trigger, update the current 
		if (!Container) {
			Container c = other.GetComponent<Container>();
			if (c) EnterContainer(c);
		}
	}

	private void EnterContainer(Container c) {
		// Can only enter Container if it is currently null
		if (!Container) {
			Container = c;
			if (Debug.isDebugBuild) Debug.Log("InteractableItem " + name + " entered Container " + Container.name);
		}
	}
}