using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DoorHover : MonoBehaviour
{
    public GameObject RoomPopUp;
    public Text Text;
    public RectTransform canvas;
    // Start is called before the first frame update
    void Start()
    {
        RoomPopUp.SetActive(false);
    }

    void OnMouseOver() {
        showInfo();
    }

    void OnMouseExit(){
        RoomPopUp.SetActive(false);
    }

    void showInfo(){
        StartCoroutine(GetOccupied());
        RoomPopUp.SetActive(true);
        Vector2 temp1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 temp = new Vector2(temp1.x+0.88f, temp1.y+0.73f);
        Vector2 temp2 = Camera.main.WorldToViewportPoint(temp); 
        temp2.x *= canvas.sizeDelta.x;
        temp2.y *= canvas.sizeDelta.y;

        temp2.x -= canvas.sizeDelta.x * canvas.pivot.x;
        temp2.y -= canvas.sizeDelta.y * canvas.pivot.y;

        RoomPopUp.GetComponent<RectTransform>().localPosition = temp2;


        
    }
    IEnumerator GetOccupied()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3001S/room"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }

            Text.text = "Occupied  Seat(s): " + request.downloadHandler.text;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
