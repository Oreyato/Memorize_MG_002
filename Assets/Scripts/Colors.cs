using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]

public class Colors
{
    public string colorName;
    public int number;
    public Material material;

    public Colors(string colorName, int number, Material material)  
    {   
        this.colorName = colorName;  
        this.number = number;
        this.material = material;
    }    

    /*I didn't manage to access the material so I had to do it by hand, in the Inspector
    public Colors(string colorName, int number, Material material){
        ColorName = colorName;
        Number = number;
        Material = material;
    }

    public string ColorName {get; set;}
    public int Number {get; set;}
    public Material Material {get; set;}
    */
}