﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BattleLog : MonoBehaviour
{
	public Transform log;
	public Text logEnrty;
	public Scrollbar scrollbar;    
    public Text ExpandButton_Text;
    public GameObject LogPanel;
    public static BattleLog GameLog;
    public static string NewLine;
    bool LogPanelActive;

	// Use this for initialization
	void Awake ()
	{
		if (GameLog == null)
			GameLog = this;
		else if (GameLog != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
        NewLine = System.Environment.NewLine;
        ExpandButton_Text.text = "+";
        LogPanel.SetActive(false);
        LogPanelActive = false;


    }

    public void ToggleLogePanel()
    {
        if(LogPanelActive)
        {
            ExpandButton_Text.text = "+";
            LogPanel.SetActive(false);
            LogPanelActive = false;
        }
        else
        {
            ExpandButton_Text.text = "-";
            LogPanel.SetActive(true);
            LogPanelActive = true;
        }
       
             
    }

	public static void Log (string _text)
	{
		var entry = Instantiate (BattleLog.GameLog.logEnrty);
		entry.transform.SetParent (BattleLog.GameLog.log, false);
        entry.text = _text;
       
        BattleLog.GameLog.scrollbar.value = 0;
    }

    public static void Move(Unit _unit, Vector3 _destination)
    {
        var _unitName = _unit.DsiplayName;
        var _Owner = _unit.state.Owner;

        var _LogMessage = _unitName + " [ " + _Owner + " ] " + NewLine;
                        

        Log(_LogMessage);
    }

    public static void Attack(Unit _unit, Unit _Target)
    {
        var _unitName = _unit.DsiplayName;
        var _targetName = _Target.DsiplayName;
        var _Owner = _unit.state.Owner;
        var _targetOwner = _Target.state.Owner;


        var _LogMessage = _unitName + " [ " + _Owner + " ] " + NewLine +
                            "Attacked " + _targetName + " [ " + _targetOwner + " ] " + NewLine +
                            "Weapon : " + _unit.SelectedWeapon + NewLine;


        Log(_LogMessage);
    }

    public static void Damage(Unit _unit, string _DamageType, int _damage, int _shieldDamage, int _armourDamage)
    {
        var _unitName = _unit.DsiplayName;
        var _Owner = _unit.state.Owner;
        var DamageString = "";

        if(_shieldDamage > 0)
        {
            DamageString = DamageString + "Shields Damage : " + _shieldDamage.ToString() + NewLine;
        }

        if(_armourDamage > 0)
        {
            DamageString = DamageString + "Armour Damage : " + _armourDamage.ToString() + NewLine;
        }

        var _LogMessage = _unitName + " [ " + _Owner + " ] " + " was hit! " + NewLine +
            "Damage Type : " + _DamageType  + " (" + _damage + ")" + NewLine + DamageString;

        Log(_LogMessage);
    }

}