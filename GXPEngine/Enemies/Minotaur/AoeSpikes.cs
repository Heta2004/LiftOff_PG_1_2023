using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class AoeSpikes : Sprite
{
    int startTime;
    int duration = 500;
    public AoeSpikes() : base("spikesBoss.png")
    {
        //collider.isTrigger = true;
        startTime = Time.time;
        alpha = 0.2f;//0.4f
    }

    void Update()
    {

        if (Time.time - startTime > duration)
        {
            Destroy();
        }
    }
}
