using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SelectCharStudy : MonoBehaviour
{
    public GameObject[] characters;
    public CameraController flCam;
    
    void Start()
    {
        StartCoroutine(GetGender());
    }
    IEnumerator GetGender()
    {
        flCam.target = characters[0].transform;
        characters[1].GetComponent<SpriteRenderer>().enabled = false;
        characters[1].GetComponent<movement>().enabled = false;

        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/gender"))
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
                    characters[1].GetComponent<SpriteRenderer>(). enabled = true;
                    characters[1].GetComponent<movement>(). enabled = true;
                    flCam.target = characters[1].transform;
                }
            }
        }
    }
}
