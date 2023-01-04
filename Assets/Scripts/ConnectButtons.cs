using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

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
        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            DisableButtons();
        });
        hostLocalBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = "";
            NetworkManager.Singleton.StartHost();
            DisableButtons();
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            DisableButtons();
        });
        clientJonasBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = "193.11.162.235";
            NetworkManager.Singleton.StartClient();
            DisableButtons();
        });
        clientCasperBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = "193.11.160.199";
            NetworkManager.Singleton.StartClient();
            DisableButtons();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisableButtons()
    {
        this.gameObject.SetActive(false);
    }
}
