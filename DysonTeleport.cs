using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

public class DysonTeleport : Teleporter
{
    public CinemachineVirtualCamera activeCam;
    private LevelManager levelManager;
    
    private CinemachineVirtualCamera vcam;
    private CinemachineVirtualCamera[] virtualCameras;  

    protected override void Start()
    {		
        _ignoreList = new List<Transform>();
			
        levelManager = FindObjectOfType<LevelManager>();
        virtualCameras = levelManager.GetComponent<DysonLevelManager>().vCams;

        //virtualCameras = LevelManager.Instance.GetComponent<FollowPlayer>().vCams;

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected override IEnumerator TeleportEnd()
    {



        if (FadeToBlack)
        {
            //if (TeleportCamera)
            //{
            //    LevelManager.Instance.LevelCameraController.FollowsPlayer = false;
            //}
            MMFadeInEvent.Trigger(FadeDuration);
            yield return new WaitForSeconds (FadeDuration);
            //if (TeleportCamera)
            //{
            //    LevelManager.Instance.LevelCameraController.TeleportCameraToTarget ();
            //    LevelManager.Instance.LevelCameraController.FollowsPlayer = true;
					
            //}
            MMFadeOutEvent.Trigger(FadeDuration);
        }
        else
        {
            if (TeleportCamera)
            {
                LevelManager.Instance.LevelCameraController.TeleportCameraToTarget ();

            }	
        }

        foreach (var vCam in virtualCameras.ToList())
        {
            vCam.Priority = 10;
        }
        activeCam.Priority = activeCam.Priority + 10;


        //LevelManager.Instance.GetComponent<FollowPlayer>().SetActiveCamera();
        //LevelManager.Instance.GetComponent<FollowPlayer>().SetActiveCamera();

    }

    //    private void OnTriggerEnter2D(Collider2D other)
    //    {
    //        foreach (var vCam in virtualCameras.ToList())
    //        {
    //            vCam.Priority = 10;
    //        }
    //        activeCam.Priority = 20;
    //    }

    /// <summary>
    /// When something exits the teleporter, if it's on the ignore list, we remove it from it, so it'll be considered next time it enters.
    /// </summary>
    /// <param name="collider">Collider.</param>
    protected override void OnTriggerExit2D(Collider2D collider)
    {
        if (_ignoreList.Contains(collider.transform))
        {
            _ignoreList.Remove(collider.transform);
        }
        base.OnTriggerExit2D(collider);
    }

}
