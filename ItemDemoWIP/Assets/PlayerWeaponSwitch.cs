using UnityEngine;

public class PlayerWeaponSwitch : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private GameObject rangedRoot; // 예: Pistol (Muzzle 포함된 루트)
    [SerializeField] private GameObject meleeRoot;  // 예: ClubPivot (Club 스크립트 붙은 루트)

    [Header("Config")]
    [SerializeField] private KeyCode switchKey = KeyCode.F;
    [SerializeField] private bool startWithRanged = true;

    private enum Mode { Ranged, Melee }
    private Mode mode;

    private void Start()
    {
        SetMode(startWithRanged ? Mode.Ranged : Mode.Melee);
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
            Toggle();
    }

    private void Toggle()
    {
        SetMode(mode == Mode.Ranged ? Mode.Melee : Mode.Ranged);
        // Debug.Log($"Mode: {mode}");
    }

    private void SetMode(Mode m)
    {
        mode = m;
        if (rangedRoot) rangedRoot.SetActive(mode == Mode.Ranged);
        if (meleeRoot) meleeRoot.SetActive(mode == Mode.Melee);
    }
}