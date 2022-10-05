using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer
{
    public abstract void OnNotify();
}

public class SpikeBall : Observer
{
    GameObject spikeObj;
    SpikeEditorEvents spikeEvent;

    public SpikeBall(GameObject spikeObj, SpikeEditorEvents spikeEvent)
    {
        this.spikeObj = spikeObj;
        this.spikeEvent = spikeEvent;
    }

    public override void OnNotify()
    {
        SpikeColor(spikeEvent.SpikeEditorColor());
    }

    void SpikeColor(Color mat)
    {
        if(spikeObj)
        {
            spikeObj.GetComponent<Renderer>().materials[0].color = mat;
        }
    }
}
