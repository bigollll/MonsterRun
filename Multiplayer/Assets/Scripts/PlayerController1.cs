using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class PlayerController1 : MonoBehaviour
{

    float frente;
    float re;
    float girar;
    public static int pontos1 = 0;
   
    public Image coin;
   
    public Text txtPontos1;
    
    public GameObject efeitoCoin;
    public GameObject efeitoAgua;
    
    public GameObject escPnl;
    public GameObject winPnl;
    public GameObject losePnl;

    PhotonView photonView;



    // Start is called before the first frame update
    void Start()
    {
        frente = 10;
        girar = 60;
        

        txtPontos1.gameObject.SetActive(true);
        coin.gameObject.SetActive(true);

        photonView = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, (frente * Time.deltaTime));

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, (-frente * Time.deltaTime));

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, (-girar * Time.deltaTime), 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, (girar * Time.deltaTime), 0);

        }

        if(pontos1 == 10)
        {
            winPnl.gameObject.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escPnl.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            escPnl.gameObject.SetActive(false);
        }





    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            Instantiate(efeitoCoin);
            pontos1++;
            txtPontos1.text = pontos1.ToString();
            
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Agua"))
        {

            Instantiate(efeitoAgua);
            

        }
    }
}