using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectButtons : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    // Start is called before the first frame update
    void Start()
    {
        hostBtn.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        clientBtn.onClick.AddListener(() => NetworkManager.Singleton.StartClient());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
