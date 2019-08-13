using System.Collections;
using UnityEngine;
using UnityEngine.Monetization;

public class AdHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject load;
    Controller Controller;
    public string storeId = "3017768";
    string placementId = "rewardedVideo";
    private void Start()
    {
        Monetization.Initialize(storeId, false);
        Controller = player.GetComponent<Controller>();
    }

    public void ShowAd()
    {
        //Debug.Log("Ad was called");
        //if (Monetization.IsReady(placementId))
        //{
        //    ShowAdPlacementContent ad = null;
        //    ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        //    if (ad != null)
        //    {
        //        ad.Show();
        //        Controller.respawn();
        //    }

        //}
        StartCoroutine(WaitForAd());

    }

    IEnumerator WaitForAd()
    {

        while (!Monetization.IsReady(placementId))
        {
            yield return null;
        }
        Debug.Log("we got here");
        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinished);
        }
    }

    void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            // Reward the player
            Debug.Log("reward");
            load.SetActive(false);
            Controller.respawn();
        }
    }

}
