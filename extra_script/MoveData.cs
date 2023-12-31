using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData : MonoBehaviour
{
    private int movedX;
    private int movedY;
    private int movedZ;
    private string baseUrl = "citmalumnes.upc.es/~fernandofg2";
    private string phpurl = "/position.php";
    private string url;

    public MoveData (int x, int y, int z)
    {
      this.movedX = x;
      this.movedY = y;
      this.movedZ = z;
      
      string dataUrl = "?Xpos=" + movedX + "&Ypos=" + movedY + "&Zpos=" + movedZ; //PHP friendly string
      
      this.url = baseUrl + phpurl + dataUrl;
    }

    public string GetUrl()
    {
        return url;  
    }
}