using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathData : MonoBehaviour
{
    private int deathX;
    private int deathY;
    private int deathZ;
    private string baseUrl = "citmalumnes.upc.es/~fernandofg2";
    private string phpurl = "/death.php";
    private string url;

    public DeathData (int x, int y, int z)
    {
      this.deathX = x;
      this.deathY = y;
      this.deathZ = z;
      
      string dataUrl = "?Xpos=" + deathX + "&Ypos=" + deathY + "&Zpos=" + deathZ; //PHP friendly string
      
      this.url = baseUrl + phpurl + dataUrl;
    }

    public string GetUrl()
    {
        return url;  
    }
}