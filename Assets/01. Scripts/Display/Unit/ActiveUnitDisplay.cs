using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;


public class ActiveUnitDisplay : MonoBehaviour
{
    #region Variables
    public Button ActionButton;
    public Button Next_ActionButton;
    public Button Prev_ActionButton;
    public Text UnitName;
	public Image ActionIcon;
	public Text ActionName;
    public Text currentAP;
    public Text APCost;
    public Text ActiveTarget;

    public Text Armour;
	public Text Shields;
    public Text Engines;
    public Text Evasion;
    public Text Speed;
    public Text Size;
    public Text ArmourType;
    public Transform weaponsPanel;
    public Transform DetailsPanel;
    public Transform weaponsList;
	public WeaponDisplay weaponDisplayPrefab;

	Unit unit;
	List<WeaponDisplay> weaponDisplays = new List<WeaponDisplay> ();


    bool weaponsPanelHidden;
    bool detailsPanelHidden;
    #endregion

    #region Delegates & Events

    public delegate void UnitModelDisplayDelegate ();

	public event UnitModelDisplayDelegate onChangeAction;

	#endregion

    void Awake()
    {
        weaponsPanel.Translate(0, -120, 0);
        weaponsPanelHidden = true;
        DetailsPanel.Translate(0, -120, 0);
        detailsPanelHidden = true;
    }


	public void Prime (Unit _unitModel)
	{
		unit = _unitModel;        
        clearWeaponDisplays ();

		if (unit.SelectedWeapon == null || unit.SelectedWeapon == "")
			unit.SelectedWeapon = unit.Weapons.First ();

		if (unit.SelectedAction == null || unit.SelectedAction == "")
			unit.SelectedAction = unit.Actions.First ();

		var _selectedActionIcon = Game.Register.GetActionIcon (_unitModel.SelectedAction);

        if (ActiveTarget != null && Game.BattleManager.ActiveUnit.ActiveTarget != null)
            ActiveTarget.text = Game.BattleManager.ActiveUnit.ActiveTarget.DsiplayName;

        #region UnitDetails
        if (UnitName != null)
            UnitName.text = unit.state.UnitType;
        if (ActionName != null)
			ActionName.text = unit.SelectedAction;
		if (ActionIcon != null)
			ActionIcon.sprite = _selectedActionIcon;
       
        if (Armour != null)
            Armour.text = unit.state.armour.ToString();
        if (Shields != null)
            Shields.text = unit.state.shields.ToString();
        if (Engines != null)
            Engines.text = unit.state.engines.ToString();
        if (Evasion != null)
            Evasion.text = unit.state.evasion.ToString();       
        if (Size != null)
            Size.text = unit.state.Size.ToString();
        if (ArmourType != null)
            ArmourType.text = unit.state.armourType.ToString();
        #endregion

        if (weaponsList != null)
		{
            PrimeWeapons();
        }

        checkAP();

        //if (ActionButton != null)
        //{
        //    if (!Battle.LocalPlayerTurn)
        //    {
        //        ActionName.text = "Waiting";
        //        ActionButton.interactable = false;
        //        Next_ActionButton.interactable = false;
        //        Prev_ActionButton.interactable = false;
        //    }
        //    else
        //    {
        //        Next_ActionButton.interactable = true;
        //        Prev_ActionButton.interactable = true;

        //    }
        //}
    }

    public void ActionClick()
    {
        //unit.FireSelectedWeapon(Game.BattleManager.ActiveUnit.ActiveTarget);

        var _selectedAction = unit.SelectedAction;

        if (_selectedAction != null || _selectedAction != "")
        {
           
        }

    


        
    }

	public void NextAction ()
	{
		var _actionList = unit.Actions;
		var i = _actionList.IndexOf (unit.SelectedAction);

		if (i < _actionList.Count () - 1)
		{
			unit.SelectedAction = _actionList [i + 1];
		} else
		{
			unit.SelectedAction = _actionList [0];
		}

		var _selectedActionIcon = Game.Register.GetActionIcon (unit.SelectedAction);

		if (ActionName != null)
			ActionName.text = unit.SelectedAction;
		
		if (ActionIcon != null)
			ActionIcon.sprite = _selectedActionIcon;

        if (APCost != null)
           

        if (onChangeAction != null)
			onChangeAction.Invoke ();

        checkAP();
    }

	public void PrevAction ()
	{
		var _actionList = unit.Actions;
		var i = _actionList.IndexOf (unit.SelectedAction);

		if (i > 0)
		{
			unit.SelectedAction = _actionList [i - 1];
		} else
		{
			unit.SelectedAction = _actionList [_actionList.Count () - 1];
		}

		var _selectedActionIcon = Game.Register.GetActionIcon (unit.SelectedAction);

		if (ActionName != null)
			ActionName.text = unit.SelectedAction;
		if (ActionIcon != null)
			ActionIcon.sprite = _selectedActionIcon;

        if (APCost != null)
          

        if (onChangeAction != null)
			onChangeAction.Invoke ();

        checkAP();
    }

    public void ToggleWeaponsPanel()
    {
        if(weaponsPanelHidden)
        {
            weaponsPanel.Translate(0, 120, 0);
            weaponsPanelHidden = false;
        }
        else
        {
            weaponsPanel.Translate(0, -120, 0);
            weaponsPanelHidden = true;
        }
        
    }

    public void ToggleDetailsPanel()
    {
        if (detailsPanelHidden)
        {
            DetailsPanel.Translate(0, 120, 0);
            detailsPanelHidden = false;
        }
        else
        {
            DetailsPanel.Translate(0, -120, 0);
            detailsPanelHidden = true;
        }

    }

    public void UpdateActiveTarget()
    {
        if (ActiveTarget != null && Game.BattleManager.ActiveUnit.ActiveTarget != null)
            ActiveTarget.text = Game.BattleManager.ActiveUnit.ActiveTarget.DsiplayName;
    }

    public void NextTarget()
    {


    }

    public void PrevTarget()
    {

      
    }

    void highlightSelectedWeapon ()
	{
		foreach (var item in weaponDisplays)
		{
			if (item.weaponName.text == unit.SelectedWeapon)
				item.weaponName.fontStyle = FontStyle.Bold;
			else
				item.weaponName.fontStyle = FontStyle.Normal;
		}


	}

	void WeaponDsiplay_onClick (Weapon _weapon)
	{
		unit.SelectedWeapon = _weapon.name;
		highlightSelectedWeapon ();
     
    }

	void onDestroy ()
	{
		clearWeaponDisplays ();
	}

	void clearWeaponDisplays ()
	{
		foreach (var weaponDsiplay in weaponDisplays)
		{
			weaponDsiplay.onClick -= WeaponDsiplay_onClick;
		}

		for (int i = 0; i < weaponsList.childCount; i++)
		{
			Destroy (weaponsList.GetChild (i).gameObject);
		}

		weaponDisplays.Clear ();

	}

    void checkAP()
    {
      
    }

    void PrimeWeapons()
    {
        foreach (var item in unit.Weapons)
        {
            var weapon = Game.Register.GetWeapon(item);
            var weaponDsiplay = (WeaponDisplay)Instantiate(weaponDisplayPrefab);
            weaponDsiplay.transform.SetParent(weaponsList, false);
            weaponDsiplay.Prime(weapon);
            weaponDsiplay.onClick += WeaponDsiplay_onClick;
            weaponDisplays.Add(weaponDsiplay);
        }

        highlightSelectedWeapon();
    }





}
