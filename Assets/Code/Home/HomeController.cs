using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomeController : MonoBehaviour
{
    public TMP_Text star_count;

    void Awake()
    {
        GameDataController.initialize_progress();
    }

    // Start is called before the first frame update
    void Start()
    {
        star_count.text = "" + GameDataController.get_total_stars();
    }
}
