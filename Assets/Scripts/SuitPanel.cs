using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitPanel : AbstractPanel
{
    // Set Panel
    public override void setPanel()
    {
        panel = GameObject.Find("SuitPanel");
    }
}
