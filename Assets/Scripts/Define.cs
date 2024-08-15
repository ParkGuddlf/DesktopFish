using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class Define
{
    public enum Weather
    {
        sun = 0,
        rain = 1,
        snew = 2,
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
}
