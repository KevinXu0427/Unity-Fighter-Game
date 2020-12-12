using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Health : MonoBehaviourPunCallbacks,IPunObservable
{
    private int maxHealth = 100;
    [SerializeField] private int mCurrentHealth = 0;
    public int CurrentHealth { get { return mCurrentHealth; } }

    public Text healthText;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // sync health
        if (stream.IsWriting)
        {
            stream.SendNext(CurrentHealth);
        }
        else
        {
            mCurrentHealth = (int)stream.ReceiveNext();
        }
    }

    private void Start()
    {
        mCurrentHealth = maxHealth;
    }

    private void Update()
    {
        healthText.text = mCurrentHealth.ToString();
    }

    public void SetMaxHealth(int maxHp)
    {
        maxHealth = maxHp;
        mCurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        mCurrentHealth -= damage;

    }
}
