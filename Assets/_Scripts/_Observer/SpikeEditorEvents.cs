using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpikeEditorEvents
{
    public abstract Color SpikeEditorColor();
}


public class YellowMat : SpikeEditorEvents
{
    public override Color SpikeEditorColor()
    {
        return Color.yellow;
    }
}

public class GreenMat : SpikeEditorEvents
{
    public override Color SpikeEditorColor()
    {
        return Color.green;
    }
}