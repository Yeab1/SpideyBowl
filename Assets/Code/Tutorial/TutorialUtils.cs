using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUtils : MonoBehaviour
{
    // TODO: is this irrelevant if we have the dictionary below?
    public static HashSet<string> levels_with_tutorial = new HashSet<string>(new string[] {
        "Level-1-0",
        "Level-1-1",
        "Level-3-0",
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
                {"tutorial_title", "title 1 test"},
                {"tutorial_body", "body 1 test"},
            }
        },
        { 
            "Level-1-1", new Dictionary<string, string> {
                {"tutorial_title", "title 2 test"},
                {"tutorial_body", "body 2 test"},
            }
        },
        { 
            "Level-3-0", new Dictionary<string, string> {
                {"tutorial_title", "title 3 test"},
                {"tutorial_body", "body 3 test"},
            }
        },
        { 
            "Level-5-0", new Dictionary<string, string> {
                {"tutorial_title", "title 4 test"},
                {"tutorial_body", "body 4 test"},
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
