using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class TeleporterManager:GameObject{
    Teleporter[] teleporters;
    int previousDeactivation = -1;



    public TeleporterManager() : base() { 
            
            
            
    }

    public void ChooseTarget(Teleporter currentTeleporter) {
        var rand = new Random();
        int randomNumber=currentTeleporter.GetNumber();
        previousDeactivation= randomNumber;
        while (randomNumber==currentTeleporter.GetNumber()) {
            randomNumber = rand.Next(0, teleporters.Length);
        }
        currentTeleporter.Teleport(teleporters[randomNumber].x, teleporters[randomNumber].y);
        teleporters[randomNumber].ChangeActivation(); 
    }

    public void SetTeleporterList(Teleporter[] pTeleporters) {
        teleporters = pTeleporters;
    }

}
