using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class characterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public CameraController flCam;
    public MiniCamController flMiniCam;
    
    void Start()
    {
        StartCoroutine(GetGender());
    }
    IEnumerator GetGender()
    {
        flCam.target = characters[0].transform;
        flMiniCam.target = GameObject.FindWithTag("Minimap").transform;
        characters[1].GetComponent<SpriteRenderer>().enabled = false;
        characters[1].GetComponent<movement>().enabled = false;
        GameObject.FindWithTag("MinimapOther").GetComponent<SpriteRenderer>().enabled = false;

        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3001/gender"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                if (request.downloadHandler.text.Equals("M")){
                    characters[0].GetComponent<SpriteRenderer>(). enabled = false;
                    characters[0].GetComponent<movement>(). enabled = false;
                    GameObject.FindWithTag("Minimap").GetComponent<SpriteRenderer>().enabled = false;
                    characters[1].GetComponent<SpriteRenderer>(). enabled = true;
                    characters[1].GetComponent<movement>(). enabled = true;
                    GameObject.FindWithTag("MinimapOther").GetComponent<SpriteRenderer>().enabled = true;
                    flCam.target = characters[1].transform;
                    flMiniCam.target = GameObject.FindWithTag("MinimapOther").transform;
                }
            }
        }
    }
}
