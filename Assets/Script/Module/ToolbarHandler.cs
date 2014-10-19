using UnityEngine;
using System.Collections;

public class ToolbarHandler : MonoBehaviour {

	public static ToolbarHandler Instance;
	void Awake () {
		Instance = this;
	}

	public int MAX_STEP = 512;
	public UIInput inpDragStep;

	void Start () {
		inpDragStep.value = ""+DragableObject1.step.x;
	}

	void Update () {

	}

	public void OnSubmitDragStep () {

		int step;
		if (int.TryParse (inpDragStep.value, out step)) {
			if (step >= 1 && step <= MAX_STEP) {
				Main.Instance.log.text = "Step = " + step;
				DragableObject1.step = new Vector2 (step, step);
			}
		}
	}
}
