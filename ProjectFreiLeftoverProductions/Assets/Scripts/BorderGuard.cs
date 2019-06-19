using System.Linq;
using UnityEngine;

public class BorderGuard : MonoBehaviour {
	[SerializeField] private GuardEyes eyePos;
	[SerializeField] private LayerMask containerLayer;

	[Space(10)]
	[SerializeField, Range(0, 100)]
	private float suspicionLevel;

	public float SuspicionLevel => suspicionLevel;

	private ItemWatcher watcher;

	private void Start() {
		watcher = GameObject.FindGameObjectWithTag(ItemWatcher._itemWatcherTag).GetComponent<ItemWatcher>();
	}

	private void SetSuspicionLevel(float newValue) {
		suspicionLevel = Mathf.Clamp(newValue, 0, 100);
	}

	public void RaiseSuspicionLevel(float amt) {
		SetSuspicionLevel(suspicionLevel + amt);
	}

	private void Update() {
		// TODO only do this if window down (interaction started)
		UpdateItemVisibility();

		foreach (InteractableItem item in watcher.Items.Where(item => watcher.IsVisible(item))) {
			// TODO Do stuff with the visible items here
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
	
}