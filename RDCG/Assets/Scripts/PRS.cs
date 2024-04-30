using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRS
{
    //카드가 위치할 위치 정보
    public Vector3 Pos;
    public Quaternion Rot;
    public Vector3 Scale;
    
    //생성자
    public PRS(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        Pos = pos;
        Rot = rot;
        Scale = scale;

    }
}
