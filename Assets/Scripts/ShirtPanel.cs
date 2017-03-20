using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtPanel : AbstractPanel
{
    // Set panel
    public override void setPanel()
    {
        panel = GameObject.Find("ShirtPanel");
        stopRightX = -330f;
        stopLeftX = 2566f;
    }
}
