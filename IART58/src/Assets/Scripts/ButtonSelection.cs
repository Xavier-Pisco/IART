using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSelection : MonoBehaviour
{
    // Start is called before the first frame update

    public ButtonBehaviour[] bot1buttons;
    public ButtonBehaviour[] bot2buttons;

    void Start(){

    }

    public void ButtonClick(ButtonBehaviour button) {

        if(bot1buttons.Contains(button)){
            for (int i = 0; i < bot1buttons.Length; i++){
                Button test = bot1buttons[i].GetComponent<Button>();
                ColorBlock cb = test.colors;
                cb.normalColor = new Color (0f, 0f, 0f, 0f);
                test.colors = cb;
            }
		    Button test1 = button.GetComponent<Button>();
            ColorBlock cb1 = test1.colors;
            cb1.normalColor = test1.colors.selectedColor;
            test1.colors = cb1;
        }

        if(bot2buttons.Contains(button)){
            for (int i = 0; i < bot2buttons.Length; i++){
                Button test = bot2buttons[i].GetComponent<Button>();
                ColorBlock cb = test.colors;
                cb.normalColor = new Color (0f, 0f, 0f, 0f);
                test.colors = cb;
            }
		    Button test1 = button.GetComponent<Button>();
            ColorBlock cb1 = test1.colors;
            cb1.normalColor = test1.colors.selectedColor;
            test1.colors = cb1;
        }
	}
}
