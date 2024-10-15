using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static Dictionary<string, Sprite> tutorial_images = new Dictionary<string, Sprite>();

    public static bool level_needs_tutorial(string level_name) {
        return levels_with_tutorial.Contains(level_name);
    }

    public static void setupTutorialSprites(Sprite[] images) {
        Debug.Log("setting up tutorial images");
        for (int i = 0; i < images.Length; i++) {
            tutorial_images.Add(images[i].name, images[i]);
            Debug.Log(images[i].name);
        }
    }

    public static Sprite getTutorialSpriteForLevel(string level_name) {
        if (tutorial_images.Count == 0) {
            return null;
        }
        return tutorial_images[level_name];
    }
}
