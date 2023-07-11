using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TopKontrol : MonoBehaviour
{
    public Button btn;
    public TextMeshProUGUI Zaman,KalanCan,Durum;
    private Rigidbody rg;
    public float hiz=1.5f;
    float zamanSayaci=100;
    int canSayaci=10;
    bool oyunDevam=true;
    bool oyunTamam=false;
    void Start()
    {
        rg=GetComponent<Rigidbody>();
    }

    
    void Update()
    {

        if(oyunDevam && !oyunTamam)
        {
            zamanSayaci-=Time.deltaTime;
            Zaman.text=(int)zamanSayaci+"";
        }
        else if(!oyunTamam)
        {
            Durum.text="Oyun Tamamlanamadı!";
            btn.gameObject.SetActive(true);
        }

        if(zamanSayaci<0)
        {
            oyunDevam=false;
        }
    }
    void FixedUpdate()
    {
        if(oyunDevam && !oyunTamam)
        {
            float yatay=Input.GetAxis("Horizontal");
            float dikey=Input.GetAxis("Vertical");
            Vector3 kuvvet=new Vector3(yatay,0,dikey);
            rg.AddForce(kuvvet*hiz);
        }
        else
        {
            rg.velocity=Vector3.zero;//topu durdurmak için yapıyoruz.
            rg.angularVelocity=Vector3.zero;


        }
    }
    void OnCollisionEnter(Collision bit) 
    {
        string objeIsmi=bit.gameObject.name;
        if(objeIsmi.Equals("BitisNoktasi"))
        {
            //print("Oyun Tamamlandi!");
            oyunTamam=true;
            Durum.text="Oyunu Tamamladiniz.Tebrikler:)";
            btn.gameObject.SetActive(true);
        }
        else if(!objeIsmi.Equals("Zemin") && !objeIsmi.Equals("LabirentZemin"))
        {
            canSayaci-=1;
            KalanCan.text=canSayaci+"";
            if(canSayaci==0)
            {
                oyunDevam=false;
            }
        }
        
    }
}
