using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text body;
    public GameObject skipBtn;

    string level_name = LevelsList.get_level_name_from_index(GameDataController.getLevel());

    void Start() {
        skipBtn.gameObject.SetActive(false);
        StartCoroutine(ActivateSkipButtonAfterDelay(3f));
    }
    // Start is called before the first frame update
    void Awake()
    {
        string tutorial_title = TutorialUtils.get_tutorial_title(level_name);
        string tutorial_body = TutorialUtils.get_tutorial_body(level_name);
        
        title.text = tutorial_title;
        body.text = tutorial_body;
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
