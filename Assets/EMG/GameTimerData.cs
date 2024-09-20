using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameTimerData : MonoBehaviour
{
    public GunFire gunFire;
    public float gameTimer;
    public float currentTime;
    public int gameFPS;
    private List<string> allData = new List<string>();
    public GameObject targetPos1;
    public GameObject targetPos2;
    public GameObject targetPos3;
    public GameObject targetPos4;

    public Text displayText; // 这是你要在表格后添加的Text字段
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!float.IsNaN(gunFire.GunFireDisplayedValue))
        {
            gameTimer += Time.deltaTime;
            gameFPS++;

            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string textColumn = displayText != null ? displayText.text : "No Text";

            allData.Add(currentTime + "," + gameTimer.ToString("0.000") + "s," + gameFPS + ","
                + targetPos1.transform.name + ":" + targetPos1.transform.position.ToString() + ","
                + targetPos2.transform.name + ":" + targetPos2.transform.position.ToString() + ","
                + targetPos3.transform.name + ":" + targetPos3.transform.position.ToString() + ","
                + targetPos4.transform.name + ":" + targetPos4.transform.position.ToString() + ","

                + textColumn + "\n");

        }
    }
    private void OnApplicationQuit()
    {
        if (gameFPS != 0)
        {
            string fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string fileText = string.Empty;
            for (int S = 0; S < allData.Count; S++)
            {
                fileText += allData[S];
            }
            File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".txt", fileText);
        }

    }
}