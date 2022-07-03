using System.Collections;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    public GameObject ammo;
    public Transform shootingPoint;
    public int timeBetweenShoots;
    public int a = 0;
    public void Update()
    {
        if (Tower.Isbuild && a==0)
        {
            StartCoroutine(shooting());
            a = 1;
        }
    }

    private IEnumerator shooting()
    {
        while(Spawner.enemysLine != null)
        {
            Instantiate(ammo, shootingPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenShoots);
        }
        
    }
}
