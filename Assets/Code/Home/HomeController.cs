using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomeController : MonoBehaviour
{
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameDataController.initialize_progress();
    }
}
