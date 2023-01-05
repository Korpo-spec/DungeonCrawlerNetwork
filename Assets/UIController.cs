using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public string JonasPublicIP;
    public string CasperPublicIP;
    public Button Host;
    public Button Connect;
    public Button JonasIP;
    public Button CasperIP;
    public Toggle LocalHost;
    public TextField IPAddress;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Host = root.Q<Button>("host-button");
        Connect = root.Q<Button>("connect-button");
        JonasIP = root.Q<Button>("jonas-ipbutton");
        CasperIP = root.Q<Button>("casper-ipbutton");
        LocalHost = root.Q<Toggle>("localHost");
        IPAddress = root.Q<TextField>("IP-address");
        Host.clicked += HostButtonPressed;
        Connect.clicked += ConnectButtonPressed;
        JonasIP.clicked += JonasIPPressed;
        CasperIP.clicked += CasperIPPressed;
    }

    void HostButtonPressed()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = LocalHost.value ? "" : IPHelper.GetLocalIPAddress();
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Disable();
    }
    void ConnectButtonPressed()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = IPAddress.value;
        NetworkManager.Singleton.StartClient();
        Disable();
    }

    void JonasIPPressed()
    {
        IPAddress.value = JonasPublicIP;
    }
    void CasperIPPressed()
    {
        IPAddress.value = CasperPublicIP;
    }

    void Disable()
    {
        Host.clicked -= HostButtonPressed;
        Connect.clicked -= ConnectButtonPressed;
        JonasIP.clicked -= JonasIPPressed;
        CasperIP.clicked -= CasperIPPressed;
        
        Destroy(gameObject);
    }
}
