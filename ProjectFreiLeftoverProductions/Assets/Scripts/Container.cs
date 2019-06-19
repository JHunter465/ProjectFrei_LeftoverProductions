using System;
using UnityEngine;
using Valve.VR.InteractionSystem;
// ReSharper disable SwitchStatementMissingSomeCases

[DisallowMultipleComponent]
public class Container : MonoBehaviour {
	public enum BooleanCheck {
		Equal,
		SmallerThan,
		LargerThan,
		InRange,
		OutRange
	}

	[SerializeField] private bool open = true;
	public bool Open {
		get => open;
		private set => open = value;
	}

	[SerializeField] private LinearMapping lm;
	[SerializeField] private BooleanCheck bc;

	[SerializeField] private bool inclusive;
	[SerializeField] private float value;
	[SerializeField] private float rangeMin;
	[SerializeField] private float rangeMax;

	public BooleanCheck CheckMode => bc;

	private void Update() {
		bool comparison = false;

		if (bc == BooleanCheck.Equal) {
			comparison = Math.Abs(lm.value - value) < .01f;
		}
		else if (inclusive) {
			switch (bc) {
				case BooleanCheck.LargerThan:
					comparison = lm.value >= value;
					break;
				case BooleanCheck.SmallerThan:
					comparison = lm.value <= value;
					break;
				case BooleanCheck.InRange:
					comparison = lm.value >= rangeMin && lm.value <= rangeMax;
					break;
				case BooleanCheck.OutRange:
					comparison = lm.value <= rangeMin || lm.value >= rangeMax;
					break;
			}
		}
		else if (!inclusive) {
			switch (bc) {
				case BooleanCheck.LargerThan:
					comparison = lm.value > value;
					break;
				case BooleanCheck.SmallerThan:
					comparison = lm.value < value;
					break;
				case BooleanCheck.InRange:
					comparison = lm.value > rangeMin && lm.value < rangeMax;
					break;
				case BooleanCheck.OutRange:
					comparison = lm.value < rangeMin || lm.value > rangeMax;
					break;
			}
		}

		open = comparison;
	}
}