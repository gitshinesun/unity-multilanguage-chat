using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputfieldFocused : MonoBehaviour {

	public InputfieldSlideScreen slideScreen;
	public InputField inputField;


	void Start () {

		slideScreen = gameObject.GetComponentInParent<InputfieldSlideScreen>();
		inputField = transform.GetComponent<InputField>();
		
		inputField.shouldHideMobileInput=true;
		
	}
	void Update () {
		
	}

	public void OnInputFieldActive () {
		slideScreen.InputFieldActive = true;
		slideScreen.childRectTransform = transform.GetComponent<RectTransform>();
	}

	public void OnInputFieldDeActive () {
		inputField.DeactivateInputField();
		slideScreen.InputFieldActive = false;
	}
}
