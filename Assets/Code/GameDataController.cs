using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
    public static GameDataController instance;
    static int currentLevel = 1;
    static int lastLevel = 5;

    public static void setLevel(int level) {
        currentLevel = level;
    }

    public static int getLevel() {
        return currentLevel;
    }

    public static void incrementLevel() {
        currentLevel++;
    }

    public static int getLastLevel() {
        return lastLevel;
    }
}
