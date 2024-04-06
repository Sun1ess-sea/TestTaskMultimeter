using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Regulator : MonoBehaviour
{
    public GameObject cursor;
    public Color defaultColor;
    public float mouseScore;
    public GameObject cursorColor;
    public float cursorAngle;
    public TMP_Text multimetrValue;
    public int multValue;

    public float a;
    public float v;
    public float r = 1000;
    public float p = 400;

    public TMP_Text canvasA;
    public TMP_Text canvasV;
    public TMP_Text canvasVac;
    public TMP_Text canvasOm;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = cursorColor.GetComponent<Renderer>().material.color;
        cursor.transform.eulerAngles = new Vector3(0, cursorAngle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        v = Mathf.Sqrt(p / r);
        a = p / v;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject == cursor)
            {
                cursorColor.GetComponent<Renderer>().material.color = new Color(255, 0, 0);

                mouseScore = Input.GetAxis("Mouse ScrollWheel");
                if (mouseScore != 0)
                {

                    if (mouseScore > 0)
                    {
                        multValue += 1;
                        if (multValue > 4)
                        {
                            multValue = 0;
                        }
                    }
                    else
                    {
                        multValue -= 1;
                        if (multValue < 0)
                        {
                            multValue = 4;
                        }
                    }

                }
                switch (multValue)
                {
                    case 1:
                        cursor.transform.eulerAngles = new Vector3(0, -45, 0); // v
                        multimetrValue.text = v.ToString("F2");
                        canvasV.text = v.ToString("F2");
                        canvasVac.text = "0";
                        canvasA.text = "0";
                        canvasOm.text = "0";
                        break;
                    case 2:
                        cursor.transform.eulerAngles = new Vector3(0, -135, 0); // v~
                        multimetrValue.text = "0,01";
                        canvasVac.text = "0,01";
                        canvasV.text = "0";
                        canvasA.text = "0";
                        canvasOm.text = "0";
                        break;
                    case 3:
                        cursor.transform.eulerAngles = new Vector3(0, -215, 0); // a
                        multimetrValue.text = a.ToString("F2");
                        canvasA.text = a.ToString("F2");
                        canvasVac.text = "0";
                        canvasV.text = "0";
                        canvasOm.text = "0";
                        break;
                    case 4:
                        cursor.transform.eulerAngles = new Vector3(0, -305, 0); // омега
                        multimetrValue.text = r.ToString();
                        canvasOm.text = r.ToString();
                        canvasVac.text = "0";
                        canvasA.text = "0";
                        canvasV.text = "0";
                        break;
                    case 0:
                        cursor.transform.eulerAngles = new Vector3(0, 90, 0); // 1 позиция
                        multimetrValue.text = "0";
                        canvasOm.text = "0";
                        canvasVac.text = "0";
                        canvasA.text = "0";
                        canvasV.text = "0";
                        break;
                }
            }
            else
            {
                cursorColor.GetComponent<Renderer>().material.color = defaultColor;
            }
        }
    }
}
