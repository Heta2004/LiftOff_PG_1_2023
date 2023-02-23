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


    public int AKCOST = 65;
    public int BOWCOST = 35;
    public int ROCKETLAUNCHERCOST = 150;


    public int MAXBIDENTBULLETS=300;
    public int MAXBOWBULLETS=30;
    public int MAXLIGHTNINGBULLETS=10;
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

    public int DEFAULTBIDENTSPEED = 75;
    public int DEFAULTBOWSPEED = 500;//500
    public int DEFAULTLIGHTNINGSPEED = 1000;
    public int DEFAULTSPEARSPEED = 750;
    public int DEFAULTKNIFESPEED = 350;

    public int DEFAULTBIDENTDAMAGE = 10;
    public int DEFAULTBOWDAMAGE = 25;
    public int DEFAULTLIGHTNINGDAMAGE = 50;
    public int DEFAULTSPEARDAMAGE = 75;
    public int DEFAULTKNIFEDAMAGE = 10;

    public int DEFAULTMAXBIDENTBULLETS = 300;
    public int DEFAULTMAXBOWBULLETS = 30;
    public int DEFAULTMAXLIGHTNINGBULLETS = 10;
    public int DEFAULTMAXSPEARBULLETS = 10;

    public int MAXHPCOST = 30;
    public int MAXHPINCREASE = 10;
    public int ARMORCOST = 40;

    public float SPEEDINCREASE = 0.1f;
    public float SPEEDINCREASEPICKUP = 0.15f;
    public int SPEEDINCREASECOST = 45;

    public int HPONPICKUP = 10; 

    public int playerMaxHp;
    public int maxGunNumber = 4;
    public int playerArmor;
    public float playerSpeedIncrease;

    public int[] guns;
    public int[] bullets;
    public List<int> gunArray;
    public List<int> GunBullets;

    public int gunNumber;
    public int stage=-1;

    public bool changedLevel = false;
    public long levelStartTime;

    public int dayLength = 5000;//45000
    public int nightLength = 5000;//45000

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

    public int contactDamageStandard = 20;
    public int contactDamageTank = 30;
    public int contactDamageMinotaur = 50;
    public int minotaurSlamDamage = 50;
    public int minotaurSpikeDamage = 40;

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
        playerMaxHp = 500;
        playerArmor = 0;
        playerSpeedIncrease = 0;
        guns = new int[] { 5,1,3,2 };
        bullets=new int[] { MAXKNIFEBULLES,MAXBIDENTBULLETS,MAXLIGHTNINGBULLETS,MAXBOWBULLETS };
        gunArray = new List<int>(guns);
        GunBullets= new List<int>(bullets);
        gunNumber = 0;
        stage = 0;
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
