using Gamekit3D;
using Gamekit3D.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Data_Listener : MonoBehaviour, IMessageReceiver
{

    private void OnEnable()
    {
        Damageable[] dmgScripts = GameObject.FindObjectsOfType<Damageable>();

        for (int i = 0; i < dmgScripts.Length; i++)
        {
            dmgScripts[i].onDamageMessageReceivers.Add(this);
        }
    }
    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case MessageType.DAMAGED:
                {
                    Damageable dmg = (Damageable)sender;
                    var playerComponent = dmg.gameObject.GetComponent<PlayerController>();
                    var chomperComponent = dmg.gameObject.GetComponent<ChomperBehavior>();
                    var spitterComponent = dmg.gameObject.GetComponent<SpitterBehaviour>();
                    if (playerComponent)
                    {
                        DamagedPlayer(dmg);
                        break;
                    }
                    if (chomperComponent)
                    {
                        break;
                    }
                    if (spitterComponent)
                    {
                        break;
                    }
                    break;
                }
            case MessageType.DEAD:
                {
                    break;
                }

            case MessageType.RESPAWN:
                {
                    break;
                }

        }
    }

    void DamagedPlayer(Damageable dmgScript)
    {
        Vector3 pos = dmgScript.gameObject.transform.position;
        StartCoroutine(UploadDamagedPlayer(pos));
    }
    void DamagedChomper(Damageable dmgScript)
    {

    }
    void DamagedSpitter(Damageable dmgScript)
    {

    }

    IEnumerator UploadDamagedPlayer(Vector3 position)
    {
        WWWForm form = new WWWForm();

        form.AddField("XPos",((int)position.x).ToString());
        form.AddField("YPos",((int)position.y).ToString());
        form.AddField("ZPos",((int)position.z).ToString());


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Damaged_Player.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Player form upload complete!");


            Debug.Log(www.downloadHandler.text);

            string phpText = www.downloadHandler.text;



        }
    }
}