using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customization : MonoBehaviour
{
    public static Customization instance;
    public Sprite selected_skin; // default skin

    public void ChangeSkin(Sprite new_skin) {
        if (selected_skin != new_skin) {
            selected_skin = new_skin;
        }
    }
}
