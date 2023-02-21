using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class GameData : GameObject {
    public int AK = 1;
    public int MOSIN = 2;
    public int ROCKETLAUNCHER = 3;
    public int SNIPER = 4;
    public int KNIFE = 5;
    public int AKCOST = 65;
    public int MOSINCOST = 35;
    public int ROCKETLAUNCHERCOST = 150;
    public int MAXAKBULLETS = 90;
    public int MAXMOSINBULLETS = 30;
    public int MAXROCKETLAUNCHERBULLETS = 10;
    public int MAXSNIPERBULLETS = 10;
    public int MAXKNIFEBULLES = 2147483647;

    public int MAXHPCOST = 30;
    public int MAXHPINCREASE = 10;
    public int ARMORCOST = 40;

    public float SPEEDINCREASE = 0.1f;
    public float SPEEDINCREASEPICKUP = 0.25f;
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
    public int[] stageLength = new int[5] { 30000, 40000, 45000, 50000, 60000 };
    public long levelStartTime;

    public int dayLength = 45000;//45000
    public int nightLength = 45000;//45000

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

    public int minotaurSlamDamage = 50;
    public int minotaurSpikeDamage = 40;

    public int speedBoostDuration = 10000;
    public int money;

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
        guns = new int[] { 5,1,3,4 };
        bullets=new int[] { MAXKNIFEBULLES,MAXAKBULLETS,MAXROCKETLAUNCHERBULLETS,MAXSNIPERBULLETS };
        gunArray = new List<int>(guns);
        GunBullets= new List<int>(bullets);
        gunNumber = 0;
        stage = 3;
        score = 0;
        changedLevel = false;
    }

}
