using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class EMGToHealthBar : MonoBehaviour
{
    public class stuff
    {
        public int bullets;
        public int grabbs;
        public int qqaas;

        public stuff(int bul, int gra, int qqa)
        {
            bullets = bul;
            grabbs = gra;
            qqaas = qqa;
        }
    }

    public GunFire gunFire;
    public int index;
    public LSLInput lslInput;
    public Slider healthBar;
    public Text healthText; // Canvas 上的 Text 组件
    //public Text encouragementText; // 用于显示提示信息的 Text 组件
    //private bool isEncouraging = false; // 用于控制提示信息的显示
    public stuff mystuff = new stuff(10, 5, 5);
    public Rigidbody bulletPrefabs;
    public Transform firepositions;
    public float bulletspeed;

    public GameObject objectC; // 物体 C
    public GameObject objectmainobj;
    public GameObject objectmainobj1;
    public GameObject objectmainobj3;
    public GameObject objectmainobj4;

    public TouchHandGrabInteractable cube1small;
    public TouchHandGrabInteractable cube2small;
    public TouchHandGrabInteractable cube3small;
    public TouchHandGrabInteractable cube4small;

    public GameObject colortored1;
    public GameObject colortored2;
    public GameObject colortored3;
    public GameObject colortored4;

    public MeshRenderer rightone;
    public MeshRenderer righttwo;
    public MeshRenderer leftone;
    public MeshRenderer lefttwo;

    private Material objectCMaterial;
    private Queue<float> signalBuffer = new Queue<float>();
    private int bufferSize = 100;

    private float updateInterval = 0f; // 设置更新间隔为0.2秒（即每0.2秒更新一次）
    private float nextUpdateTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        objectCMaterial = objectC.GetComponent<Renderer>().material;

        // 确保物体C的材质启用透明度
        if (objectCMaterial != null)
        {
            objectCMaterial.SetFloat("_Mode", 3);
            objectCMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            objectCMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            objectCMaterial.SetInt("_ZWrite", 0);
            objectCMaterial.DisableKeyword("_ALPHATEST_ON");
            objectCMaterial.EnableKeyword("_ALPHABLEND_ON");
            objectCMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            objectCMaterial.renderQueue = 3000;
        }
    }

    private float MapUVrmsToHealth(float uVrms)
    {
        float maxValue = 2000f;
        return Mathf.Clamp(uVrms / maxValue, 0, 1);

    }

    private float MapToTransparency(float value, float min, float max)
    {
        return Mathf.Clamp((value - min) / (max - min), 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdateTime)
        {
            nextUpdateTime = Time.time + updateInterval;

            float uVrmsValue = lslInput.currentSmoothedUVrms;

            signalBuffer.Enqueue(uVrmsValue);
            if (signalBuffer.Count > bufferSize)
            {
                signalBuffer.Dequeue();
            }

            float smoothedValue = 0f;
            foreach (var value in signalBuffer)
            {
                if (float.IsNaN(value))
                {
                    smoothedValue += 0f;
                }
                else
                {
                    smoothedValue += value;
                }
            }
            smoothedValue /= signalBuffer.Count;
            Debug.Log(smoothedValue);

            float healthValue = MapUVrmsToHealth(smoothedValue);
            healthBar.value = healthValue;

            // 更新 Text 组件的值
            float displayedValue = Mathf.Clamp(smoothedValue, 0, 2000); // 显示为两位小数
            healthText.text = displayedValue.ToString("F3");
            //Debug.Log(displayedValue + "-displayedValue");
            // 更新物体C的透明度
            UpdateObjectCTransparency(displayedValue);

            // 检查 Text 上的数值是否大于 690 并且未显示鼓励信息
            //if (displayedValue > 690f && !isEncouraging)
            //{
            //    StartCoroutine(ShowEncouragementText());
            //}

            if (cube1small != null)
            {
                bool abscube1 = Mathf.Abs(displayedValue - 0.060f) < 0.001f ? true : false;
                cube1small.enabled=(displayedValue >= 0.060f || abscube1);

                if (colortored1.activeInHierarchy)
                {
                rightone.material.color = displayedValue > 0.060f ? Color.green : Color.red;
                }

                else
                {
                rightone.material.color = Color.red;
                }

                Rigidbody rb = objectmainobj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = displayedValue <= 0.060f;
                }
            }

            if (cube2small != null)
            {
                cube2small.enabled = (displayedValue >= 0.080f);

                if (colortored2.activeInHierarchy)
                {
                    righttwo.material.color = displayedValue > 0.080f ? Color.green : Color.red;
                }

                else
                {
                    righttwo.material.color = Color.red;
                }

                Rigidbody rb = objectmainobj1.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = displayedValue <= 0.080f;
                }
            }

            if (cube3small != null)
            {
                cube3small.enabled = (displayedValue >= 0.060f);

                if (colortored3.activeInHierarchy)
                {
                    leftone.material.color = displayedValue > 0.060f ? Color.green : Color.red;
                }

                else
                {
                    leftone.material.color = Color.red;
                }

                Rigidbody rb = objectmainobj3.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = displayedValue <= 0.060f;
                }
            }

            if (cube4small != null)
            {
                cube4small.enabled = (displayedValue >= 0.060f);

                if (colortored4.activeInHierarchy)
                {
                    lefttwo.material.color = displayedValue > 0.060f ? Color.green : Color.red;
                }

                else
                {
                    lefttwo.material.color = Color.red;
                }

                Rigidbody rb = objectmainobj4.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = displayedValue <= 0.060f;
                }
            }

            if (index == 1)
            {
                gunFire.Fire();
            }
            
            Debug.Log("Fire");
        }
        //Debug.Log("Current Health Bar Value: " + healthValue);
    }

    void UpdateObjectCTransparency(float value)
    {
        if (objectCMaterial != null)
        {
            float alpha = MapToTransparency(value, 0, 2000);
            Color color = objectCMaterial.color;
            color.a = alpha;
            objectCMaterial.color = color;
        }
    }
}