using UnityEngine;
using System.Collections;

public class BurstFiringMode : MonoBehaviour, IFiringMode
{
    public int burstCount = 3;
    public float burstCoolDown = 0.3f;

    public string GetModeName()
    {
        return "Burst";
    }

    public void Fire(float fireRate, System.Action shoot, System.Action setTriggerReady)
    {
        StartCoroutine(FireCoroutine(fireRate, shoot, setTriggerReady));
    }

    private IEnumerator FireCoroutine(float fireRate,System.Action shoot, System.Action setTriggerReady)
    {
        for (int i = 0; i < burstCount; i++)
        {
            shoot();
            if (i == burstCount - 1)
            {
                break;
            }
            yield return new WaitForSeconds(fireRate);
        }
        yield return new WaitForSeconds(burstCoolDown);
        setTriggerReady();
    }

    public bool CheckInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }
}