using Gamekit3D;
using Gamekit3D.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace Gamekit3D
{
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

                        Damageable.DamageMessage message = (Damageable.DamageMessage)msg;

                        if (playerComponent)
                        {
                             chomperComponent = message.damager.gameObject.GetComponent<ChomperBehavior>();
                             spitterComponent = message.damager.gameObject.GetComponent<SpitterBehaviour>();
                            if(chomperComponent) DamagedPlayer(dmg, "Chomper");
                            if(spitterComponent) DamagedPlayer(dmg, "Spitter");


                            break;
                        }
                        if (chomperComponent)
                        {
                            playerComponent = message.damager.gameObject.GetComponent<PlayerController>();
                            spitterComponent = message.damager.gameObject.GetComponent<SpitterBehaviour>();
                            if (playerComponent) DamagedChomper(dmg, "Player");
                            if (spitterComponent) DamagedChomper(dmg, "Spitter");
                            
                            break;
                        }
                        if (spitterComponent)
                        {
                            playerComponent = message.damager.gameObject.GetComponent<PlayerController>();
                            chomperComponent = message.damager.gameObject.GetComponent<ChomperBehavior>();
                            if (playerComponent) DamagedChomper(dmg, "Player");
                            if (chomperComponent) DamagedChomper(dmg, "Chomper");
                            
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

        void DamagedPlayer(Damageable dmgScript, string damager)
        {
            Vector3 pos = dmgScript.gameObject.transform.position;
            
            StartCoroutine(UploadDamagedPlayer(pos, damager));
        }
        void DamagedChomper(Damageable dmgScript, string damager)
        {
            Vector3 pos = dmgScript.gameObject.transform.position;
            StartCoroutine(UploadDamagedChomper(pos, damager));
        }
        void DamagedSpitter(Damageable dmgScript,string damager)
        {
            Vector3 pos = dmgScript.gameObject.transform.position;
            StartCoroutine(UploadDamagedSpitter(pos, damager));
        }

        IEnumerator UploadDamagedPlayer(Vector3 position, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Damager", damager);

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
        IEnumerator UploadDamagedChomper(Vector3 position, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Damager", damager);

            UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Damaged_Player.php", form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Chomper form upload complete!");


                Debug.Log(www.downloadHandler.text);

                string phpText = www.downloadHandler.text;



            }
        }
        IEnumerator UploadDamagedSpitter(Vector3 position, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Damager", damager);

            UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Damaged_Player.php", form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Spitter form upload complete!");


                Debug.Log(www.downloadHandler.text);

                string phpText = www.downloadHandler.text;



            }
        }
    }
}
