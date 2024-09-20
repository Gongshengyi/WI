using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Assets.EMG
{
    public class EMGToHealthBar1 : MonoBehaviour
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


        private Material objectCMaterial;
        private Queue<float> signalBuffer = new Queue<float>();
        private int bufferSize = 100;

        private float updateInterval = 0f; // 设置更新间隔为0.2秒（即每0.2秒更新一次）
        private float nextUpdateTime = 0f;

        // Start is called before the first frame update
        void Start()
        {

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
                    smoothedValue += value;
                }
                smoothedValue /= signalBuffer.Count;

                float healthValue = MapUVrmsToHealth(smoothedValue);
                healthBar.value = healthValue;

                // 更新 Text 组件的值
                float displayedValue = Mathf.Clamp(smoothedValue, 0, 2000); // 显示为两位小数
                healthText.text = displayedValue.ToString("F3");

                // 更新物体C的透明度
                UpdateObjectCTransparency(displayedValue);

                // 检查 Text 上的数值是否大于 690 并且未显示鼓励信息
                //if (displayedValue > 690f && !isEncouraging)
                //{
                //    StartCoroutine(ShowEncouragementText());
                //}


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
}