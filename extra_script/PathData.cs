using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathData : MonoBehaviour
{
    private int pathX;
    private int pathY;
    private int pathZ;
    private string baseUrl = "citmalumnes.upc.es/~fernandofg2";
    private string phpurl = "/path.php";
    private string url;

    public PathData (int x, int y, int z)
    {
      this.pathX = x;
      this.pathY = y;
      this.pathZ = z;
      
      string dataUrl = "?Xpos=" + pathX + "&Ypos=" + pathY + "&Zpos=" + pathZ; //PHP friendly string
      
      this.url = baseUrl + phpurl + dataUrl;
    }

    public string GetUrl()
    {
        return url;  
    }
}