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


    int[] maxEnemyPerStage = new int[5]{10,15,20,25,1};
    int[] delayPerStage = new int[5] { 1000, 800, 750, 700, 650 };

    Player player;
    
    public EnemySpawnManager(string filename, int cols, int rows, TiledObject obj = null):base(filename,cols,rows) {
        
    }

    void Update() {
        SpawnEnemy();
    }

    void SpawnEnemy() {
        int maxEnemies,delay;

        if (gameData.gameState == gameData.NIGHT) {
            if (gameData.stage > 4){
                maxEnemies = maxEnemyPerStage[4];
                delay = delayPerStage[4];
            }
            else {
                maxEnemies = maxEnemyPerStage[gameData.stage];
                delay = delayPerStage[gameData.stage];
            }
        }
        else {
            if (gameData.stage > 4){
                maxEnemies = maxEnemyPerStage[4]/2;
                delay = delayPerStage[4]*2;
            }
            else
            {
                maxEnemies = maxEnemyPerStage[gameData.stage]/2;
                delay = delayPerStage[gameData.stage]*2;
            }


        }
 
            maxEnemies=maxEnemyPerStage[4];
        if (Time.time > delay +lastSpawnTime && enemyNumber < maxEnemies &&player!=null) {
            lastSpawnTime= Time.time;
            enemyNumber++;
            var rand = new Random();
            int randomNumber = rand.Next(1,101);
            if (randomNumber <= spawnChances[Math.Min(gameData.stage,4),0]){
                EnemyMarker em = new EnemyMarker("standard",gameData,this,player);
                parent.AddChild(em);
            }
            else
                if (randomNumber <= spawnChances[Math.Min(gameData.stage, 4), 1]){
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

