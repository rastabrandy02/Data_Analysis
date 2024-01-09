using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpData : MonoBehaviour
{
    private int jumpedX;
    private int jumpedY;
    private int jumpedZ;
    private string baseUrl = "citmalumnes.upc.es/~fernandofg2";
    private string phpurl = "/jumped.php";
    private string url;

    public JumpData (int x, int y, int z)
    {
      this.jumpedX = x;
      this.jumpedY = y;
      this.jumpedZ = z;
      
      string dataUrl = "?Xpos=" + jumpedX + "&Ypos=" + jumpedY + "&Zpos=" + jumpedZ; //PHP friendly string
      
      this.url = baseUrl + phpurl + dataUrl;
    }

    public string GetUrl()
    {
        return url;  
    }
}