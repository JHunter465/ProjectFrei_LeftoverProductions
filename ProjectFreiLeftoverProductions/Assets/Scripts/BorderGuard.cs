using System.Linq;
using UnityEngine;

public class BorderGuard : MonoBehaviour {
	[SerializeField] private GuardEyes eyePos;
//	[SerializeField] private ContainerWatcher watcher;

	[SerializeField] private LayerMask containerLayer;

	[Space(10)]
	[SerializeField, Range(0, 100)]
	private float suspicionLevel;

	private LevelManager lvlMgr;

	public float SuspicionLevel => suspicionLevel;

	private void Awake() {
		lvlMgr = GameObject.FindGameObjectWithTag(LevelManager._lvlMgrTag).GetComponent<LevelManager>();
	}

	private void RaiseSuspicionLevel(float amt) {
		suspicionLevel += amt;
	}

	private void Update() {
		foreach (InteractableItem i in lvlMgr.items.Where(i => i.HasMoved())) {
			// Check if guard can see this item
			Physics.Linecast(eyePos.transform.position, i.transform.position, out RaycastHit info, ~containerLayer);
			Debug.Log("hit: " + info.collider.name);
		}
		
		// Check if suspicion level is too high
		
	}
}