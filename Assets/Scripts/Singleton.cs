using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Singleton{
    public class Singleton<T> :
        MonoBehaviour where T : Component{
            private static T _instance;

            public static T Instance{
                get{
                    if(_instance == null){
                        _instance = FindObjectOfType<T>();
                        if(_instance == null){
                            GameObject obj = new GameObject();
                            obj.name = typeof(T).Name;
                            _instance = obj.AddComponent<T>();
                        }
                    }
                    return _instance;
                }
            }
            public virtual void Awake(){
                if(_instance == null){
                    _instance = this as T;
                    DontDestroyOnLoad(gameObject);
                }else{
                    Destroy(gameObject);
                }
            }
        }
}

namespace Chapter.State{
    public interface IBikeState{
        void Handle(BikeController controller);
    }
}

namespace Chapter.State{
    public class BikeStateContext{
        public IBikeState CurrentState{
            get;set;
        }

        private readonly BikeController _bikeController;

        public BikeStateContext(BikeController bikeController){
            _bikeController = bikeController;
        }

        public void Transition(){
            CurrentState.Handle(_bikeController);
        }

        public void Transition(IBikeState state){
            CurrentState = state;
            CurrentState.Handle(_bikeController);
        }
    }
}

namespace Chapter.State{
    public class BikeStopState : MonoBehaviour, IBikeState{
        private BikeController _bikeController;
        public void Handle(BikeController bikeController){
            if(!_bikeController){
                _bikeController = bikeController;
            }
            _bikeController.CurrentSpeed = 0;
        }
    }
}

namespace Chapter.State{
    public class BikeStartState : MonoBehaviour, IBikeState{
        private BikeController _bikeController;
        public void Handle(BikeController bikeController){
            if(!_bikeController){
                _bikeController = bikeController;
            }
            _bikeController.CurrentSpeed = _bikeController.maxSpeed;
        }

        void Update(){
            if(_bikeController){
                if(_bikeController.CurrentSpeed > 0){
                    _bikeController.transform.Translate(Vector3.forward * (_bikeController.CurrentSpeed * Time.deltaTime));
                }
            }
        }
    }
}

namespace Chapter.State{
    public class BikeTurnState : MonoBehaviour, IBikeState{
        private Vector3 _turnDirection;
        private BikeController _bikeController;

        public void Handle(BikeController bikeController){
            if(!_bikeController){
                _bikeController = bikeController;
            }
            _turnDirection.x = (float) _bikeController.CurrentTurnDirection;
            if(_bikeController.CurrentSpeed > 0){
                transform.Translate(_turnDirection * _bikeController.turnDistance);
            }
        }
    }
}

namespace Chapter.State{
    public enum Direction{
        Left = -1,
        Right = 1
    }
}
