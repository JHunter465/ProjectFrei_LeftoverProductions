using System.Collections.Generic;
using UnityEngine;

public class ItemWatcher : MonoBehaviour {
	public const string _itemWatcherTag = "ItemWatcher";
	
	// Todo this could probably be solved using a single collection
	private readonly HashSet<InteractableItem> items = new HashSet<InteractableItem>();
	private readonly Dictionary<InteractableItem, bool> visibility = new Dictionary<InteractableItem, bool>();

	// Public collection accessor property
	public ICollection<InteractableItem> Items => items;
	
	private void Awake() {
		tag = _itemWatcherTag;
		
	}

	public void RegisterItem(InteractableItem item) {
		if (items.Add(item)) {
			visibility.Add(item, false);
		}
		else {
			Debug.LogWarning("Tried to add item " + item.name + " while it was already registered.");
		}
	}

	public void DeregisterItem(InteractableItem item) {
		if (items.Remove(item)) {
			visibility.Add(item, false);
		}
		else {
			Debug.LogWarning("Tried to remove item " + item.name + " while it was not registered.");
		}
	}

	public void SetVisible(InteractableItem item, bool visible) {
		if (items.Contains(item)) {
			visibility[item] = visible;
		}
		else {
			Debug.LogWarning("Item is not registered to the ItemWatcher!");
		}
	}

	public bool IsVisible(InteractableItem item) {
		return visibility[item];
	}
}