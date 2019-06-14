using System.Linq;
using UnityEngine;

public class BorderGuard : MonoBehaviour {
	[SerializeField] private GuardEyes eyePos;

	[SerializeField] private LayerMask containerLayer;

//	[Space(10)]
//	[SerializeField, Range(0, 100)]
//	private float suspicionLevel;

	private ItemWatcher watcher;

//	public float SuspicionLevel => suspicionLevel;

	private void Start() {
		watcher = GameObject.FindGameObjectWithTag(ItemWatcher._itemWatcherTag).GetComponent<ItemWatcher>();
	}

//	private void RaiseSuspicionLevel(float amt) {
//		suspicionLevel += amt;
//	}

	private void Update() {
		foreach (InteractableItem item in watcher.Items) {
			if (item.InContainer && !item.Container.Open) {
				watcher.SetVisible(item, false);
				continue;
			}

			// Check if guard can see this item
			Physics.Linecast(eyePos.transform.position, item.transform.position, out RaycastHit info, ~containerLayer);
			if (info.collider.GetComponent<InteractableItem>()) {
				watcher.SetVisible(item, true);
			}
			else {
				watcher.SetVisible(item, false);
			}
		}

		// Check if suspicion level is too high

	}
}