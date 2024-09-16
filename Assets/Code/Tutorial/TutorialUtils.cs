using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUtils : MonoBehaviour
{
    // TODO: is this irrelevant if we have the dictionary below?
    public static HashSet<string> levels_with_tutorial = new HashSet<string>(new string[] {
        "Level-1-0",
        "Level-1-1",
        "Level-1-4",
        "Level-3-0",
        "Level-4-0",
        "Level-5-0",
    });
    /*
        structure of the dictionary below:
            tutorial_details = {
                level_name: {
                    "tutorial_title": "...",
                    "tutorial_body": "..."
                }
            }
    */
    public static Dictionary<string, Dictionary<string, string>> tutorial_details = new Dictionary<string, Dictionary<string, string>> {
        { 
            "Level-1-0", new Dictionary<string, string> {
                {"tutorial_title", "Jump"},
                {"tutorial_body", "Tap the left side of your screen to jump."},
            }
        },
        { 
            "Level-1-1", new Dictionary<string, string> {
                {"tutorial_title", "Swing"},
                {"tutorial_body", "While in the air, tap and hold the right side of your screen to tap to the nearest light fixture."},
            }
        },
        { 
            "Level-1-4", new Dictionary<string, string> {
                {"tutorial_title", "Slippery tables"},
                {"tutorial_body", "Oily tables can give you a bit more speed."},
            }
        },
        { 
            "Level-3-0", new Dictionary<string, string> {
                {"tutorial_title", "Double Jump"},
                {"tutorial_body", "Collect double jump tokens to jump again while in the air."},
            }
        },
        { 
            "Level-4-0", new Dictionary<string, string> {
                {"tutorial_title", "Obstacles"},
                {"tutorial_body", "Avoid colliding with obstacles."},
            }
        },
        { 
            "Level-5-0", new Dictionary<string, string> {
                {"tutorial_title", "Spices"},
                {"tutorial_body", "Collecting spices will give you an instant boost in speed for a short period of time."},
            }
        }
    };

    public static bool level_needs_tutorial(string level_name) {
        return levels_with_tutorial.Contains(level_name);
    }

    public static string get_tutorial_title (string level_name) {
        return tutorial_details[level_name]["tutorial_title"];
    }

    public static string get_tutorial_body (string level_name) {
        return tutorial_details[level_name]["tutorial_body"];
    }
}
