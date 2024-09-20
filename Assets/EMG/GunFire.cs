using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using static Assets.EMG.EMGToHealthBar1;

public class GunFire : MonoBehaviour
{
    public LSLInput lslInput;
    public Slider healthBar;
    public Text healthText;
    private bool hasFired = false;
    public stuff mystuff = new stuff(10, 5, 5);

    private Queue<float> signalBuffer = new Queue<float>();
    private int bufferSize = 100;

    public Rigidbody bulletPrefabs;
    public Transform firepositions;
    public float bulletspeed;

    public float GunFireDisplayedValue;
    // Start is called before the first frame update

    float MapUVrmsToHealth(float uVrms)
    {
        float maxValue = 2000f;
        return Mathf.Clamp(uVrms / maxValue, 0, 1);
    }

    void Update()
    {
        float uVrmsValue = lslInput.currentSmoothedUVrms;

        signalBuffer.Enqueue(uVrmsValue);
        if (signalBuffer.Count > bufferSize)
        {
            signalBuffer.Dequeue();
        }

        float smoothedValue = 0f;
        foreach (var value in signalBuffer)
        {
            smoothedValue += value;
        }
        smoothedValue /= signalBuffer.Count;

        float healthValue = MapUVrmsToHealth(smoothedValue);
        healthBar.value = healthValue;

        float displayedValue = Mathf.Clamp(smoothedValue, 0, 2000); // 显示为两位小数
        GunFireDisplayedValue=displayedValue;
        healthText.text = displayedValue.ToString("F3");

        if (displayedValue > 100.100f && !hasFired && mystuff.bullets > 0)
        {
            hasFired = true;
            Rigidbody bulletInstance = Instantiate(bulletPrefabs, firepositions.position, firepositions.rotation) as Rigidbody;
            bulletInstance.AddForce(-firepositions.forward * bulletspeed);
            //mystuff.bullets--;

            Debug.Log("Remaining bullets:" + mystuff.bullets);
        }
        else if (displayedValue < 100.100f)
        {
            hasFired = false;
        }
    }
    // Update is called once per frame
    public void Fire()
    {

    //    if (displayedValue > 100.050f && !hasFired && mystuff.bullets > 0)
    //    {
    //        hasFired = true;
    //        Rigidbody bulletInstance = Instantiate(bulletPrefabs, firepositions.position, firepositions.rotation) as Rigidbody;
    //        bulletInstance.AddForce(-firepositions.forward * bulletspeed);
    //        //mystuff.bullets--;

    //        Debug.Log("Remaining bullets:" + mystuff.bullets);
    //    }
    //    else if (healthBar.value <= 0.7)
    //    {
    //        hasFired = false;
    //    }
    }
}
