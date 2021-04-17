using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    
    public void PlayEasyGame(){
        Attributes.setGameTypePvC();
        Attributes.setGameDifficultyEasy();
        SceneManager.LoadScene(1);
    }

    public void PlayMediumGame(){
        Attributes.setGameTypePvC();
        Attributes.setGameDifficultyMedium();
        SceneManager.LoadScene(1);
    }

    public void PlayHardGame(){
        Attributes.setGameTypePvC();
        Attributes.setGameDifficultyHard();
        SceneManager.LoadScene(1);
    }

    public void PlayPvP(){
        Attributes.setGameTypePvP();
        SceneManager.LoadScene(1);
    }


    public void AuxPlayCvC(){

        GameObject buttonselector = GameObject.FindWithTag("ButtonSelector");

        ButtonSelection buttonSelection = buttonselector.GetComponent<ButtonSelection>();

        ButtonBehaviour[] bot1buttons = buttonSelection.bot1buttons;
        ButtonBehaviour[] bot2buttons = buttonSelection.bot2buttons;

        int difficulty1 = -1;
        int difficulty2 = -1;

        for (int i = 0; i < bot1buttons.Length; i++){
            Button test = bot1buttons[i].GetComponent<Button>();
            ColorBlock cb = test.colors;
            if(cb.normalColor == test.colors.selectedColor){
                difficulty1 = i;
            }
            test = bot2buttons[i].GetComponent<Button>();
            cb = test.colors;
            if(cb.normalColor == test.colors.selectedColor){
                difficulty2 = i;
            }
        }

        if (difficulty1 != -1 && difficulty2 != -1){
            PlayCvC(difficulty1, difficulty2);
        }
    }


    public void PlayCvC(int dif1, int dif2){
        if(dif1 == 0){
            Attributes.setGameDifficultyEasy();
        }else if(dif1 == 1){
            Attributes.setGameDifficultyMedium();
        }else if(dif1 == 2){
            Attributes.setGameDifficultyHard();
        }

        if(dif2 == 0){
            Attributes.setGameDifficultyEasy2();
        }else if(dif2 == 1){
            Attributes.setGameDifficultyMedium2();
        }else if(dif2 == 2){
            Attributes.setGameDifficultyHard2();
        }

        Attributes.setGameTypeCvC();
        SceneManager.LoadScene(1);
    }


    public void QuitGame(){
        Application.Quit();
    }
}
