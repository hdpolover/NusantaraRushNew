﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStrategyScreen : MonoBehaviour
{
    public void End()
    {
        SceneManager.LoadScene(11);
    }
}
