using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // the total number of coins player could have collected in a level
    public static int[] coins_per_level = {
        4,  // level 1-0
        8,  // level 1-1
        14, // level 1-2
        11, // level 1-3
        11, // level 1-4
        10, // level 2-0
        13, // level 2-1
        10, // level 2-2
        17, // level 2-3
        8,  // level 3-0
        7,  // level 3-1
        7,  // level 3-2
        7,  // level 3-3
        6,  // level 4-0
        8, // level 4-1
        10, // level 4-2
        10, // level 4-3
        20, // level 5-0
        29, // level 5-1
        28, // level 5-2
        37, // level 5-3
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
    public static int[] collected_stars_per_level;

    static int max_level;

    public static int get_total_coins(int level) {
        return coins_per_level[level];
    }

    public static int get_max_unlocked_level() {
        return max_level;
    }
    public static void set_max_unlocked_level(int level) {
        max_level = level;
    }

    public static ProgressData load_progress() {
        ProgressData progress = ProgressDataManager.LoadProgress();
        if (progress == null) {
            collected_stars_per_level = new int[GameDataController.getLastLevel()];
            max_level = 1;
        } else {
            // copy the data from file over to the new array because in case of 
            // new level creations, the array on file is smaller than the new one
            // which creates an index out of bounds exception
            // TODO: Release: No need to copy data over for release.
            // Just uncomment this line:
            // collected_stars_per_level = progress.get_collected_stars_per_level();
            // remove this start:
            int[] saved_collected_stars_per_level;
            saved_collected_stars_per_level = progress.get_collected_stars_per_level();
            collected_stars_per_level = new int[GameDataController.getLastLevel()];
            for (int i = 0; i < saved_collected_stars_per_level.Length; i++) {
                collected_stars_per_level[i] = saved_collected_stars_per_level[i];
            }
            // remove this end:
            max_level = progress.get_max_level();
        }
        return progress;
    }

    public static void set_collected_stars(int level, int collected_stars) {
        if (collected_stars_per_level[level - 1] < collected_stars) {
            collected_stars_per_level[level - 1] = collected_stars;
        }
    }

    public static int get_collected_stars(int level) {
        return collected_stars_per_level[level - 1];
    }

    public static int[] get_all_collected_stars() {
        return collected_stars_per_level;
    }

    // returns the total number of stars collected through all levels
    public static int get_total_stars() {
        int sum = 0;
        for (int i = 0; i < collected_stars_per_level.Length; i++) {
            sum += collected_stars_per_level[i];
        }
        return sum;
    }

    public static void clear_all_progress() {
        collected_stars_per_level = new int[GameDataController.getLastLevel()];
        ProgressData progress = new ProgressData(collected_stars_per_level, 1);
        ProgressDataManager.SaveProgress(progress);
        load_progress();

        // reset UI on level select window after clearing process
        LevelSelect.instance.destroy_all_level_prefabs();
        LevelSelect.instance.setup_level_select_grid();
    }
}
