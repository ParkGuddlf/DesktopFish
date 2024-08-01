using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Define
{
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
        Catch = 4
    }

    public class FishData
    {
        public string id;
        public string name;
        public float hp;
        public int place;
        public string weather;
    }
}
