using UnityEngine;
   public interface IWeaponEffectReceiver
{
    void ApplySlow(float slowAmount, float slowDuration);
    void ApplyStun(float stunDuration);
    void ApplyDotDam(float DotDam, float DotNum);
   // void ApplyKnockBack(float slowAmount, float slowDuration);

}
