using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class Define
{
    public enum DateTime
    {
        all = 0,
        morning = 1,
        night = 2,
    }

    public enum Weather
    {
        nomal =0,
        sun = 1,
        rain = 2,
        snow = 3,
    }

    public enum Place
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3
    }
    public enum Charecter
    {
        Bule = 0,
        Black = 1,
        Pink = 2
    }

    public enum CharecterState
    {
        Idle = 0,
        Walk = 1,
        Sit = 2,
        Throw = 3,
        Catch = 4,
        StandUp = 5
    }

    public class FishData
    {
        public string rare;
        public string id;
        public string name;
        public float hp;
        public int place;
        public string weather;
        public int size;
        public string time;
    }
    public class EquipData
    {
        public string id;
        public string name;
        public int price;
        public List<string> probabilitytable;
        public int attack;
        public int castingspeed;
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
    public enum Scene
    {
        Unknown,
        SettingScene,
        GameScene,
    }
    public enum UIEvent
    {
        Click,
        Drag,
    }
}
