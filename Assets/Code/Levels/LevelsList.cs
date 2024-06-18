using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used as a reference list of the available levels.
// Levels are named under the convention of "Level-GROUP-LEVEL" in order
// to make it easier to add levels without having to rename too many scenes
public class LevelsList : MonoBehaviour
{
    public static string[] Level_string_list = {
        "Level-1-0",
        "Level-1-1",
        "Level-2-0",
        "Level-3-0",
        "Level-4-0",
        "Level-5-0",
        "Level-6-0",
        "Level-7-0",
        "Level-8-0",
        "Level-8-1",
        "Level-8-2",
        "Level-9-0", 
        "Level-9-1",
    };

    public static int get_number_of_levels() {
        return Level_string_list.Length; // to avoid the level 0 padding
    }

    public static string get_level_name_from_index(int index) {
        return Level_string_list[index - 1];
    }

    public static int get_index_from_level_name(string level) {
        for (int i = 0; i < Level_string_list.Length; i++) {
            if (Level_string_list[i] == level) {
                return i;
            }
        }
        Debug.Log("Could not find "+ level + " in Level_string_list");
        return -1;
    }
}
