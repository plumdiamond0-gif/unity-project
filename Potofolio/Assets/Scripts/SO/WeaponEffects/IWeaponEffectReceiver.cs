using Unity.Hierarchy;
using UnityEngine;
   public interface IWeaponEffectReceiver
{
    void ApplySlow(float slowAmount, float slowDuration);
    void ApplyStun(float stunDuration);
    void ApplyFire(float DotDam, float DotNum);
    void ApplyToxic(float DotDam, float DotNum);
    void ApplyRangeDam(float damage, float range);
    void ApplyKnockBack(float force, float upMoidfier, float radius, float rangeDam);

}
