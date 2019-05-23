using UnityEngine;

namespace Vuforia
{
    public class TrackableHandler : MonoBehaviour, ITrackableEventHandler
    {
		public GameObject hands;
        public GameObject mainCanv;
        public GameObject riglaCanv;


		MenuManager mm;
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        private ABLoader abloader;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            
            mTrackableBehaviour = gameObject.GetComponent<TrackableBehaviour>(); 
            abloader = gameObject.GetComponent<ABLoader>();
            if (abloader)
            {
                abloader.URLSet(mTrackableBehaviour.TrackableName);
            }
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                Debug.Log("TRACKED");
                OnTrackingFound();
                OnTrackingActivated();
            }
            else if(newStatus == TrackableBehaviour.Status.DETECTED)
            {
                OnTrackingFound();
                Debug.Log("DETECTED");
            }
            else
            {
                Debug.Log("LOST");
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS
        private void OnTrackingActivated()
        {
            if (!abloader.objectActive)
            {
                Debug.Log("startdownload");
                abloader.StartDownload();                
            }

        }

        private void OnTrackingFound()
        {
			mm = GameObject.FindGameObjectWithTag("manager").GetComponent<MenuManager>();

			hands.SetActive (false);
            mainCanv.SetActive(true);
            riglaCanv.SetActive(false);
			if (!mTrackableBehaviour.TrackableName.Contains ("pavelg")) {
				PlayerPrefs.SetInt ("StartGlaz", 0);
				PlayerPrefs.Save();
				mm.GatchinaUI (false);
			}

            if (abloader.isRun)
            {
                abloader.onRequestLost = false;
                Debug.Log("onRequestFound 1111");
                abloader.ActivePreloader();
            }
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			Light[] lightComponents = GetComponentsInChildren<Light>(true);


            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }
			foreach (Light component in lightComponents)
			{
				component.enabled = true;
			}

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
		//	hands.SetActive (true);
			if (abloader.isRun)
            {
                abloader.onRequestLost = true;
                Debug.Log("onRequestLost");
            }
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

			Light[] lightComponents = GetComponentsInChildren<Light>(true);

            abloader.RefreshPreloader();

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

			foreach (Light component in lightComponents)
			{
				component.enabled = false;
			}


            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
