using UnityEngine;
using UnityEngine.UI;

public class SuspcicionMeter : MonoBehaviour {
	[SerializeField] private BorderGuard guard;

    [SerializeField] private float emptyAngle = -90;
    [SerializeField] private float fullAngle = 90;

	private void Update() {
        // Update visual to current suspicion level
        transform.localRotation = Quaternion.Euler(Mathf.Abs(fullAngle - emptyAngle) * (guard.SuspicionLevel / 100) + emptyAngle, 0, 0);
	}
}