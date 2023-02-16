using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class ButtonBase:AnimationSprite{

    protected EasyDraw popUp = new EasyDraw(100,100,false);

    public ButtonBase(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows) {
        popUp.Fill(255,255,255);
        popUp.TextSize(8);
        popUp.TextAlign(CenterMode.Center,CenterMode.Min); 
    }

    protected virtual void Update() {

        parent.RemoveChild(popUp);
        if (HitTestPoint(Input.mouseX, Input.mouseY))
            OnMouseHover();
        if(HitTestPoint(Input.mouseX, Input.mouseY) && Input.GetMouseButtonUp(0))
            OnMouseClick();
    }

    protected virtual void OnMouseHover() {
            popUp.SetXY(Input.mouseX - 50, Input.mouseY + 50);
            parent.AddChild(popUp);
    }

    protected virtual void OnMouseClick() { 
    
    }

    //selling guns
    protected void SetText(int arg1){
        popUp.Text(String.Format("Sell For : {0}", arg1));
    }


    //buy upgrades
    protected void SetText(string message1,int arg1,int arg2){
        popUp.Text(String.Format("         Cost : {0}\n" + message1 + "\n              {1}", arg1, arg2));
    }

    //buy speed
    protected void SetText(string message1, int arg1, string message2){
        popUp.Text(String.Format("         Cost : {0}\n" + message1 + "\n              "+message2, arg1));
    }
    //buy weapons
    protected void SetText(int arg1,int arg2,int arg3){
        popUp.Text(String.Format("    Cost : {0}\n Damage : {1}\nFire Rate : {2}/s", arg1, arg2, arg3));
    }

}
