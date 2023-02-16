using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GXPEngine;
using TiledMapParser;

internal class ShopManager : AnimationSprite{
    GameData gameData;
    List<SellButton> sellButtons = new List<SellButton> { };

    int[,] buttonPositions = new int[4, 2] {

                                                {32,384},
                                                {112,384},
                                                {192,384},
                                                {272,384}

                                            };

    int lastGunNumber = 0;
    bool addButtons = false;

    public ShopManager(string filename, int cols, int rows, TiledObject obj=null) : base(filename, cols, rows) {
    }

    void Update() {

        //after buttons are deleter upon selling a weapon add the new ones
        if (addButtons){
            SpawnButtons();
            addButtons=false;
        }
        //after a gun is sold delete all sell buttons
        if (lastGunNumber > gameData.gunArray.Count && lastGunNumber != 0){
            Console.WriteLine(gameData.gunArray.Count);
            Console.WriteLine(sellButtons.Count);
            while(sellButtons.Count > 0){
                sellButtons[0].LateDestroy();
                sellButtons.RemoveAt(0);
            }
            //tell the game to add the buttons on the next frame
            addButtons = true;
            lastGunNumber= gameData.gunArray.Count;
        }
        //after we buy a gun add a sell button for it
        if (lastGunNumber < gameData.gunArray.Count) {
            CreateButton(gameData.gunArray.Count - 1);
            lastGunNumber= gameData.gunArray.Count;
        }
    }

    public void SetGameData(GameData pGameData){
        gameData= pGameData;    
        lastGunNumber=gameData.gunArray.Count;
        SpawnButtons();
    }

    void SpawnButtons() {
        for (int i = 0; i < gameData.gunArray.Count; i++)
            CreateButton(i);
    }

    void CreateButton(int i){
        sellButtons.Add(new SellButton(i));
        sellButtons[i].SetXY(buttonPositions[i, 0], buttonPositions[i, 1]);
        sellButtons[i].SetGameData(gameData);
        parent.AddChild(sellButtons[i]);
    }
}
