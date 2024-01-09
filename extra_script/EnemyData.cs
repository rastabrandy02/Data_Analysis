using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    private int enemyX;
    private int enemyY;
    private int enemyZ;
    private string baseUrl = "citmalumnes.upc.es/~fernandofg2";
    private string phpurl = "/enemy.php";
    private string url;

    public EnemyData (int x, int y, int z)
    {
      this.enemyX = x;
      this.enemyY = y;
      this.enemyZ = z;
      
      string dataUrl = "?Xpos=" + enemyX + "&Ypos=" + enemyY + "&Zpos=" + enemyZ; //PHP friendly string
      
      this.url = baseUrl + phpurl + dataUrl;
    }

    public string GetUrl()
    {
        return url;  
    }
}