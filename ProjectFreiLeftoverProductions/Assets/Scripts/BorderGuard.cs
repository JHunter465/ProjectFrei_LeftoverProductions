using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class BorderGuard : MonoBehaviour {
	[SerializeField] private GuardEyes eyePos;
	[SerializeField] private LayerMask containerLayer;

	[Space(10), SerializeField, Range(0, 100)] private float suspicionLevel;
	public float SuspicionLevel => suspicionLevel;

	public float DisplaySuspicion { get; private set; }

	[SerializeField] private float meterSpeed = 1;

	private bool active;
	private ItemWatcher watcher;

	private void OnValidate() {
		meterSpeed = meterSpeed <= 0.01 ? 1 : meterSpeed;
	}

	private void Start() {
		watcher = GameObject.FindGameObjectWithTag(ItemWatcher._itemWatcherTag).GetComponent<ItemWatcher>();
		meterSpeed = meterSpeed <= 0.01 ? 1 : meterSpeed;
	}

	public void AddSuspicion(float amt) {
		suspicionLevel = Mathf.Clamp(suspicionLevel + amt, 0, 100);
		ShowSuspicionLevel(suspicionLevel);
	}

	private void ShowSuspicionLevel(float newValue) {
		StopAllCoroutines();
		StartCoroutine(LerpToSuspicionLevel(newValue));
	}

	private IEnumerator LerpToSuspicionLevel(float target) {
		while (Mathf.Abs(target - DisplaySuspicion) > 0.1) {
			DisplaySuspicion = Mathf.Lerp(DisplaySuspicion, target, Mathf.Sqrt(Time.deltaTime * meterSpeed));
			yield return null;
		}

		DisplaySuspicion = target;
	}

	private void Update() {
		if (active) {
			UpdateItemVisibility();

			foreach (InteractableItem item in watcher.Items.Where(item => watcher.IsVisible(item))) {
				// TODO Do stuff with the visible items here
			}
		}
	}

	private void UpdateItemVisibility() {
		foreach (InteractableItem item in watcher.Items) {
			if (item.InContainer && !item.Container.Open) {
				// Item is in a closed container so it is invisible by definition
				// End loop here to save on raycasts being performed
				watcher.SetVisible(item, false);
				continue;
			}

			// Check if guard can see this item
			Physics.Linecast(eyePos.transform.position, item.transform.position, out RaycastHit info, ~containerLayer);
			// Guard can see item if the object that was hit has an InteractableItem component, use the result of the null check to set visibility
			watcher.SetVisible(item, info.collider.GetComponent<InteractableItem>() != null);
		}
	}

	public void Activate() {
		active = true;
	}
}