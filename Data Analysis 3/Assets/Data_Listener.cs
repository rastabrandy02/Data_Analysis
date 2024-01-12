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
            Damageable[] dmgScripts = FindObjectsOfType<Damageable>();
            PlayerController playerController = FindObjectOfType<PlayerController>();

            for (int i = 0; i < dmgScripts.Length; i++)
            {
                dmgScripts[i].onDamageMessageReceivers.Add(this);
            }
            playerController.onMovementMessageReceivers.Add(this);
        }
        public void OnReceiveMessage(MessageType type, object sender, object msg)
        {           
            EntityID entity = EntityID.DEFAULT;
                       
            switch (type)
            {
                case MessageType.DAMAGED:
                    {
                        Damageable dmgScript = (Damageable)sender;
                        var playerComponent = dmgScript.gameObject.GetComponent<PlayerController>();
                        var chomperComponent = dmgScript.gameObject.GetComponent<ChomperBehavior>();
                        var spitterComponent = dmgScript.gameObject.GetComponent<SpitterBehaviour>();

                        Damageable.DamageMessage message = (Damageable.DamageMessage)msg;

                        if (playerComponent) entity = EntityID.PLAYER;
                        if (chomperComponent) entity = EntityID.CHOMPER;
                        if (spitterComponent) entity = EntityID.SPITTER;

                        DamagedUploader(dmgScript, message, entity);
                    }
                    break;
                    
                case MessageType.DEAD:
                    {
                        Damageable dmgScript = (Damageable)sender;
                        var playerComponent = dmgScript.gameObject.GetComponent<PlayerController>();
                        var chomperComponent = dmgScript.gameObject.GetComponent<ChomperBehavior>();
                        var spitterComponent = dmgScript.gameObject.GetComponent<SpitterBehaviour>();

                        Damageable.DamageMessage message = (Damageable.DamageMessage)msg;

                        if (playerComponent) entity = EntityID.PLAYER;

                        if (chomperComponent) entity = EntityID.CHOMPER;

                        if (spitterComponent) entity = EntityID.SPITTER;

                        DeathUploader(dmgScript, message, entity);
                    }
                    break;
                                          
                case MessageType.RESPAWN:
                    break;
                case MessageType.JUMP:
                    {
                        PlayerController controller = (PlayerController)sender;
                        JumpUploader(controller);
                    }
                    break;
                case MessageType.POSITION:
                    {
                        PlayerController controller = (PlayerController)sender;
                        PositionUploader(controller);
                    }
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
        void JumpUploader(PlayerController controller)
        {
            Vector3 position = controller.gameObject.transform.position;
            StartCoroutine(UploadJump(position));
        }
        void PositionUploader(PlayerController controller)
        {
            Vector3 position = controller.gameObject.transform.position;
            StartCoroutine(UploadPosition(position));
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

            UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~ogylandyy/damaged.php", form);
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

            UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~ogylandyy/death.php", form);
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
        IEnumerator UploadJump(Vector3 position)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~ogylandyy/jump.php", form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Jump form upload complete!");

                Debug.Log(www.downloadHandler.text);

                string phpText = www.downloadHandler.text;
            }
        }
        IEnumerator UploadPosition(Vector3 position)
        {
            WWWForm form = new WWWForm();

            form.AddField("XPos", ((int)position.x).ToString());
            form.AddField("YPos", ((int)position.y).ToString());
            form.AddField("ZPos", ((int)position.z).ToString());

            UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~ogylandyy/position.php", form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Jump form upload complete!");

                Debug.Log(www.downloadHandler.text);

                string phpText = www.downloadHandler.text;
            }
        }
    }
}
