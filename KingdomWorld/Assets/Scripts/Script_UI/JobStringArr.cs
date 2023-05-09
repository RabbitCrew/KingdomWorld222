using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobStringArr : MonoBehaviour
{
    protected string[] jobArr = new string[13]
        { "시민", "나무꾼", "목수", "사냥꾼", "농부", "목축업자", "창고지기", "철광부", "돌광부", "햄장인", "치즈장인", "옷감장인", "대장장이" };

    protected string[] buildingArr = new string[17]
        {"나무", "광산", "거래소", "목공소",
        "치즈공장", "옷감공장", "농장", "햄공장",
        "거주지", "사냥꾼의 오두막", "광부의 오두막", "대장간",
        "창고", "대학교", "나무꾼의 오두막", "밭",
        "길" };
}
