using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Oscillator : MonoBehaviour 
{
    public double frequency;
    private double increment;
    private double phase;
    private double sampling_frequency = 44100.0;

    public float gain;
    public float volume;
    public float currentMixerVolume;

    public float[] frequencies;
    private float[][] volumes;
    public int thisFreq;

    public double[] shepardTones;
    public int shepardPos;

    public AudioMixer shepardMixer;

    void Start()
    {
        volume = 0.1f;
        shepardTones = new double[7];

        frequencies = new float[31];
        frequencies[0] = 65.775f;       //C
        frequencies[1] = 67.260f;       //Cssh
        frequencies[2] = 68.790f;       //Csh
        frequencies[3] = 70.340f;       //Df
        frequencies[4] = 71.930f;       //Dsf
        frequencies[5] = 73.555f;       //D
        frequencies[6] = 75.220f;       //Dssh
        frequencies[7] = 76.920f;       //Dsh
        frequencies[8] = 78.660f;       //Ef
        frequencies[9] = 80.440f;       //Esf
        frequencies[10] = 82.255f;      //E
        frequencies[11] = 84.120f;      //Ff
        frequencies[12] = 86.020f;      //Esh
        frequencies[13] = 87.960f;      //F
        frequencies[14] = 89.950f;      //Fssh
        frequencies[15] = 91.990f;      //Fsh
        frequencies[16] = 94.070f;      //Gf
        frequencies[17] = 96.190f;      //Gsf
        frequencies[18] = 98.365f;      //G
        frequencies[19] = 100.590f;     //Gssh
        frequencies[20] = 102.870f;     //Gsh
        frequencies[21] = 105.190f;     //Af
        frequencies[22] = 107.570f;     //Asf
        frequencies[23] = 110.000f;     //A
        frequencies[24] = 112.490f;     //Assh
        frequencies[25] = 115.030f;     //Ash
        frequencies[26] = 117.640f;     //Bf
        frequencies[27] = 120.300f;     //Bsf
        frequencies[28] = 123.015f;     //B
        frequencies[29] = 125.800f;     //Cf
        frequencies[30] = 128.640f;     //Bsh

        volumes = new float[31][]; //Shepard Tone dB's
        volumes[0] = new float[7] {-54.838f,-34.838f,-14.838f,0.000f,0.000f,0.000f,-5.162f};        //C
        volumes[1] = new float[7] {-54.194f,-34.194f,-14.194f,0.000f,0.000f,0.000f,-5.806f};        //Cssh
        volumes[2] = new float[7] {-53.545f,-33.545f,-13.545f,0.000f,0.000f,0.000f,-6.455f};        //Csh
        volumes[3] = new float[7] {-52.902f,-32.902f,-12.902f,0.000f,0.000f,0.000f,-7.098f};        //Df
        volumes[4] = new float[7] {-52.257f,-32.257f,-12.257f,0.000f,0.000f,0.000f,-7.743f};        //Dsf
        volumes[5] = new float[7] {-51.612f,-31.612f,-11.612f,0.000f,0.000f,0.000f,-8.387f};        //D
        volumes[6] = new float[7] {-50.966f,-30.966f,-10.966f,0.000f,0.000f,0.000f,-9.034f};        //Dssh
        volumes[7] = new float[7] {-50.321f,-30.321f,-10.321f,0.000f,0.000f,0.000f,-9.679f};        //Dsh
        volumes[8] = new float[7] {-49.676f,-29.676f,-9.676f,0.000f,0.000f,0.000f,-10.324f};        //Ef
        volumes[9] = new float[7] {-49.030f,-29.030f,-9.030f,0.000f,0.000f,0.000f,-10.970f};        //Esf
        volumes[10] = new float[7] {-48.387f,-28.387f,-8.387f,0.000f,0.000f,0.000f,-11.613f};       //E
        volumes[11] = new float[7] {-47.740f,-27.740f,-7.740f,0.000f,0.000f,0.000f,-12.260f};       //Ff
        volumes[12] = new float[7] {-47.095f,-27.095f,-7.095f,0.000f,0.000f,0.000f,-12.905f};       //Esh
        volumes[13] = new float[7] {-46.452f,-26.452f,-6.452f,0.000f,0.000f,0.000f,-13.548f};       //F
        volumes[14] = new float[7] {-45.806f,-25.806f,-5.806f,0.000f,0.000f,0.000f,-14.194f};       //Fssh
        volumes[15] = new float[7] {-45.159f,-25.159f,-5.159f,0.000f,0.000f,0.000f,-14.841f};       //Fsh
        volumes[16] = new float[7] {-44.514f,-24.514f,-4.514f,0.000f,0.000f,0.000f,-15.486f};       //Gf
        volumes[17] = new float[7] {-43.871f,-23.871f,-3.871f,0.000f,0.000f,0.000f,-16.129f};       //Gsf
        volumes[18] = new float[7] {-43.226f,-23.226f,-3.226f,0.000f,0.000f,0.000f,-16.774f};       //G
        volumes[19] = new float[7] {-42.580f,-22.580f,-2.580f,0.000f,0.000f,0.000f,-17.420f};       //Gssh
        volumes[20] = new float[7] {-41.934f,-21.934f,-1.934f,0.000f,0.000f,0.000f,-18.066f};       //Gsh
        volumes[21] = new float[7] {-41.290f,-21.290f,-1.290f,0.000f,0.000f,0.000f,-18.710f};       //Af
        volumes[22] = new float[7] {-40.645f,-20.645f,-0.645f,0.000f,0.000f,0.000f,-19.355f};       //Asf
        volumes[23] = new float[7] {-40.000f,-20.000f,0.000f,0.000f,0.000f,0.000f,-20.000f};        //A
        volumes[24] = new float[7] {-39.354f,-19.354f,0.000f,0.000f,0.000f,-0.646f,-20.646f};       //Assh
        volumes[25] = new float[7] {-38.710f,-18.710f,0.000f,0.000f,0.000f,-1.290f,-21.290f};       //Ash
        volumes[26] = new float[7] {-38.062f,-18.062f,0.000f,0.000f,0.000f,-1.938f,-21.938f};       //Bf
        volumes[27] = new float[7] {-37.417f,-17.417f,0.000f,0.000f,0.000f,-2.583f,-22.583f};       //Bsf
        volumes[28] = new float[7] {-36.773f,-16.773f,0.000f,0.000f,0.000f,-3.226f,-23.226f};       //B
        volumes[29] = new float[7] {-36.127f,-16.127f,0.000f,0.000f,0.000f,-3.873f,-23.873f};       //Cf
        volumes[30] = new float[7] {-35.483f,-15.483f,0.000f,0.000f,0.000f,-4.517f,-24.517f};       //Bsh

    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     SetSound(volumes[thisFreq][shepardPos]);
        //     gain = volume; // / shepardTone.Length;
        //     frequency = frequencies[thisFreq];
        //     thisFreq += 1;
        //     thisFreq = thisFreq % frequencies.Length;
        //     shepardTones[0] = frequency;

        //     for(int j = 1; j < shepardTones.Length; j++)
        //     {
        //         shepardTones[j] = shepardTones[j-1] * 2;
        //     }
        // }
        
        // frequency = shepardTones[shepardPos];

        // if(Input.GetKeyUp(KeyCode.Space))
        // {
        //     gain = 0;
        // }
    }

    public void PlayTune(int _pos)
    {
        thisFreq = _pos;
        SetSound(volumes[thisFreq][shepardPos]);
        frequency = frequencies[thisFreq];
        //thisFreq = thisFreq % frequencies.Length;
        shepardTones[0] = frequency;

        for(int j = 1; j < shepardTones.Length; j++)
        {
            shepardTones[j] = shepardTones[j-1] * 2;
        }

        frequency = shepardTones[shepardPos];

 
        gain = volume;
        
    }

    public void StopPlaying()
    {
        gain = 0;
    }

    /// <summary>
    /// If OnAudioFilterRead is implemented, Unity will insert a custom filter into the
    /// audio DSP chain.
    /// </summary>
    /// <param name="data">An array of floats comprising the audio data.</param>
    /// <param name="channels">An int that stores the number of channels
    ///                        of audio data passed to this delegate.</param>
    void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency  * 2.0 * Mathf.PI / sampling_frequency;


        for (int i = 0; i < data.Length; i+= channels)
        {
            phase += increment;
            // this is where we copy audio data to make them “available” to Unity
            data[i] = (float) (gain * Mathf.Sin((float)phase));

            // if we have stereo, we copy the mono data to each channel
            if(channels == 2)
            {
                data[i + 1] = data[i];
            }

            // if(phase > (Mathf.PI * 2))
            // {
            //     phase = 0.0;
            // }
        } 
    }

    public void SetSound(float soundLevel)
    {  
        shepardMixer.SetFloat ("vol" + shepardPos, soundLevel);
    }
}