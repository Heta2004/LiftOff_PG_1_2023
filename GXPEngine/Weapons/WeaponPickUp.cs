using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class WeaponPickUp:Sprite{
    protected string weaponType;
    public WeaponPickUp(string filename,string pWeaponType) :base(filename) {
        collider.isTrigger = true;
        weaponType = pWeaponType;   
    }

    public string CheckWeaponType() { 
        return weaponType;
    }
}
