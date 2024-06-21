using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUtils : MonoBehaviour
{
    // TODO: is this irrelevant if we have the dictionary below?
    public static HashSet<string> levels_with_tutorial = new HashSet<string>(new string[] {
        "Level-1-0",
        "Level-1-1",
        "Level-1-3",
        "Level-3-0",
        "Level-4-0",
        "Level-5-0",
    });
    /*
        structure of the dictionary below:
            tutorial_details = {
                level_name: {
                    "tutorial_title": "",
                    "tutorial_body": ""
                }
            }
    */
    public static Dictionary<string, Dictionary<string, string>> tutorial_details = new Dictionary<string, Dictionary<string, string>> {
        { 
            "Level-1-0", new Dictionary<string, string> {
                {"tutorial_title", "level-1-0 title"},
                {"tutorial_body", "level-1-0 body"},
            }
        },
        { 
            "Level-1-1", new Dictionary<string, string> {
                {"tutorial_title", "level-1-1 title"},
                {"tutorial_body", "level-1-1 body"},
            }
        },
        { 
            "Level-1-3", new Dictionary<string, string> {
                {"tutorial_title", "level-1-3 title"},
                {"tutorial_body", "level-1-3 body"},
            }
        },
        { 
            "Level-3-0", new Dictionary<string, string> {
                {"tutorial_title", "level-3-0 title"},
                {"tutorial_body", "level-3-0 body"},
            }
        },
        { 
            "Level-4-0", new Dictionary<string, string> {
                {"tutorial_title", "level-4-0 title"},
                {"tutorial_body", "level-4-0 body"},
            }
        },
        { 
            "Level-5-0", new Dictionary<string, string> {
                {"tutorial_title", "level-5-0 title"},
                {"tutorial_body", "level-5-0 body"},
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
