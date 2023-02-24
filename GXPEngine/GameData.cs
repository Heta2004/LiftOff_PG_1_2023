using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class GameData : GameObject {
    public int BIDENT = 1;
    public int BOW = 2;
    public int LIGHTNING = 3;
    public int SPEAR = 4;
    public int KNIFE = 5;


    public int BIDENTCOST = 15;
    public int BOWCOST = 15;
    public int BOLTCOST = 15;
    public int SPEARCOST = 15;


    public int MAXBIDENTBULLETS=200;
    public int MAXBOWBULLETS=30;
    public int MAXLIGHTNINGBULLETS=3;
    public int MAXSPEARBULLETS=10;
    public int MAXKNIFEBULLES = 2147483647;

    public int BIDENTDAMAGE;
    public int BOWDAMAGE ;
    public int LIGHTNINGDAMAGE ;
    public int SPEARDAMAGE;
    public int KNIFEDAMAGE;

    public int BIDENTSPEED;
    public int BOWSPEED;
    public int LIGHTNINGSPEED;
    public int SPEARSPEED;
    public int KNIFESPEED;

    public int DEFAULTBIDENTSPEED = 75;//75
    public int DEFAULTBOWSPEED = 500;//500
    public int DEFAULTLIGHTNINGSPEED = 1000;
    public int DEFAULTSPEARSPEED = 750;
    public int DEFAULTKNIFESPEED = 350;

    public int DEFAULTBIDENTDAMAGE = 10;
    public int DEFAULTBOWDAMAGE = 25;
    public int DEFAULTLIGHTNINGDAMAGE = 125;
    public int DEFAULTSPEARDAMAGE = 75;
    public int DEFAULTKNIFEDAMAGE = 10;

    public int DEFAULTMAXBIDENTBULLETS = 200;
    public int DEFAULTMAXBOWBULLETS = 30;
    public int DEFAULTMAXLIGHTNINGBULLETS = 3;
    public int DEFAULTMAXSPEARBULLETS = 10;

    public int MAXHPCOST = 10;
    public int MAXHPINCREASE = 10;
    public int DAMAGECOST = 20;

    public float SPEEDINCREASE = 0.05f;
    public float SPEEDINCREASEPICKUP = 0.15f;
    public int SPEEDINCREASECOST = 25;
    public int WEAPONSPEEDCOST = 20;

    public int HPONPICKUP = 100; 

    public int playerMaxHp;
    public int maxGunNumber = 5;
    //public int playerArmor;
    public float playerSpeedIncrease;

    public int[] guns;
    public int[] bullets;
    public List<int> gunArray;
    public List<int> GunBullets;

    public int gunNumber;
    public int stage=-1;

    public bool changedLevel = false;
    public long levelStartTime;

    public int dayLength = 1000;//45000
    public int nightLength = 1000;//45000

    public int DAY = 1;
    public int NIGHT = 2;

    public int gameState;

    public int score;
    public float scoreMultiplier;
    public float scoreMultiplierIncrease=0.25f;

    public float speedDecreaseMutliplier = (1f / 3);

    public float nightSpeedIncrease = 0.1f;
    public float nightHPIncrease = 0.25f;
    public float nightDamageIncrease = 0.25f;

    public int selectedWeapon;

    public string nextLevel="Level1.tmx";

    public int speedBoostDuration = 10000;
    public int money;

    public int contactDamageStandard = 10;
    public int contactDamageTank = 20;
    public int contactDamageMinotaur = 50;
    public int minotaurSlamDamage = 50;
    public int minotaurSpikeDamage = 25;

    public string lastLevel;
    public int spawnXLeft;
    public int spawnXRight;
    public int spawnYLeft;
    public int spawnYRight;

    public GameData(){
        Reset();
    }


    public void Reset() {
        money = 0;
        selectedWeapon= 0;
        score = 0;
        scoreMultiplier=1;
        gameState = DAY;
        playerMaxHp = 200;
        //playerArmor = 0;
        playerSpeedIncrease = 0;
        guns = new int[] { 5,1,3,4,2 };
        bullets=new int[] { MAXKNIFEBULLES,MAXBIDENTBULLETS,MAXLIGHTNINGBULLETS,MAXSPEARBULLETS,MAXBOWBULLETS };
        gunArray = new List<int>(guns);
        GunBullets= new List<int>(bullets);
        gunNumber = 0;
        stage = 3;
        score = 0;
        changedLevel = false;

        MAXBIDENTBULLETS = DEFAULTMAXBIDENTBULLETS;
        MAXBOWBULLETS = DEFAULTMAXBOWBULLETS;
        MAXLIGHTNINGBULLETS = DEFAULTMAXLIGHTNINGBULLETS;
        MAXSPEARBULLETS = DEFAULTMAXSPEARBULLETS;

        BIDENTDAMAGE=DEFAULTBIDENTDAMAGE;
        BOWDAMAGE=DEFAULTBOWDAMAGE;
        LIGHTNINGDAMAGE=DEFAULTLIGHTNINGDAMAGE;
        SPEARDAMAGE=DEFAULTSPEARDAMAGE;
        KNIFEDAMAGE=DEFAULTKNIFEDAMAGE;

        BIDENTSPEED = DEFAULTBIDENTSPEED;
        BOWSPEED = DEFAULTBOWSPEED;
        LIGHTNINGSPEED = DEFAULTLIGHTNINGSPEED;
        SPEARSPEED = DEFAULTSPEARSPEED;
        KNIFESPEED = DEFAULTKNIFESPEED;
}

}
