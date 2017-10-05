using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcIK : MonoBehaviour
{
    public static double[] theta = new double[6];    //angle of the joints

    private float L1, L2, L3, L4, L5, L6;    //arm length in order from base
    private float C3;
    [SerializeField] private GameObject[] robot = new GameObject[6];

    // Use this for initialization
    void Start()
    {
        Debug.Log("Part1.size : " + robot[0].GetComponent<MeshRenderer>().bounds.size);
        Debug.Log("Part2.size : " + robot[1].GetComponent<MeshRenderer>().bounds.size);
        Debug.Log("Part3.size : " + robot[2].GetComponent<MeshRenderer>().bounds.size);
        Debug.Log("Part4.size : " + robot[3].GetComponent<MeshRenderer>().bounds.size);
        Debug.Log("Part5.size : " + robot[4].GetComponent<MeshRenderer>().bounds.size);
        Debug.Log("Part6.size : " + robot[5].GetComponent<MeshRenderer>().bounds.size);

        theta[0] = theta[1] = theta[2] = theta[3] = theta[4] = theta[5] = 0.0;
        L1 = 4.0f;
        L2 = 6.0f;
        L3 = 3.0f;
        L4 = 4.0f;
        L5 = 2.0f;
        L6 = 5.0f;
        C3 = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float px, py, pz;
        float rx, ry, rz;
        float ax, ay, az, bx, by, bz;
        float asx, asy, asz, bsx, bsy, bsz;
        float p5x, p5y, p5z;
        float C1, C23, S1, S23;

        px = transform.position.x;
        py = transform.position.y;
        pz = transform.position.z;
        rx = transform.rotation.x;
        ry = transform.rotation.x;
        rz = transform.rotation.x;

        ax = Mathf.Cos(rz * 3.14f / 180.0f) * Mathf.Cos(ry * 3.14f / 180.0f);
        ay = Mathf.Sin(rz * 3.14f / 180.0f) * Mathf.Cos(ry * 3.14f / 180.0f);
        az = -Mathf.Sin(ry * 3.14f / 180.0f);

        p5x = px - (L5 + L6) * ax;
        p5y = py - (L5 + L6) * ay;
        p5z = pz - (L5 + L6) * az;

        theta[0] = Mathf.Atan2(p5y, p5x);

        C3 = (Mathf.Pow(p5x, 2) + Mathf.Pow(p5y, 2) + Mathf.Pow(p5z - L1, 2) - Mathf.Pow(L2, 2) - Mathf.Pow(L3 + L4, 2))
            / (2 * L2 * (L3 + L4));
        theta[2] = Mathf.Atan2(Mathf.Pow(1 - Mathf.Pow(C3, 2), 0.5f), C3);

        float M = L2 + (L3 + L4) * C3;
        float N = (L3 + L4) * Mathf.Sin((float)theta[2]);
        float A = Mathf.Pow(p5x * p5x + p5y * p5y, 0.5f);
        float B = p5z - L1;
        theta[1] = Mathf.Atan2(M * A - N * B, N * A + M * B);

        C1 = Mathf.Cos((float)theta[0]);
        C23 = Mathf.Cos((float)theta[1] + (float)theta[2]);
        S1 = Mathf.Sin((float)theta[0]);
        S23 = Mathf.Sin((float)theta[1] + (float)theta[2]);

        bx = Mathf.Cos(rx * 3.14f / 180.0f) * Mathf.Sin(ry * 3.14f / 180.0f) * Mathf.Cos(rz * 3.14f / 180.0f)
            - Mathf.Sin(rx * 3.14f / 180.0f) * Mathf.Sin(rz * 3.14f / 180.0f);
        by = Mathf.Cos(rx * 3.14f / 180.0f) * Mathf.Sin(ry * 3.14f / 180.0f) * Mathf.Sin(rz * 3.14f / 180.0f)
            - Mathf.Sin(rx * 3.14f / 180.0f) * Mathf.Cos(rz * 3.14f / 180.0f);
        bz = Mathf.Cos(rx * 3.14f / 180.0f) * Mathf.Cos(ry * 3.14f / 180.0f);

        asx = C23 * (C1 * ax + S1 * ay) - S23 * az;
        asy = -S1 * ax + C1 * ay;
        asz = S23 * (C1 * ax + S1 * ay) + C23 * az;
        bsx = C23 * (C1 * bx + S1 * by) - S23 * bz;
        bsy = -S1 * bx + C1 * by;
        bsz = S23 * (C1 * bx + S1 * by) + C23 * bz;

        theta[3] = Mathf.Atan2(asy, asx);
        theta[4] = Mathf.Atan2(Mathf.Cos((float)theta[3]) * asx + Mathf.Sin((float)theta[3]) * asy, asz);
        theta[5] = Mathf.Atan2(Mathf.Cos((float)theta[3]) * bsy - Mathf.Sin((float)theta[3]) * bsx,
            -bsz / Mathf.Sin((float)theta[4]));
    }
}