using System.Collections;
using System.Linq;
using UnityEngine;

public class BorderGuard : MonoBehaviour {
	[SerializeField] private GuardEyes eyePos;         // Position of the guard's eyes
	[SerializeField] private LayerMask containerLayer; // Layermask for container colliders

	[Space(10), SerializeField, Range(0, 100)]
	private float suspicionLevel; // Suspicion level of the guard

	public float SuspicionLevel => suspicionLevel; // Accessor property for suspicion
	private float targetSuspicion;                 // Used to lerp to new suspicion
	[SerializeField] private float meterSpeed = 1; // Lerp speed setting

	private bool active;         // Defines if the border guard is active (i.e. looking for suspicious objects)
	private ItemWatcher watcher; // Used to obtain all items in the car

	private void ClampMeterSpeed() {
		// Set meter speed to 1 if below 0.01 (don't allow (near) zero values)
		meterSpeed = meterSpeed <= 0.01 ? 1 : meterSpeed;
	}

	private void OnValidate() {
		ClampMeterSpeed();
	}

	private void Start() {
		// Register item watcher
		watcher = GameObject.FindGameObjectWithTag(ItemWatcher._itemWatcherTag).GetComponent<ItemWatcher>();

		ClampMeterSpeed();
	}

	public void AddSuspicion(float amt) {
		// Set new target for suspicion
		// Suspicion is a value between 0 and 100
		targetSuspicion = Mathf.Clamp(targetSuspicion + amt, 0, 100);
		SetSuspicionLevel(targetSuspicion);
	}

	private void SetSuspicionLevel(float newValue) {
		// Stop all coroutines currently editing the suspicion
		StopAllCoroutines();

		// Start new coroutine to lerp to new target pos
		StartCoroutine(LerpToSuspicionLevel(newValue));
	}

	private IEnumerator LerpToSuspicionLevel(float target) {
		// Lerp for as long as the value hasn't reached it's target
		while (Mathf.Abs(target - suspicionLevel) > 0.1) {
			suspicionLevel = Mathf.Lerp(suspicionLevel, target, Mathf.Sqrt(Time.deltaTime * meterSpeed));
			yield return null;
		}

		// Skip the last few decimals to the target to avoid floating point imprecision and save on lerp calculations
		suspicionLevel = target;
	}

	private void Update() {
		// Check if the current suspcicion level is too high
		CheckSuspicion();

		if (active) {
			UpdateItemVisibility();

			foreach (InteractableItem item in watcher.Items.Where(item => watcher.IsVisible(item))) {
				// TODO Do stuff with the visible items here
			}
		}
	}

	private void CheckSuspicion() {
		// If the suspicion is over 99, you lost the game
		// 99 feels better than 100 because the last 1% is approached very slowly due to lerp
		if (SuspicionLevel > 99) {
			LevelManager mgr = GameObject.FindWithTag(LevelManager._lvlMgrTag).GetComponent<LevelManager>();
			mgr.GameOver();
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