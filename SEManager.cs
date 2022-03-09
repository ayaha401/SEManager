using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField, Tooltip("SE")]
    List<AudioClip> _audioList = new List<AudioClip>();

    private static Dictionary<string, AudioClip> _audioDictionary = new Dictionary<string, AudioClip>();
    private static AudioSource _audioSource = null;

    public static bool SEManagerMaked = false;
    public static float SE_Volume = 0.0f;

    void Awake()
    {
        if (SEManagerMaked == false)
        {
            DontDestroyOnLoad(this);
            SEManagerMaked = true;

            AudioDictionaryUpdate();
        }
        else
        {
            AudioDictionaryUpdate();

            Destroy(this);
            return;
        }



        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SE_Volume = _audioSource.volume;
    }

    // ディクショナリの中身を更新
    private void AudioDictionaryUpdate()
    {
        foreach (AudioClip item in _audioList)
        {
            if (_audioDictionary.ContainsKey(item.name) == true)
            {
                return;
            }
            _audioDictionary.Add(item.name, item);
        }
    }

    // ディクショナリから指定されたAudioClipを返す
    private static AudioClip GetAudioClip(string audioName)
    {
        if (_audioDictionary.ContainsKey(audioName) == true)
        {
            return _audioDictionary[audioName];
        }
        else
        {
            return null;
        }
    }

    // 音にランダムなピッチを加算してランダム性を追加する。
    private static void AudioRandomize(float pitchRange)
    {
        _audioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
    }

    // SEを鳴らす
    // 重複しないで再生可能
    public static void AudioPlay(string audioName, float pitchRange = 0.0f)
    {
        AudioClip clip;
        clip = GetAudioClip(audioName);
        if (clip != null)
        {
            AudioRandomize(pitchRange);
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }

    // 重複して再生可能
    public static void AudioPlayOneShot(string audioName, float volume = 1.0f, float pitchRange = 0.0f)
    {
        AudioClip clip;
        clip = GetAudioClip(audioName);
        if (clip != null)
        {
            AudioRandomize(pitchRange);
            _audioSource.volume = volume;
            _audioSource.PlayOneShot(clip);
        }
    }

    // volume : 0~10
    public static void SetVolume(float volume)
    {
        SE_Volume = volume * 0.1f;
        _audioSource.volume = SE_Volume;
    }
}
