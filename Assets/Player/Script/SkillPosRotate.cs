using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPosRotate : MonoBehaviour
{
    void Update()
    {
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 200, LayerMask.GetMask("Floor"))) {
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }else
        {
            //面向鼠標
            var dir_r = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir_r.x, dir_r.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, angle - 45, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1f);
        }
    }
}
