using Gamekit3D;
using Gamekit3D.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Listener : MonoBehaviour, IMessageReceiver
{
    [SerializeField] Damageable playerScript;
    private void OnEnable()
    {
        //GameObject.FindAnyObjectByType<Damageable>();
        playerScript.onDamageMessageReceivers.Add(this);
    }
    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        //switch (type)
        //{

        //}
    }


}
