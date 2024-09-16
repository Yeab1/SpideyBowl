using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // the total number of coins player could have collected in a level
    public static int[] coins_per_level = {
        0,  // level 1-0
        0,  // level 1-1
        8,  // level 1-2
        14, // level 1-3
        11, // level 1-4
        11, // level 1-5
        10, // level 2-0
        13, // level 2-1
        10, // level 2-2
        17, // level 2-3
        0,  // level 3-0
        7,  // level 3-1
        7,  // level 3-2
        7,  // level 3-3
        7,  // level 3-4
        0,  // level 4-0
        6,  // level 4-1
        8,  // level 4-2
        10, // level 4-3
        10, // level 4-4
        0,  // level 5-0
        20, // level 5-1
        29, // level 5-2
        28, // level 5-3
        37, // level 5-4
        8,  // level 6-0
        10, // level 6-1
        15, // level 7-0
        17, // level 8-0
        36, // level 8-1
        28, // level 8-2
        26, // level 9-0 
        28, // level 9-1
    };
    // the number of stars player collected per level
    public static int[] collected_stars_per_level =
        GameDataController.get_collected_stars_per_level();

    static int max_unlocked_level;

    public static int get_total_expected_coins(int level) {
        return coins_per_level[level - 1];
    }
}
