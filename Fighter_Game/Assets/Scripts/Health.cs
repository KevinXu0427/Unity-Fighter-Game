using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Health : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField] private float mCurrentHealth = 0.0f;
    public float CurrentHealth { get { return mCurrentHealth; } }
    
    private float lerpTimer;
    private float maxHealth = 100.0f;
    private float chipSpeed = 2.0f;
    public Image frontHealthBar;
    public Image backHealthBar;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // sync health
        if (stream.IsWriting)
        {
            stream.SendNext(CurrentHealth);
        }
        else
        {
            mCurrentHealth = (float)stream.ReceiveNext();
        }
    }

    private void Start()
    {
        mCurrentHealth = maxHealth;
    }

    private void Update()
    {
        mCurrentHealth = Mathf.Clamp(mCurrentHealth, 0, maxHealth);
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(Random.Range(5, 10));
        }
    }

    public void UpdateHealthUI()
    {
        //Debug.Log(mCurrentHealth);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = mCurrentHealth / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentCompete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentCompete);
        }
    }

    public void SetMaxHealth(int maxHp)
    {
        maxHealth = maxHp;
        mCurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        mCurrentHealth -= damage;
        lerpTimer = 0f;

    }
}
