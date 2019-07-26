using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    private Button attackButton;
    private Button defendButton;
    private Button healButton;


    public Image phealthBarFill;
    public Image ehealthBarFill;

    public BattleManager bManager;

    public event System.Action CallAttack;
    public event System.Action CallDefend;
    public event System.Action CallHeal;

    public Text[] combatLogLines;
    public List<string> combatLog;

    void Awake()
    {
        bManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        bManager.UpdateHealth += UpdateHealthBar;

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DebugLogTest());

    }

    public void UpdateHealthBar(bool isPlayer, float health)
    {
        //handle fill amount in the respective scripts calling this function.
        if (isPlayer)
        {
            phealthBarFill.fillAmount = health;
        }
        else
        {
            ehealthBarFill.fillAmount = health;
        }
    }

    //

    public void CallAttackEvent()
    {
        Debug.Log("Attacked");
        CallAttack();
    }

    public void CallDefendEvent()
    {
        Debug.Log("Defend");
        CallDefend();
    }

    public void CallHealEvent()
    {
        Debug.Log("Heal");
        CallHeal();
    }

    public void UpdateCombatLog(string incLog)
    {
        //add string to list(At position 0, super important),
        combatLog.Insert(0, incLog);
        //if list lenght is > array lenght, delete top entry[last index]
        if(combatLog.Count > combatLogLines.Length)
        {
            combatLog.RemoveAt(combatLog.Count - 1);
        }
        //loop through array and set the text to strings!
        for(int i = 0; i < combatLog.Count; i++)
        {
            combatLogLines[i].text = combatLog[i];
        }
        StartCoroutine(DebugLogTest());
    }

    IEnumerator DebugLogTest()
    {
        int randomNumber = Random.Range(1, 1000);
        
        yield return new WaitForSeconds(3f);
        UpdateCombatLog(randomNumber.ToString());
        
    }
}
