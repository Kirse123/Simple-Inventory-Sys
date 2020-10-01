using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : MonoBehaviour
{
    [SerializeField]
    private string URL;

    public enum PostMsgType
    {
        Added, 
        Removed
    }

    private void Start()
    {
        // Subscribe for events
        EventManager.instance.ItemAdded.AddListener(OnItemAdded);
        EventManager.instance.ItemRemoved.AddListener(OnItemRemoved);
    }
    
    private void OnItemAdded(InventorySlot slot)
    {
        StartCoroutine(SendItemID(URL, slot.Item.Identifier, PostMsgType.Added));
    }    
    private void OnItemRemoved(InventorySlot slot)
    {
        StartCoroutine(SendItemID(URL, slot.Item.Identifier, PostMsgType.Removed));
    }

    /// <summary>
    /// Sends ItemID with msgType mark
    /// </summary>
    /// <param name="url"></param>
    /// <param name="ItemID"></param>
    public IEnumerator SendItemID(string url, string ItemID, PostMsgType msgType)
    {
        // Create msg Data
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();        
        switch (msgType)
        {
            case PostMsgType.Added:
                formData.Add(new MultipartFormDataSection("addedItemID", ItemID));
                break;
            case PostMsgType.Removed:
                formData.Add(new MultipartFormDataSection("removedItemID", ItemID));
                break;
        }

        // Form and send request
        UnityWebRequest uwr = UnityWebRequest.Post(URL, formData);
        // Set auth in header
        uwr.SetRequestHeader("Authorization", "Basic " + "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }
}
