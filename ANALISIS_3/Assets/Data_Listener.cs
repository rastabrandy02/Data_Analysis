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

            if (playerComponent) entity = EntityID.PLAYER;                
                        
            if (chomperComponent) entity = EntityID.CHOMPER;
            
            if (spitterComponent) entity = EntityID.SPITTER;
            

            switch (type)
            {
                case MessageType.DAMAGED: DamagedUploader(dmgScript, message, entity);
                    break;
                    
                case MessageType.DEAD: DeathUploader(dmgScript, message, entity);
                    break;
                                          
                case MessageType.RESPAWN:
                    break;
                    

            }
        }

        void DamagedUploader(Damageable dmgScript, Damageable.DamageMessage message, EntityID entity)
        {
            Vector3 pos = dmgScript.gameObject.transform.position;

            string damager = " ";
            var playerComponent = message.damager.gameObject.GetComponentInParent<PlayerController>();
            var chomperComponent = message.damager.gameObject.GetComponentInParent<ChomperBehavior>();
            var spitterComponent = message.damager.gameObject.GetComponent<Spit>();
            if (playerComponent) damager = "Player";
            if (chomperComponent) damager = "Chomper";
            if (spitterComponent) damager = "Spitter";
            
            string receiver = " ";
            switch(entity)
            {
                case EntityID.PLAYER: receiver = "Player";
                    break;
                case EntityID.CHOMPER:
                    receiver = "Chomper";
                    break;
                case EntityID.SPITTER:
                    receiver = "Spitter";
                    break;

            }
            StartCoroutine(UploadDamaged(pos, message.amount, message.damageSource, receiver, damager));
        }
       
        void DeathUploader(Damageable dmgScript, Damageable.DamageMessage message, EntityID entity)
        {
            Vector3 pos = dmgScript.gameObject.transform.position;

            string damager = " ";
            var playerComponent = message.damager.gameObject.GetComponentInParent<PlayerController>();
            var chomperComponent = message.damager.gameObject.GetComponentInParent<ChomperBehavior>();
            var spitterComponent = message.damager.gameObject.GetComponent<Spit>();

            if (playerComponent) damager = "Player";
            if (chomperComponent) damager = "Chomper";
            if (spitterComponent) damager = "Spitter";

            string receiver = " ";
            switch (entity)
            {
                case EntityID.PLAYER:
                    receiver = "Player";
                    break;
                case EntityID.CHOMPER:
                    receiver = "Chomper";
                    break;
                case EntityID.SPITTER:
                    receiver = "Spitter";
                    break;

            }
            StartCoroutine(UploadDeath(pos, message.amount, message.damageSource, receiver, damager));
        }
        IEnumerator UploadDamaged(Vector3 position, int amount, Vector3 damageSource,string receiver, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            form.AddField("Amount", amount.ToString());

            form.AddField("XDmgSource", ((int)damageSource.x).ToString());
            form.AddField("YDmgSource", ((int)damageSource.y).ToString());
            form.AddField("ZDmgSource", ((int)damageSource.z).ToString());

            form.AddField("Receiver", receiver);
            form.AddField("Damager", damager);

            UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/damaged.php", form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Damaged form upload complete!");

                Debug.Log(www.downloadHandler.text);

                string phpText = www.downloadHandler.text;
            }
        }
        
        IEnumerator UploadDeath(Vector3 position, int amount, Vector3 damageSource, string receiver, string damager)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());
            
            form.AddField("Amount", amount.ToString());

            form.AddField("XDmgSource", ((int)damageSource.x).ToString());
            form.AddField("YDmgSource", ((int)damageSource.y).ToString());
            form.AddField("ZDmgSource", ((int)damageSource.z).ToString());

            form.AddField("Receiver", receiver);
            form.AddField("Damager", damager);

            UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/death.php", form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Death form upload complete!");

                Debug.Log(www.downloadHandler.text);

                string phpText = www.downloadHandler.text;
            }
        }
        
    }
}
