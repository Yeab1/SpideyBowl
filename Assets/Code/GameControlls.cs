using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameControlls : MonoBehaviour
{
    public static GameControlls instance;

    // Outlets
    public static void jumpIfPossible() {
        BowlController.instance.jumpIfPossible();
    }
    // TODO: are these trash? can't we just use the ones in BowlContoller?
    public static void dashIfPossible() {
        BowlController.instance.dashIfPossible();
    }
}
