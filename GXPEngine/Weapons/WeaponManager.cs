using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class WeaponManager:AnimationSprite{
    Player player;
    Camera camera;
    GameData gameData;

    const int BIDENT = 1;
    const int BOW = 2;
    const int LIGHTNING = 3;
    const int SPEAR = 4;
    const int KNIFE = 5;

    Gun gun;
    Gun secondGun;
    string weaponString;

    //int gunX = 10;
    //int gunY = 0;

    public WeaponManager(string filename, int cols, int rows, TiledObject obj = null) :base(filename,rows,cols){

    }

    void Update() {
        //if (selectedWeapon!=-1)
        //Console.WriteLine(gameData.GunBullets[selectedWeapon]);

        if (gameData.changedLevel&&gameData.gunArray.Count>0&&gameData.selectedWeapon>=0&&gameData.selectedWeapon<gameData.gunArray.Count) { 
            SpawnWeapon(gameData.gunArray[gameData.selectedWeapon]);
            gameData.changedLevel= false;
        }

        ThrowWeapon();
        SwitchWeapons();

        UpdateLocation();
    
    }

    void SpawnWeapon(int weaponType) {

        switch (weaponType) {
            case BIDENT:
                gun = new Bident(player, camera, gameData);
                player.AddChild(gun);
                gun.SetXY(20,0);
                weaponString = "Bident.png";
                break;
            case BOW:
                gun = new Bow(player, camera, gameData);
                player.AddChild(gun);
                weaponString = "bow.png";
                break;
            case LIGHTNING:
                gun = new Lightning(player, camera, gameData);
                player.AddChild(gun);
                weaponString = "Lighting_bolt.png";
                break;
            case SPEAR:
                gun = new Spear(player,camera,gameData);
                player.AddChild(gun);
                weaponString = "spear.png";
                break;
            case KNIFE:
                gun = new InfiniteKnife(player, camera, gameData);
                secondGun = new InfiniteKnife(player, camera, gameData);
                player.AddChild(gun);
                player.AddChild(secondGun);
                gun.SetScaleXY(1/player.scaleX,1/player.scaleY);
                secondGun.SetScaleXY(1 / player.scaleX, 1 / player.scaleY);
                gun.SetXY(-25,-25);
                secondGun.SetXY(25,-25);
                weaponString = "";
                break;
        }
        gun.SetSlot(gameData.selectedWeapon);
    
    }

    void UpdateLocation(){
    }

    public void setTarget(Player pPlayer)
    {
        player= pPlayer;
    }

    public void setCamera(Camera pCamera) { 
        camera= pCamera;    
    }

    public void SetGameData(GameData pGameData)
    {
        gameData = pGameData;
        gameData.selectedWeapon = 0;
    }

    void ThrowWeapon()
    {

        if (Input.GetKeyUp(Key.G) && gameData.gunArray.Count > 0&&!(gun is InfiniteKnife)){
            var result = camera.ScreenPointToGlobal(Input.mouseX, Input.mouseY);

            float angle = Tools.DirectionRelatedTools.CalculateAngle(gun.x+player.x, gun.y+player.y, result.x, result.y);
            ThrownWeapon bullet = new ThrownWeapon(player, weaponString);
            bullet.SetXY(gun.x + player.x, gun.y + player.y);
            bullet.rotation = angle;
            parent.AddChild(bullet);

            gun.Destroy();
            if (gameData.selectedWeapon != -1) {
                gameData.gunArray.RemoveAt(gameData.selectedWeapon);
                gameData.GunBullets.RemoveAt(gameData.selectedWeapon);
            }

            if (gameData.selectedWeapon > gameData.gunArray.Count-1){ 
                gameData.selectedWeapon--;
            }   
            
            if (gameData.gunArray.Count > 0)
                SpawnWeapon(gameData.gunArray[gameData.selectedWeapon]);
        }

    }

    void SwitchWeapons() {
        bool change = false;
        if (Input.GetKeyUp(Key.X)) {
            gameData.selectedWeapon++;
            change = true;
        }
        if (gameData.selectedWeapon > gameData.gunArray.Count - 1) { 
            gameData.selectedWeapon = 0;
        }
        if (change) {
            gun.Destroy();
            secondGun.Destroy();
            if (gameData.gunArray.Count>0)
                SpawnWeapon(gameData.gunArray[gameData.selectedWeapon]);
        }


    }
}
