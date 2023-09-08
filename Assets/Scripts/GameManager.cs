using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter.Singleton{
    public class GameManager : MonoBehaviour{
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        void Start(){
            _sessionStartTime = DateTime.Now;
            Debug.Log("Game session start at: "+ DateTime.Now);
        }

        void OnApplicationQuit(){
            _sessionEndTime = DateTime.Now;
            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);
            Debug.Log("Game session ended at: "+ DateTime.Now);
            Debug.Log("Game session lasted: "+ timeDifference);
        }

        void OnGUI(){
            if(GUILayout.Button("Next Scene")){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
