using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRS
{
    //ī�尡 ��ġ�� ��ġ ����
    public Vector3 Pos;
    public Quaternion Rot;
    public Vector3 Scale;
    
    //������
    public PRS(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        Pos = pos;
        Rot = rot;
        Scale = scale;

    }
}
