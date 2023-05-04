using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;

public class Video_Controller : MonoBehaviour
{
    string url;
    
    VideoPlayer vidplayer;


    // Start is called before the first frame update
    void Start()
    {
        // Attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");
        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

        videoPlayer.targetCameraAlpha = 0.5F;
        vidplayer = GetComponent<VideoPlayer>();
        StartCoroutine(Download());

    }

    IEnumerator Download()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3001/lecture"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                vidplayer.url = request.downloadHandler.text;
            }
        }
        yield return new WaitForSeconds(10);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
