using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Container : MonoBehaviour {
//	private HashSet<InteractableItem> items;
//	private LevelManager lvlMgr;
//
//	private void Awake() {
//		lvlMgr = GameObject.FindGameObjectWithTag(LevelManager._lvlMgrTag).GetComponent<LevelManager>();
//	}
//
//	public void RegisterItem(InteractableItem item) {
//		
//	}
//
//	public void DeregisterItem(InteractableItem item) {
//		
//	}

	[SerializeField] private bool open = true;
	public bool Open {
		get => open;
		private set => open = value;
	}

}