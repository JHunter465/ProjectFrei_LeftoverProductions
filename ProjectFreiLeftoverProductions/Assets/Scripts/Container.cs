using UnityEngine;

[DisallowMultipleComponent]
public class Container : MonoBehaviour {
	[SerializeField] private bool open = true;
	public bool Open {
		get => open;
		private set => open = value;
	}
}