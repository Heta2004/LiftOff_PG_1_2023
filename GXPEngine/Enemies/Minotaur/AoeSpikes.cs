using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class AoeSpikes : Sprite
{
    int startTime;
    int duration = 300;
    public AoeSpikes() : base("spikesBoss.png", true)
    {
        collider.isTrigger = true;
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime > duration)
        {
            Destroy();
        }
    }
}
