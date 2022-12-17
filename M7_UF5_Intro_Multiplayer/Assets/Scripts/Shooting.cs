using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float shotRate = 1f;
    private float shotRateTime = 0;
  
    void Update()
    {

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 10, Color.red);
      
        if (Input.GetButton("Fire1"))
        {
            if (Time.time > shotRateTime)
            {
                RaycastHit _hit;
                Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
          
                if (Physics.Raycast(ray, out _hit, 100))
                {
                    Debug.Log(_hit.collider.gameObject.name);

                    if (_hit.collider.CompareTag("Player") && !_hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
                    {
                        Debug.Log("Etoy disparando a un player");
                        _hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(20f);
                    }
                }

                shotRateTime = Time.time + shotRate;
            }
          
        }
    }
}