using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledData : MonoBehaviour
{
    private int enemykilledX;
    private int enemykilledY;
    private int enemykilledZ;
    private string baseUrl = "citmalumnes.upc.es/~fernandofg2";
    private string phpurl = "/enemieskilled.php";
    private string url;

    public EnemiesKilledData (int x, int y, int z)
    {
      this.enemykilledX = x;
      this.enemykilledY = y;
      this.enemykilledZ = z;
      
      string dataUrl = "?Xpos=" + enemykilledX + "&Ypos=" + enemykilledY + "&Zpos=" + enemykilledZ; //PHP friendly string
      
      this.url = baseUrl + phpurl + dataUrl;
    }

    public string GetUrl()
    {
        return url;  
    }
}