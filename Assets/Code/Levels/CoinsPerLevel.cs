using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPerLevel : MonoBehaviour
{
    // the total number of coins player could have collected in a level
    public static int[] coins_per_level;
    // the number of stars player collected per level
    public static int[] collected_stars_per_level;

    public static void initialize_coins_per_level () {
        coins_per_level = new int[GameDataController.getLastLevel()];
        coins_per_level[0] = 8;
        coins_per_level[1] = 13;
        coins_per_level[2] = 8;
        coins_per_level[3] = 10;
        coins_per_level[4] = 26;
        coins_per_level[5] = 8;
        coins_per_level[6] = 15;
        coins_per_level[7] = 17;
        coins_per_level[8] = 18;
        coins_per_level[9] = 17;
    }

    public static int get_total_coins(int level) {
        return coins_per_level[level - 1];
    }

    public static void load_star_progress() {
        ProgressData progress = ProgressDataManager.LoadProgress();
        if (progress == null) {
            collected_stars_per_level = new int[GameDataController.getLastLevel()];
        } else {
            collected_stars_per_level = progress.get_collected_stars_per_level();
        }
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
        load_star_progress();

        // reset UI on level select window after clearing process
        LevelSelect.instance.destroy_all_level_prefabs();
        LevelSelect.instance.setup_level_select_grid();
    }
}
