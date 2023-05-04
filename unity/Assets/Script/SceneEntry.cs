using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class SceneEntry : MonoBehaviour
{
    public string direction;
    public void OnTriggerEnter2D(Collider2D other)
    {        
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            if (direction == "IN") {
                StartCoroutine(Upload("add", "StudyRoom"));
            }
            else {
                StartCoroutine(Upload("minus", "SL_1"));
            }
        }      
    }


    public IEnumerator Upload(string op, string loadScene)
    {
        WWWForm form = new WWWForm();
        form.AddField("op", op); 

        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/room", form))
        {   
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Connection succeeds!");
            }
            if (request.downloadHandler.text == "received"){
                SceneManager.LoadScene(loadScene);
            }
        }
        yield return new WaitForSeconds(1);
    }

}
