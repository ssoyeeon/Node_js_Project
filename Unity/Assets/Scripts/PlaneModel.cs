using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaneModel
{
    public int id;
    public string name;
    public int metal;
    public int crystal;
    public int deuterirum;

    public PlaneModel(int id, string name)
    {
        this.id = id;
        this.name = name;
        this.metal = 500;
        this.crystal = 300;
        this.deuterirum = 100;
    }
}
