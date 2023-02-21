using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class EnemyMarker:Sprite{

    int timeSpawned = Time.time;
    int lifeTime = 1000;
    int blinkTime = 200;
    int time = 0;
    EnemySpawnManager esm;
    GameData gameData;
    Player player;
    string enemyType;

    public EnemyMarker(string pEnemyType,GameData pGameData,EnemySpawnManager pEsm,Player pPlayer) : base("marker.png", false,false) {
        gameData= pGameData;
        esm = pEsm;
        player = pPlayer;

        SetOrigin(width / 2,height/2) ;
        enemyType = pEnemyType;
        if (enemyType=="tank")
            SetScaleXY(2,2) ;
        ChooseSpawnLocation();
    
    }


    void Update() {

        time += Time.deltaTime;
        Blink();

        if (Time.time - timeSpawned >= lifeTime) {
            SpawnEnemy();
            this.LateDestroy();
        }
    
    }

    void Blink(){

        if (time >= 2 * blinkTime){
            time -= 2 * blinkTime;
            alpha = 1.0f;
            blinkTime -= 30;
        }

        if (time >= blinkTime)
            alpha = 0.5f;
        
    }
    void ChooseSpawnLocation(){
        GameObject[] overlaps = GetCollisions(false, true);
        bool setCoords = false;
        while (overlaps.Length > 0||!setCoords) {
            setCoords = true;
            var rand = new Random();
            x = rand.Next(380, 1000);
            y = rand.Next(380, 1000);
            overlaps=GetCollisions(false, true);
        }

    }

    void SpawnEnemy() {
        switch (enemyType){
            case "standard":
                StandardEnemy enemy = new StandardEnemy(player);
                enemy.SetEnemySpawnManager(esm);
                enemy.SetGameData(gameData);
                enemy.SetXY(x, y);
                enemy.setSpawnXY(x, y);
                parent.AddChild(enemy);
                break;
            case "tank":
                TankEnemy tank = new TankEnemy(player);
                tank.SetEnemySpawnManager(esm);
                tank.SetGameData(gameData);
                tank.SetXY(x, y);
                tank.setSpawnXY(x, y);
                parent.AddChild(tank);
                break;
            case "shooter":
                Shooter shooter = new Shooter(player);
                shooter.SetEnemySpawnManager(esm);
                shooter.SetGameData(gameData);
                shooter.SetXY(x, y);
                shooter.setSpawnXY(x, y);
                parent.AddChild(shooter);
                break;
        }
    
    
    }

}
