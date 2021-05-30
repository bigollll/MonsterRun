using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class Load : MonoBehaviour
{
    public GameObject creditosPnl;
    public GameObject escPnl;

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        PhotonNetwork.Disconnect();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void Creditos()
    {
        creditosPnl.gameObject.SetActive(true);
    }

    public void VoltarDosCreditos()
    {
        creditosPnl.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }



}