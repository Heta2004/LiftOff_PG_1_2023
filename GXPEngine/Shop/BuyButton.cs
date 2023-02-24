using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class BuyButton : ButtonBase{
    string type;
    GameData gameData;
    Sound sound = new Sound("shopBuySell.mp3");

    public BuyButton(string filename, int cols, int rows, TiledObject obj=null) : base(filename, cols, rows,obj){
        type = obj.GetStringProperty("type");
    }

    protected override void OnMouseClick(){

        switch (type){
            case "bow":
                BuyWeapon(gameData.BOW, gameData.BOWCOST,gameData.MAXBOWBULLETS);
                break;
            case "bident":
                BuyWeapon(gameData.BIDENT, gameData.BIDENTCOST,gameData.MAXBIDENTBULLETS);
                break;
            case "bolt":
                BuyWeapon(gameData.LIGHTNING, gameData.BOLTCOST,gameData.MAXLIGHTNINGBULLETS);
                break;
            case "spear":
                BuyWeapon(gameData.SPEAR, gameData.SPEARCOST, gameData.MAXSPEARBULLETS);
                break;
            case "Hp":
                if (gameData.money >= gameData.MAXHPCOST){
                    sound.Play(false, 0, 0.5f);
                    gameData.money -= gameData.MAXHPCOST;
                    gameData.playerMaxHp += gameData.MAXHPINCREASE;
                }
                break;
            case "Damage":
                if (gameData.money >= gameData.DAMAGECOST){
                    sound.Play(false, 0, 0.5f);
                    gameData.money -= gameData.DAMAGECOST;
                    gameData.BIDENTDAMAGE += (int)(0.05f * gameData.DEFAULTBIDENTDAMAGE);
                    gameData.BOWDAMAGE += (int)(0.05f * gameData.DEFAULTBOWDAMAGE);
                    gameData.KNIFEDAMAGE += (int)(0.05f * gameData.DEFAULTKNIFEDAMAGE);
                    gameData.LIGHTNINGDAMAGE += (int)(0.05f * gameData.DEFAULTLIGHTNINGDAMAGE);
                    gameData.SPEARDAMAGE += (int)(0.05f * gameData.DEFAULTSPEARDAMAGE);
                }
                break;
            case "Speed":
                if (gameData.money >= gameData.SPEEDINCREASECOST){
                    sound.Play(false, 0, 0.5f);
                    gameData.money -= gameData.SPEEDINCREASECOST;
                    gameData.playerSpeedIncrease += gameData.SPEEDINCREASE;
                }
                break;
            case "WeaponSpeed":
                gameData.money -= gameData.DAMAGECOST;
                gameData.BIDENTSPEED -= (int)(0.05f * gameData.DEFAULTBIDENTSPEED);
                gameData.BOWSPEED -= (int)(0.05f * gameData.DEFAULTBOWSPEED);
                gameData.KNIFESPEED -= (int)(0.05f * gameData.DEFAULTKNIFESPEED);
                gameData.LIGHTNINGSPEED -= (int)(0.05f * gameData.DEFAULTLIGHTNINGSPEED);
                gameData.SPEARSPEED -= (int)(0.05f * gameData.DEFAULTSPEARSPEED);
                break;
            default:
                break;

        }
    }

    public void SetGameData(GameData pGameData){
        gameData = pGameData;
        CheckStuffForDisplay();
    }

    void BuyWeapon(int weaponType, int price, int bullets)
    {
        if (gameData.money > price) { 
            sound.Play(false, 0, 0.5f);
            gameData.money -= price;
            switch (weaponType)
            {
                case 1:
                    gameData.MAXBIDENTBULLETS += 20;
                    break;
                case 2:
                    gameData.MAXBOWBULLETS += 5;
                    break;
                case 3:
                    gameData.MAXLIGHTNINGBULLETS += 1;
                    break;
                case 4:
                    gameData.MAXSPEARBULLETS += 2;
                    break;

            }
            for (int i = 0; i < gameData.gunArray.Count; i++)
                if (gameData.gunArray[i] == weaponType)
                {
                     
                    switch (weaponType) {
                        case 1:
                            gameData.GunBullets[i]=gameData.MAXBIDENTBULLETS;
                            break;
                        case 2:
                            gameData.GunBullets[i] = gameData.MAXBOWBULLETS;
                            break;
                        case 3:
                            gameData.GunBullets[i] = gameData.MAXLIGHTNINGBULLETS;
                            break;
                        case 4:
                            gameData.GunBullets[i] = gameData.MAXSPEARBULLETS;
                            break;

                    }

                }
        }

    }
    //add the text for when the mouse is hovering over shop items
    void CheckStuffForDisplay() {
        switch (type){
            case "bow":
                SetText(gameData.BOWCOST,5);
                break;
            case "bident":
                SetText(gameData.BIDENTCOST,20);
                break;
            case "bolt":
                SetText(gameData.BOLTCOST, 1);
                break;
            case "spear":
                SetText(gameData.SPEARCOST, 2);
                break;
            case "Hp":
                SetText("Increase max Hp by ", gameData.MAXHPCOST, gameData.MAXHPINCREASE);
                break;
            case "Damage":
                SetText("Increase damage by",gameData.DAMAGECOST,"5%");
                break;
            case "Speed":
                SetText("  Increase speed by",gameData.SPEEDINCREASECOST,"5%");
                break;
            case "WeaponSpeed":
                SetText("Increase weapon speed by", gameData.SPEEDINCREASECOST, "5%");
                break;
            default:
                break;
        }
    }
}
