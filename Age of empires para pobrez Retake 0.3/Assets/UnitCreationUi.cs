using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreationUi : MonoBehaviour
{
    public Button createUnitButton;
    public UnitSpawner selectedBase;

    void Start()
    {
        createUnitButton.onClick.AddListener(OnCreateUnitButtonClicked);
    }

    public void OnCreateUnitButtonClicked()
    {
        if (selectedBase != null && selectedBase.baseFaction == NeutralBase.Faction.Player)
        {
            selectedBase.TrySpawnUnit();
        }
    }

    public void SetSelectedBase(UnitSpawner baseToSelect)
    {
        selectedBase = baseToSelect;
    }
}
