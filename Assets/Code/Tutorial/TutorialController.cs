using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public static TutorialController instance;
    public Canvas tutorial_canvas;
    public TMP_Text tutorial_header;
    public TMP_Text tutorial_text;
    
    void Start() {
        instance = this;
    }

    void Awake() {
        tutorial_canvas.gameObject.SetActive(false);
    }

    public void display_tutorial() {
        tutorial_canvas.gameObject.SetActive(true);
    }

    public void hide_tutorial() {
        tutorial_canvas.gameObject.SetActive(false);
    }

    public void update_tutorial(string header, string text) {
        tutorial_header.text = header;
        tutorial_text.text = text;
    }
}
