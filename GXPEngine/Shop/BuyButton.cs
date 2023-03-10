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
            case "Mosin":
                BuyWeapon(gameData.MOSIN, gameData.MOSINCOST);
                break;
            case "Ak":
                BuyWeapon(gameData.AK, gameData.AKCOST);
                break;
            case "RocketLauncher":
                BuyWeapon(gameData.ROCKETLAUNCHER, gameData.ROCKETLAUNCHERCOST);
                break;
            case "Hp":
                if (gameData.score >= gameData.MAXHPCOST){
                    sound.Play(false, 0, 0.5f);
                    gameData.score -= gameData.MAXHPCOST;
                    gameData.playerMaxHp += gameData.MAXHPINCREASE;
                }
                break;
            case "Armor":
                if (gameData.score >= gameData.ARMORCOST){
                    sound.Play(false, 0, 0.5f);
                    gameData.score -= gameData.ARMORCOST;
                    gameData.playerArmor++;
                }
                break;
            case "Speed":
                if (gameData.score >= gameData.SPEEDINCREASECOST){
                    sound.Play(false, 0, 0.5f);
                    gameData.score -= gameData.SPEEDINCREASECOST;
                    gameData.playerSpeedIncrease += gameData.SPEEDINCREASE;
                }
                break;
            default:
                break;

        }
    }

    public void SetGameData(GameData pGameData){
        gameData = pGameData;
        CheckStuffForDisplay();
    }

    void BuyWeapon(int weaponType,int price) {
        if (gameData.gunArray.Count < gameData.maxGunNumber && gameData.score >= price) {
            sound.Play(false, 0, 0.5f);
            gameData.gunArray.Add(weaponType);
            gameData.score -= price;
        }
    }
    //add the text for when the mouse is hovering over shop items
    void CheckStuffForDisplay() {
        switch (type){
            case "Mosin":
                SetText(gameData.MOSINCOST,25,2);
                break;
            case "Ak":
                SetText(gameData.AKCOST,15,5);
                break;
            case "RocketLauncher":
                SetText(gameData.ROCKETLAUNCHERCOST, 50, 1);
                break;
            case "Hp":
                SetText("Increase max Hp by ", gameData.MAXHPCOST, gameData.MAXHPINCREASE);
                break;
            case "Armor":
                SetText("Increase armor by",gameData.ARMORCOST,1);
                break;
            case "Speed":
                SetText("  Increase speed by",gameData.SPEEDINCREASECOST,"10%");
                break;
            default:
                break;
        }
    }
}
