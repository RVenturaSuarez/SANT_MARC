using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TakingDamage : MonoBehaviourPunCallbacks, IDamageable, IPunObservable
{
    [SerializeField] private Image healthBar;
    public float startHealth = 100;
    [SerializeField] private float health;


  
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();
            healthBar.fillAmount = health / startHealth;
        }
    }
  
  
    void Start()
    {
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
    }
  
  
    public void TakeDamage(float damage)
    {
        photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    private void RPC_TakeDamage(float damage)
    {
        if(!photonView.IsMine)
            return;
      
        health -= damage;
        Debug.Log(health);

        healthBar.fillAmount = health / startHealth;

        if (health <= 0f)
        {
            Die();
        }
    }


    public void Die()
    {
        GameSceneManager.instance.LeaveRoom();
    }
  
}