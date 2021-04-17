using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    void Clicked() {
		ButtonSelection buttonSelector = this.transform.parent.transform.parent.GetComponent<ButtonSelection>();
		buttonSelector.ButtonClick(this);
	}

}
