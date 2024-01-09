using Gamekit3D;
using Gamekit3D.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;

namespace Gamekit3D
{
    
    public class Data_Listener : MonoBehaviour, IMessageReceiver
    {
        enum EntityID
        {
            PLAYER,
            CHOMPER,
            SPITTER,
            DEFAULT,
        }
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
            Damageable dmgScript = (Damageable)sender;
            var playerComponent = dmgScript.gameObject.GetComponent<PlayerController>();
            var chomperComponent = dmgScript.gameObject.GetComponent<ChomperBehavior>();
            var spitterComponent = dmgScript.gameObject.GetComponent<SpitterBehaviour>();

            Damageable.DamageMessage message = (Damageable.DamageMessage)msg;

            EntityID entity = EntityID.DEFAULT;

            if (playerComponent)
            {
                entity = EntityID.PLAYER;                
            }            
            if (chomperComponent)
            {
                entity = EntityID.CHOMPER;
            }
            if (spitterComponent)
            {
                entity = EntityID.SPITTER;
            }

            switch (type)
            {
                case MessageType.DAMAGED:
                    {                                                
                        DamagedUploader(dmgScript, message, entity);
                        break;
                    }
                case MessageType.DEAD:
                    {
                        DeathUploader(dmgScript, message, entity);
                        break;
                    }

                case MessageType.RESPAWN:
                    {
                        break;
                    }

            }
        }

        void DamagedUploader(Damageable dmgScript, Damageable.DamageMessage message, EntityID entity)
        {
            Vector3 pos = dmgScript.gameObject.transform.position;

            string damager = " ";
            var playerComponent = message.damager.GetComponent<PlayerController>();
            var chomperComponent = message.damager.gameObject.GetComponent<ChomperBehavior>();
            var spitterComponent = message.damager.gameObject.GetComponent<SpitterBehaviour>();
            if (playerComponent) damager = "Player";
            if (chomperComponent) damager = "Chomper";
            if (spitterComponent) damager = "Spitter";
            
            switch(entity)
            {
                case EntityID.PLAYER:
                    {
                        StartCoroutine(UploadDamagedPlayer(pos, message.amount, message.damageSource, damager));
                        
                    } break;
                case EntityID.CHOMPER: 
                    {
                        StartCoroutine(UploadDamagedChomper(pos, message.amount, message.damageSource, damager));
                    }
                    break;
                case EntityID.SPITTER:
                    {
                        StartCoroutine(UploadDamagedSpitter(pos, message.amount, message.damageSource, damager));
                    }
                    break;
            }
            
        }
       
        void DeathUploader(Damageable dmgScript, Damageable.DamageMessage message, EntityID entity)
        {
            Vector3 pos = dmgScript.gameObject.transform.position;

            string damager = " ";
            var playerComponent = message.damager.GetComponent<PlayerController>();
            var chomperComponent = message.damager.gameObject.GetComponent<ChomperBehavior>();
            var spitterComponent = message.damager.gameObject.GetComponent<SpitterBehaviour>();
            if (playerComponent) damager = "Player";
            if (chomperComponent) damager = "Chomper";
            if (spitterComponent) damager = "Spitter";

            switch (entity)
            {
                case EntityID.PLAYER:
                    {
                        StartCoroutine(UploadDeathPlayer(pos, message.amount, message.damageSource, damager));

                    }
                    break;
                case EntityID.CHOMPER:
                    {
                        StartCoroutine(UploadDeathChomper(pos, message.amount, message.damageSource, damager));
                    }
                    break;
                case EntityID.SPITTER:
                    {
                        StartCoroutine(UploadDeathSpitter(pos, message.amount, message.damageSource, damager));
                    }
                    break;
            }
        }
        IEnumerator UploadDamagedPlayer(Vector3 position, int amount, Vector3 damageSource, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Amount", amount.ToString());

            form.AddField("XDmgSource", ((int)damageSource.x).ToString());
            form.AddField("YDmgSource", ((int)damageSource.y).ToString());
            form.AddField("ZDmgSource", ((int)damageSource.z).ToString());

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
        IEnumerator UploadDamagedChomper(Vector3 position, int amount, Vector3 damageSource, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Amount", amount.ToString());

            form.AddField("XDmgSource", ((int)damageSource.x).ToString());
            form.AddField("YDmgSource", ((int)damageSource.y).ToString());
            form.AddField("ZDmgSource", ((int)damageSource.z).ToString());

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
        IEnumerator UploadDamagedSpitter(Vector3 position, int amount, Vector3 damageSource, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Amount", amount.ToString());

            form.AddField("XDmgSource", ((int)damageSource.x).ToString());
            form.AddField("YDmgSource", ((int)damageSource.y).ToString());
            form.AddField("ZDmgSource", ((int)damageSource.z).ToString());

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
        IEnumerator UploadDeathPlayer(Vector3 position, int amount, Vector3 damageSource, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Amount", amount.ToString());

            form.AddField("XDmgSource", ((int)damageSource.x).ToString());
            form.AddField("YDmgSource", ((int)damageSource.y).ToString());
            form.AddField("ZDmgSource", ((int)damageSource.z).ToString());

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
        IEnumerator UploadDeathChomper(Vector3 position, int amount, Vector3 damageSource, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Amount", amount.ToString());

            form.AddField("XDmgSource", ((int)damageSource.x).ToString());
            form.AddField("YDmgSource", ((int)damageSource.y).ToString());
            form.AddField("ZDmgSource", ((int)damageSource.z).ToString());

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
        IEnumerator UploadDeathSpitter(Vector3 position, int amount, Vector3 damageSource, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Amount", amount.ToString());

            form.AddField("XDmgSource", ((int)damageSource.x).ToString());
            form.AddField("YDmgSource", ((int)damageSource.y).ToString());
            form.AddField("ZDmgSource", ((int)damageSource.z).ToString());

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
