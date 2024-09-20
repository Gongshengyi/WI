using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;
using System;
using UnityEngine.UI;
using UnityEditor;

public class LSLInput : MonoBehaviour
{
    
    private string StreamType1 = "EMG1";
    private string StreamType2 = "EMG2";
    private string StreamType3 = "EMG3";

    public float scaleInput = 1f;
    public float maxHealth = 200f;
    public Transform healthBar;

    StreamInfo[] streamInfos1, streamInfos2, streamInfos3;
    StreamInlet streamInlet1, streamInlet2, streamInlet3;

    float[] sample1, sample2, sample3;
    private int channelCount1 = 0, channelCount2 = 0, channelCount3 = 0;

    // Define EMG data stream type and initial parameters

    private bool foundStream1, foundStream2, foundStream3;

    public bool IsEMGInitialized => foundStream1 && foundStream2 && foundStream3;

    public bool log = false;

    public static Dictionary<string, int> ClassificationDict = new Dictionary<string, int>()
    {
        { "LEFT", 0 },
        { "RIGHT", 1 },
        { "BOTH", 2 },
        { "NULL_CLASS", -1 },
    };

    private string lastClassification = "NULL_CLASS"; 

    public float currentUVrms = 0.0f;
    public float currentSmoothedUVrms = 0.0f;
    private float smoothingUVrmsCoef = 0.5f; // change the smoothing coefficient

    //Initialize stream and signal buffer

    private Queue<float> signalBuffer = new Queue<float>();
    private int bufferSize = 10; // increase the buffersize

    void Start()
    {
        //Parse the stream data

        streamInfos1 = LSL.LSL.resolve_stream("name='EMGONE'", StreamType1, 1, 5.0);
        if (streamInfos1.Length > 0)
        {
            Debug.Log("found 1 stream");
            streamInlet1 = new StreamInlet(streamInfos1[0]);
            channelCount1 = streamInlet1.info().channel_count();
            streamInlet1.open_stream();
            Debug.Log("opened 1 stream");
            foundStream1 = true;
        }
        else
        {
            Debug.Log("stream 1 not found");
        }

        streamInfos2 = LSL.LSL.resolve_stream("name='EMGTWO'", StreamType2, 1, 5.0);
        if (streamInfos2.Length > 0)
        {
            Debug.Log("found 2 stream");
            streamInlet2 = new StreamInlet(streamInfos2[0]);
            channelCount2 = streamInlet2.info().channel_count();
            streamInlet2.open_stream();
            Debug.Log("opened 2 stream");
            foundStream2 = true;
        }
        else
        {
            Debug.Log("stream 2 not found");
        }

        streamInfos3 = LSL.LSL.resolve_stream("name='EMGTHREE'", StreamType3, 1, 5.0);
        if (streamInfos3.Length > 0)
        {
            Debug.Log("found 3 stream");
            streamInlet3 = new StreamInlet(streamInfos3[0]);
            channelCount3 = streamInlet3.info().channel_count();
            streamInlet3.open_stream();
            Debug.Log("opened 3 stream");
            foundStream3 = true;
        }
        else
        {
            Debug.Log("stream 3 not found");
        }
    }

     // Process and scale data samples
    void Update()
    {
        if (streamInlet1 != null)
        {
            sample1 = new float[channelCount1];
            double lastTimeStamp = streamInlet1.pull_sample(sample1, 0.00001f);
            if (lastTimeStamp > 0.0)
            {
                Process(sample1, "EMG1", lastTimeStamp); 
                while ((lastTimeStamp = streamInlet1.pull_sample(sample1, 0.0f)) != 0)
                {
                    currentUVrms = CalculateRMS(sample1); 
                    signalBuffer.Enqueue(currentUVrms); 
                    if (signalBuffer.Count > bufferSize)
                    {
                        signalBuffer.Dequeue();
                    }
                }
            }
        }

        if (streamInlet2 != null)
        {
            sample2 = new float[channelCount2];
            double lastTimeStamp = streamInlet2.pull_sample(sample2, 0.00001f);
            if (lastTimeStamp > 0.0)
            {
                Process(sample2, "EMG2", lastTimeStamp); // Call the Process function to process the acquired samples
                while ((lastTimeStamp = streamInlet2.pull_sample(sample2, 0.0f)) != 0) 
                {
                    currentUVrms = CalculateRMS(sample2); // Calculate the root mean square (RMS) value of the sample
                    signalBuffer.Enqueue(currentUVrms); // Store the calculated RMS value at the end of the signalBuffer queue
                    if (signalBuffer.Count > bufferSize) // If the number of elements in the signalBuffer queue exceeds bufferSize
                    {
                        signalBuffer.Dequeue(); // Remove the oldest data from the head of the queue, maintaining the buffer size
                    }
                }
            }
        }

        if (streamInlet3 != null)
        {
            sample3 = new float[channelCount3];
            double lastTimeStamp = streamInlet3.pull_sample(sample3, 0.00001f);
            if (lastTimeStamp > 0.0)
            {
                Process(sample3, "EMG3", lastTimeStamp);
                while ((lastTimeStamp = streamInlet3.pull_sample(sample3, 0.0f)) != 0)
                {
                    currentUVrms = CalculateRMS(sample3);
                    signalBuffer.Enqueue(currentUVrms);
                    if (signalBuffer.Count > bufferSize)
                    {
                        signalBuffer.Dequeue();
                    }
                }
            }
        }

        float smoothedUVrms = 0f;
        foreach (float value in signalBuffer) // Traversal
        {
            smoothedUVrms += value; // Add each value to smoothedUVrms
        }
        smoothedUVrms /= signalBuffer.Count; // Divide the accumulated value by the number of elements in signalBuffer to get the mean (i.e. the smoothed RMS value)
        currentSmoothedUVrms = currentSmoothedUVrms + smoothingUVrmsCoef * (smoothedUVrms - currentSmoothedUVrms); // Update the currentSmoothedUVrms value by smoothing the weighted coefficient smoothingUVrmsCoef

        Debug.Log("currentUVrms: " + currentUVrms);
        Debug.Log("currentSmoothedUVrms: " + currentSmoothedUVrms);
    }

    // Process EMG data, scale and calculate sums
    void Process(float[] emgData, string type, double timeStamp)
    {
        float sum = 0f;
        for (int i = 0; i < emgData.Length; i++)
        {
            emgData[i] = emgData[i] * 0.1f * scaleInput; 
            sum += emgData[i];
        }
        if (log)
        {
            Debug.Log(lastClassification + ": " + sum);
        }
    }

    void Process(string[] classificationData, string type, double timeStamp)
    {
        lastClassification = classificationData[0];
    }

    // Calculate the mean value of the sum of squares of the data array
    float CalculateRMS(float[] data)
    {
        float sum = 0;
        foreach (float value in data)
        {
            sum += value * value; // Add the square of each value to sum
        }
        return (float)(sum / data.Length); // Returns the mean squared value, which is equal to sum divided by the length of the array
    }
}