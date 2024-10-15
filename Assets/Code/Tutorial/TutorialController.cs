using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public GameObject skipBtn;
    public Image tutorial_background;

    // Sprites
    public Sprite[] tutorial_sprites;

    string level_name = LevelsList.get_level_name_from_index(GameDataController.getLevel());

    void Start() {
        skipBtn.gameObject.SetActive(false);
        StartCoroutine(ActivateSkipButtonAfterDelay(3f));
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (TutorialUtils.getTutorialSpriteForLevel(level_name) == null)
            TutorialUtils.setupTutorialSprites(tutorial_sprites);
        Sprite tutorial_background_sprite = TutorialUtils.getTutorialSpriteForLevel(level_name);
        tutorial_background.sprite = tutorial_background_sprite;
    }

    IEnumerator ActivateSkipButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Activate the skip button
        skipBtn.SetActive(true);
    }

    public void skip_tutorial() {
        SceneManager.LoadScene(level_name);
    }
}
