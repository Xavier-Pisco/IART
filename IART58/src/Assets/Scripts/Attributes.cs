using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes
{

    public static GameType gameType;
    public static int heuristic;
    public static int depth;

    public static int heuristic2;
    public static int depth2;

    public static void setGameDifficultyEasy(){
        heuristic = 5;
        depth = 2;
    }

    public static void setGameDifficultyMedium(){
        heuristic = 2;
        depth = 4;
    }

    public static void setGameDifficultyHard(){
        heuristic = 4;
        depth = 6;
    }
    
    public static int GetGameDifficultyDepth(){
        return depth;
    }

    public static int GetGameDifficultyHeuristic(){
        return heuristic;
    }

    public static void setGameDifficultyEasy2(){
        heuristic2 = 1;
        depth2 = 2;
    }

    public static void setGameDifficultyMedium2(){
        heuristic2 = 2;
        depth2 = 4;
    }

    public static void setGameDifficultyHard2(){
        heuristic2 = 3;
        depth2 = 6;
    }


    public static int GetGameDifficultyDepth2(){
        return depth2;
    }

    public static int GetGameDifficultyHeuristic2(){
        return heuristic2;
    }


    public enum GameType
    {
        PvP,
        PvC,
        CvC
    }

    public static void setGameTypePvP(){
        gameType = GameType.PvP;
    }

    public static void setGameTypePvC(){
        gameType = GameType.PvC;
    }

    public static void setGameTypeCvC(){
        gameType = GameType.CvC;
    }

    public static GameType GetGameType(){
        return gameType;
    }


    
    
}
