﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantsPanel : AbstractPanel
{
    // Set panel
    public override void setPanel()
    {
        panel = GameObject.Find("PantPanel");
        stopRightX = 2536f;
        stopLeftX = 3113f;
    }
	
}
