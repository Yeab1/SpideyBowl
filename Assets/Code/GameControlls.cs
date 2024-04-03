using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameControlls : MonoBehaviour
{
    public static MenuController instance;

    // Outlets
    public void jumpIfPossible() {
        BowlController.instance.jumpIfPossible();
    }
    public void dashIfPossible() {
        BowlController.instance.dashIfPossible();
    }
}
