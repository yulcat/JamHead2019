using System;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class EventVector3 : UnityEvent<Vector3>
{
    //internal void AddListener(Vector3 vector3)
    //{
    //    throw new NotImplementedException();
    //}
}

[System.Serializable]
public class EventVector3Line: UnityEvent<Vector3, Vector3> { }

[System.Serializable]
public class EventInt : UnityEvent<int> { }

[System.Serializable]
public class EventVector3TouchData : UnityEvent<Vector3, Vector3, Vector3, Vector3> { }
