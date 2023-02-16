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

    const int AK = 1;
    const int MOSIN = 2;
    const int ROCKETLAUNCHER = 3;
    const int SNIPER = 4;

    Gun gun;
    string weaponString;
    int selectedWeapon = 0;

    //int gunX = 10;
    //int gunY = 0;

    public WeaponManager(string filename, int cols, int rows, TiledObject obj = null) :base(filename,rows,cols){

    }

    void Update() {
        //if (selectedWeapon!=-1)
        //Console.WriteLine(gameData.GunBullets[selectedWeapon]);

        if (gameData.changedLevel&&gameData.gunArray.Count>0&&selectedWeapon>=0&&selectedWeapon<gameData.gunArray.Count) { 
            SpawnWeapon(gameData.gunArray[selectedWeapon]);
            gameData.changedLevel= false;
        }

        ThrowWeapon();
        SwitchWeapons();

        UpdateLocation();
    
    }

    void SpawnWeapon(int weaponType) {

        switch (weaponType) {
            case AK:
                gun = new AK(player, camera, gameData);
                player.AddChild(gun);
                gun.SetXY(20,0);
                weaponString = "ak.png";
                break;
            case MOSIN:
                gun = new Mosin(player, camera, gameData);
                player.AddChild(gun);
                weaponString = "Mosin_Old.png";
                break;
            case ROCKETLAUNCHER:
                gun = new RocketLauncher(player, camera, gameData);
                player.AddChild(gun);
                weaponString = "RocketLauncher.png";
                break;
            case SNIPER:
                gun = new Sniper(player,camera,gameData);
                player.AddChild(gun);
                weaponString = "ak.png";
                break;
        }
        gun.SetSlot(selectedWeapon);
    
    }

    void UpdateLocation(){
        //if (gun != null)
        //    gun.SetXY(player.x + gunX, player.y + gunY);

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
    }

    void ThrowWeapon()
    {

        if (Input.GetKeyUp(Key.G) && gameData.gunArray.Count > 0)
        {
            Console.WriteLine(gameData.gunArray.Count+" "+selectedWeapon);
            var result = camera.ScreenPointToGlobal(Input.mouseX, Input.mouseY);
            Console.WriteLine(gun.x);

            float angle = Tools.DirectionRelatedTools.CalculateAngle(gun.x+player.x, gun.y+player.y, result.x, result.y);
            Console.WriteLine(weaponString);
            ThrownWeapon bullet = new ThrownWeapon(player, weaponString);
            bullet.SetXY(gun.x + player.x, gun.y + player.y);
            bullet.rotation = angle;
            parent.AddChild(bullet);

            gun.Destroy();
            if (selectedWeapon != -1) {
                gameData.gunArray.RemoveAt(selectedWeapon);
                gameData.GunBullets.RemoveAt(selectedWeapon);
            }

            if (selectedWeapon>gameData.gunArray.Count-1){ 
                selectedWeapon--; 
            }   
            
            if (gameData.gunArray.Count > 0)
                SpawnWeapon(gameData.gunArray[selectedWeapon]);
        }

    }

    void SwitchWeapons() {
        bool change = false;
        if (Input.GetKeyUp(Key.X)) {
            selectedWeapon++;
            change = true;
        }
        if (selectedWeapon > gameData.gunArray.Count - 1) { 
            selectedWeapon = 0; 
        }
        if (change) {
            gun.Destroy();
            if (gameData.gunArray.Count>0)
                SpawnWeapon(gameData.gunArray[selectedWeapon]);
        }


    }
}
