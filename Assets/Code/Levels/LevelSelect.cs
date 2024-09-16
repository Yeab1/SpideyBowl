using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect instance;
    public Transform level_select_field;
    public GameObject level_prefab;
    public GameObject locked_level_prefab;

    public float column_spacing = 1f;
    public float row_spacing = 1f;
    public float vertical_offset = 1f;
    public float horizontal_offset = 1f;
    int levels_per_row = 5;
    int levels_per_section = 15;
    public int current_section = 0; // breakup levels list into sections
    public GameObject next_section_btn;
    public GameObject previous_section_btn;

    void Start () {
        instance = this;
        current_section = (int) Mathf.Floor((GameDataController.getLevel() - 1) / levels_per_section);
        setup_level_select_grid();
    }

    public void setup_level_select_grid () {
        // hide the next and previous section buttons if buttons cannot be used
        // i.e if they would lead to going out of bounds.
        if (current_section >= Mathf.Floor(GameDataController.getLastLevel() / levels_per_section)) 
            next_section_btn.SetActive(false);
        else next_section_btn.SetActive(true);
        
        if (current_section <= 0) 
            previous_section_btn.SetActive(false);
        else previous_section_btn.SetActive(true);

        for (int row=0; row<=2; row++) {
            for (int col=1; col < levels_per_row + 1; col++) {
                int level = (current_section * levels_per_section) + (row * levels_per_row + col);
                if (level > GameDataController.getLastLevel()) {
                    break;
                }
                GameObject created_prefab;
                if (is_locked(level)) {
                    created_prefab = Instantiate(locked_level_prefab, level_select_field);
                    created_prefab.transform.position += new Vector3(
                                                        (col * column_spacing) + horizontal_offset, 
                                                        (-row * row_spacing) + vertical_offset, 
                                                        0);
                    continue;
                }
                created_prefab = Instantiate(level_prefab, level_select_field);
                created_prefab.transform.position += new Vector3(
                                                    (col * column_spacing) + horizontal_offset, 
                                                    (-row * row_spacing) + vertical_offset, 
                                                    0);
                
                // update the text on the button to reflect the level number
                GameObject text_field = created_prefab.transform.Find("Level Number").gameObject;
                text_field.GetComponent<TMP_Text>().text = "" + (level);
                
                // set the onclick function of the button with the level number
                Button b = created_prefab.GetComponent<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(() => HomeMenuController.StartLevel(level));

                // disable unearned stars for level
                int stars_earned = GameDataController.get_collected_stars(level);
                switch (stars_earned) {
                    case 0:
                        created_prefab.transform.Find("Star1").gameObject.SetActive(false);
                        created_prefab.transform.Find("Star2").gameObject.SetActive(false);
                        created_prefab.transform.Find("Star3").gameObject.SetActive(false);
                        break;
                    case 1: 
                        created_prefab.transform.Find("Star2").gameObject.SetActive(false);
                        created_prefab.transform.Find("Star3").gameObject.SetActive(false);
                        break;
                    case 2:
                        created_prefab.transform.Find("Star3").gameObject.SetActive(false);
                        break;
                    default:
                        break;
                    
                }
            }
        }
    }

    bool is_locked(int level) {
        ProgressData progress = GameDataController.get_progress();
        // TODO: Debug: revert this change
        if (!BowlController.is_debug_mode) {
            return (progress == null && level != 1 || progress.get_max_unlocked_level() < level);
        }
        return false;
    }

    public void destroy_all_level_prefabs() {
        for(int i = level_select_field.childCount - 1; i >= 0; i--)
        {
            Destroy(level_select_field.GetChild(i).gameObject);
        }
    }
}
