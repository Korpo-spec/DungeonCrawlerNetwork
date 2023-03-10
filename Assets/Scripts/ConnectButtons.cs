using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ConnectButtons : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button hostLocalBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private Button clientJonasBtn;
    [SerializeField] private Button clientCasperBtn;
    // Start is called before the first frame update
    void Start()
    {
        
        if (!NetworkManager.Singleton)
        {
            foreach (var obj in Resources.FindObjectsOfTypeAll<NetworkManager>())
            {
                obj.gameObject.SetActive(true);
            }
        }
        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        hostLocalBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = "";
            NetworkManager.Singleton.StartHost();
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
        clientJonasBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = "193.11.162.235";
            NetworkManager.Singleton.StartClient();
        });
        clientCasperBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = "193.11.160.199";
            NetworkManager.Singleton.StartClient();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!NetworkManager.Singleton) return;
        if (NetworkManager.Singleton.IsConnectedClient || NetworkManager.Singleton.IsHost)
        {
            DisableButtons();
        }
        
    }

    void DisableButtons()
    {
        gameObject.SetActive(false);
    }
}
