using UnityEngine;
using System.Collections;

public class Pair {

    public float fX;
    public float fY;

    public Pair()
    {
        this.fX = 16.8f;
        this.fY = 14.4f;
    }

    public Pair(float x, float y)
    {
        this.fX = x;
        this.fY = y;
    }

    public static Pair operator ^ (Pair p1, Pair p2)
    {
        p1.fX = p2.fX;
        p1.fY = p2.fY;

        return p1;
    }
}
