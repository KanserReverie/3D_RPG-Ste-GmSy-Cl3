using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factions
{
    // Add if you want faction name.
    public string factionName;

    [SerializeField, Range(-1,1)]
    private float _aproval;

    public float Approval
    {
        set
        {
            _aproval = Mathf.Clamp(value, -1, 1);
        }
        get
        {
            return _aproval;
        }
    }

    public Factions(float initialApproval)
    {
        Approval = initialApproval;
    }
}

public class FactionsManager : MonoBehaviour
{
    [SerializeField]
    Dictionary<string, Factions> factions;
    [SerializeField]
    List<Factions> initaliseFactions;

    // This is the only one.
    public static FactionsManager instance;
    // If there none make one.
    private void Awake()
    {
        // Make one.
        if (instance == null)
        {
            instance = this;
        }
        // If there is another, destroy this.
        else
        {
            Destroy(this);
        }

        factions = new Dictionary<string, Factions>();

        foreach (Factions faction in initaliseFactions)
        {
            factions.Add(faction.factionName, faction);
        }
    }



        public float? FactionsApproval(string factionName, float value)
    {
        if (factions.ContainsKey(factionName))
        {
            factions[factionName].Approval += value;
            return factions[factionName].Approval;
        }

        return null;
    }

    // Overload Funtion
    // If only factionname will just return the aproval of the clan.
    public float? FactionsApproval(string factionName)
    {
        if (factions.ContainsKey(factionName))
        {
            return factions[factionName].Approval;
        }

        return null;
    }

    void PretendMethod()
    {
        FactionsManager.instance.FactionsApproval("AppleClan", -0.05f);
    }


}

