using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {
	private HashSet<InteractableItem> items;
	private LevelManager lvlMgr;

	private void Awake() {
		lvlMgr = GameObject.FindGameObjectWithTag(LevelManager._lvlMgrTag).GetComponent<LevelManager>();
	}

	public void RegisterItem(InteractableItem item) {
		if (items.Add(item)) {
			
		}
	}

	public void DeregisterItem(InteractableItem item) {
		
	}
}