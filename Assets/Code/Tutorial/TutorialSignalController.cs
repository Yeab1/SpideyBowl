using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSignalController : MonoBehaviour
{
    // state of the tutorial
    public int tutorial_ID;
    public string tutorial_header;
    public string tutorial_text;
    private bool is_paused;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            TutorialController.instance.update_tutorial(tutorial_header, tutorial_text);
            TutorialController.instance.display_tutorial();
            is_paused = true;
            Time.timeScale = 0.0f;
        }
    }

    void Update() {
        if (is_paused) {
            if (Input.touchCount > 0)
            {
                Time.timeScale = 1.0f;
                TutorialController.instance.hide_tutorial();
            }
        }
    }
}
