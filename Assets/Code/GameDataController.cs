using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
    public static GameDataController instance;
    static int currentLevel = 1;
    static int lastLevel = 12;
    static int current_level_coins = 0;

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

    public static void updateCurrentLevelCoins(int coins) {
        current_level_coins = coins;
    }

    public static int getCurrentLevelCoins() {
        return current_level_coins;
    }
}
