using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class SellButton : ButtonBase{
    int number;
    GameData gameData;
    Sprite gunSprite;
    Sound sound = new Sound("shopBuySell.mp3");

    public SellButton(int pNumber):base("ShopButton.png",1,1){
        number = pNumber;
    }

    protected override void OnMouseHover(){
        popUp.SetXY(Input.mouseX - 50, Input.mouseY - 50);
        parent.AddChild(popUp);
    }

    protected override void OnMouseClick(){
        switch (gameData.gunArray[number]){
            case 1:
                SellWeapon(20);
                break;
            case 2:
                SellWeapon(10);
                break;
            case 3:
                SellWeapon(45);
                break;
        }
    }

    public void SetGameData(GameData pGameData){
        gameData = pGameData;
        SpawnSellButton();
    }

    void SellWeapon(int price){
        sound.Play(false,0,0.5f);
        gameData.gunArray.RemoveAt(number);
        gameData.money += price;
        gunSprite.LateDestroy();
    }

    void SpawnSellButton() {
        if (gameData.gunArray.Count > number){
            switch (gameData.gunArray[number]){
                case 1:
                    SetText(20);
                    gunSprite = new Sprite("Ak.png");
                    gunSprite.SetOrigin(0, 0);
                    gunSprite.SetXY(8,10);
                    AddChild(gunSprite);
                    break;
                case 2:
                    SetText(10);
                    gunSprite = new Sprite("Mosin_Old.png");
                    gunSprite.SetOrigin(0, 0);
                    gunSprite.SetXY(9,8);
                    AddChild(gunSprite);
                    break;
                case 3:
                    SetText(45);
                    gunSprite = new Sprite("rlshop.png");
                    gunSprite.SetOrigin(0, 0);
                    gunSprite.SetXY(8,8);
                    AddChild(gunSprite);
                    break;
                default:
                    break;
            }
        }
    }

    protected override void OnDestroy(){
        if (gunSprite!= null)
            gunSprite.Destroy();
        if (popUp!=null)
            popUp.Destroy();
    }
}
