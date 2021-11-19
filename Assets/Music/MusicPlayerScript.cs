using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    public AudioSource _source;

    //---------Lazy Instantiation Code----------------
    private static MusicPlayerScript _instance;
    public static MusicPlayerScript Instance
    {
        //if we try to access it, check to see if we need to create one
        get
        {
            //attempt to find in scene
            if (_instance == null)
            {
                //attempt to find scene
                _instance = FindObjectOfType<MusicPlayerScript>();
                //if none found in scene, create one
                if (_instance == null)
                {
                    //make new gameobject
                    GameObject newGameObject = new GameObject();
                    //attach Musicplayer and mark as singleton
                    _instance = newGameObject.AddComponent<MusicPlayerScript>();
                    newGameObject.name = "Music Player (singleton)";
                    //make scene persistent
                    DontDestroyOnLoad(newGameObject);
                }
            }
            return _instance;
        }

    }
    //--------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        SetupSourceDefaults();
    }

    private void SetupSourceDefaults()
    {
        //add audosource and attach
        _source = gameObject.AddComponent<AudioSource>();
        _source.volume = .5f;
        _source.playOnAwake = false;
        _source.loop = true;
        //set audiosource to 2D
        _source.spatialBlend = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(AudioClip clip)
    {
        //configure
        _source.clip = clip;
        //activate
        _source.Play();
    }

    public void Stop()
    {
        _source.Stop();
    }

    public void Pause()
    {
        _source.Pause();
    }
}
