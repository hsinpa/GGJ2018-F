using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    public static HUDManager instance;
    public GameObject canvas;
    public GameObject HUD;



    private void Awake()
    {
        instance = this;
    }


    public void ShowHUD(string message, Vector3 position)
    {

        GameObject _hud = GameObject.Instantiate(HUD, transform);
            _hud.GetComponent<HUD>().init(message);
        _hud.transform.localPosition = WorldToUI(canvas.GetComponent<RectTransform>(), position);
    }

     public Vector2 WorldToUI(RectTransform r, Vector3 pos)
    {
        Vector2 screenPos = Camera.main.WorldToViewportPoint(pos); //世界物件在螢幕上的座標，螢幕左下角為(0,0)，右上角為(1,1)
        Vector2 viewPos = (screenPos - r.pivot) * 2; //世界物件在螢幕上轉換為UI的座標，UI的Pivot point預設是(0.5, 0.5)，這邊把座標原點置中，並讓一個單位從0.5改為1
        float width = r.rect.width / 2; //UI一半的寬，因為原點在中心
        float height = r.rect.height / 2; //UI一半的高
        return new Vector2(viewPos.x * width, viewPos.y * height); //回傳UI座標
    }




}
