using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // the total number of coins player could have collected in a level
    public static int[] coins_per_level;
    // the number of stars player collected per level
    public static int[] collected_stars_per_level;

    static int max_level;

    public static void initialize_coins_per_level () {
        coins_per_level = new int[GameDataController.getLastLevel()];
        // level 0 does not exist. Leaving the 0th element free 
        // to make code intuitive
        coins_per_level[1] = 8;
        coins_per_level[2] = 13; 
        coins_per_level[3] = 8;
        coins_per_level[4] = 10; 
        coins_per_level[5] = 37; 
        coins_per_level[6] = 8;
        coins_per_level[7] = 15;
        coins_per_level[8] = 17; 
        coins_per_level[9] = 36; 
        coins_per_level[10] = 15; 
        coins_per_level[11] = 17;
        coins_per_level[12] = 17;
    }

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
