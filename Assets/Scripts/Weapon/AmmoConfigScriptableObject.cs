using UnityEngine;

[CreateAssetMenu(fileName = "Ammo Config", menuName = "Weapons/Ammo Configuration", order = 3)]
public class AmmoConfigScriptableObject : ScriptableObject
{
    public int maxAmmo = 120;
    public int clipSize = 30;
    public int currentAmmo = 120;
    public int currentClipAmmo = 30;

    public void Reload()
    {
        int maxReloadAmount = Mathf.Min(clipSize, currentAmmo);
        int availableBulletsInCurrentClip = clipSize - currentClipAmmo;
        int reloadAmount = Mathf.Min(maxReloadAmount, availableBulletsInCurrentClip);

        currentClipAmmo += reloadAmount;
        currentAmmo -= reloadAmount;
    }

    public bool CanReload()
    {
        return currentClipAmmo < clipSize && currentAmmo > 0;
    }
}
