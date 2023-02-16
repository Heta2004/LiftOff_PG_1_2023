using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class EnemySpawnManager : AnimationSprite {


    GameData gameData;

    int lastSpawnTime = 0;
    int enemyNumber = 0;
    int[,] spawnChances = new int[5, 3] {

                                        {100,0,0},
                                        {90,100,0},
                                        {70,90,100},
                                        {65,85,100},
                                        {60,80,100}
    
                                        };


    int[] maxEnemyPerStage = new int[5]{10,15,20,25,30};
    int[] delayPerStage = new int[5] { 1000, 800, 750, 700, 650 };

    Player player;
    
    public EnemySpawnManager(string filename, int cols, int rows, TiledObject obj = null):base(filename,cols,rows) {
        
    }

    void Update() {
        SpawnEnemy();
    }

    void SpawnEnemy() {
        if (gameData.stage > 4)
            gameData.stage--;
        if (Time.time > delayPerStage[gameData.stage] +lastSpawnTime && gameData.stage != -1 && enemyNumber < maxEnemyPerStage[gameData.stage] &&player!=null) {
            lastSpawnTime= Time.time;
            enemyNumber++;
            var rand = new Random();
            int randomNumber = rand.Next(1,101);
            if (randomNumber <= spawnChances[gameData.stage,0]){
                EnemyMarker em = new EnemyMarker("standard",gameData,this,player);
                parent.AddChild(em);
            }
            else
                if (randomNumber <= spawnChances[gameData.stage,1]){
                EnemyMarker em = new EnemyMarker("tank", gameData, this, player);
                parent.AddChild(em);
            }
                else {
                EnemyMarker em = new EnemyMarker("shooter", gameData, this, player);
                parent.AddChild(em);
            }

        }


    }

    public void SetTarget(Player pPlayer) { 
        player=pPlayer;
    
    }
    public void DecreaseEnemies() { 
        enemyNumber--;
    }

    public void SetGameData(GameData pGameData)
    {
        gameData = pGameData;
    }

}

