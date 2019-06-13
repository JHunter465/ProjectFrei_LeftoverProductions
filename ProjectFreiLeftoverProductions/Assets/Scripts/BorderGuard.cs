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
		foreach (InteractableItem i in watcher.items) {
			if (i.InContainer && !i.Container.Open) continue;

			// Check if guard can see this item
//			Debug.DrawLine(eyePos.transform.position, i.transform.position, Color.cyan);
			Physics.Linecast(eyePos.transform.position, i.transform.position, out RaycastHit info, ~containerLayer);
			if (info.collider.GetComponent<InteractableItem>()) {
				Debug.Log("Item " + i.name + " is visible");
			}
		}

		// Check if suspicion level is too high

	}
}