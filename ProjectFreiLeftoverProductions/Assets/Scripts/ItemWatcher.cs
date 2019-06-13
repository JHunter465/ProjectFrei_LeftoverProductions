using System.Collections.Generic;
using UnityEngine;

public class ItemWatcher : MonoBehaviour {
	public const string _itemWatcherTag = "ItemWatcher";
	
	public readonly HashSet<InteractableItem> items = new HashSet<InteractableItem>();

	private void Awake() {
		tag = _itemWatcherTag;
	}

	public void RegisterItem(InteractableItem item) {
		if (! items.Add(item)) {
			Debug.LogWarning("Tried to add item " + item.name + " while it was already registered.");
		}
	}

	public void DeregisterItem(InteractableItem item) {
		if (! items.Remove(item)) {
			Debug.LogWarning("Tried to remove item " + item.name + " while it was not registered.");
		}
	}
}