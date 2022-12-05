using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

using UnityEngine.Assertions;

public class GuideUI : MonoBehaviour
{

    public Flowchart flowchart;
    string fungusBoolName = "isStart";

    public bool fungusBool
    {
        get
        {
            return flowchart.GetBooleanVariable(fungusBoolName);
        }
        set
        {
            flowchart.SetBooleanVariable(fungusBoolName, value);
        }
    }

    SkillManager manager;
    public Image move;
    public Image attack;
    public Image skill;
    public Image dash;


    float moveHintTimer;
    bool isMoveHint;
    float attackHintTimer;
    bool isAttackHint;
    float dashHintTimer;
    bool isDashHint;

    float showSkillHintTimer;
    public bool isShowSkillHint;
    float hideSkillHintTimer;
    public bool isHideSkillHint;
    void Start()
    {
        move.gameObject.SetActive(true);

        attack.gameObject.SetActive(true);

        skill.gameObject.SetActive(true);
        skill.color = new Color(1, 1, 1, 0);

        manager = GameObject.Find("Player").GetComponent<SkillManager>();
    }

    void Update()
    {
        if (fungusBool)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                isMoveHint = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                isAttackHint = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDashHint = true;
            }
            if (manager.skill != null && !isHideSkillHint)
            {
                isShowSkillHint = true;
            }
            if (isShowSkillHint && Input.GetMouseButtonDown(1))
            {
                isShowSkillHint = false;
                isHideSkillHint = true;
            }

            if (isMoveHint)
            {
                moveHintTimer += Time.deltaTime;
                move.color = new Color(1, 1, 1, 1 - (moveHintTimer));

                if (moveHintTimer >= 1)
                {
                    move.gameObject.SetActive(false);
                }
            }

            if (isAttackHint)
            {
                attackHintTimer += Time.deltaTime;
                attack.color = new Color(1, 1, 1, 1 - (attackHintTimer));

                if (attackHintTimer >= 1)
                {
                    attack.gameObject.SetActive(false);
                }
            }

            if (isDashHint)
            {
                dashHintTimer += Time.deltaTime;
                dash.color = new Color(1, 1, 1, 1 - (dashHintTimer));

                if (dashHintTimer >= 1)
                {
                    dash.gameObject.SetActive(false);
                }
            }

            if (isShowSkillHint)
            {
                if (showSkillHintTimer <= 1)
                {
                    showSkillHintTimer += Time.deltaTime;
                    skill.color = new Color(1, 1, 1, 0 + (showSkillHintTimer));
                }
            }

            if (isHideSkillHint)
            {
                hideSkillHintTimer += Time.deltaTime;
                skill.color = new Color(1, 1, 1, 1 - (hideSkillHintTimer));

                if (hideSkillHintTimer >= 1)
                {
                    skill.gameObject.SetActive(false);
                }
            }
        }

    }
}
