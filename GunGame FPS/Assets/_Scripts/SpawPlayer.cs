using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
    }


    void Update()
    {
        
    }

    // -- Back At It Again
}