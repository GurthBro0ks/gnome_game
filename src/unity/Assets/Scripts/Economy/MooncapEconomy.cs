using GnomeGame.Data;

namespace GnomeGame.Economy
{
    public static class MooncapEconomy
    {
        public static void Gather(PlayerProfileData profile, int amount)
        {
            if (profile == null || profile.wallet == null || amount <= 0)
            {
                return;
            }

            profile.wallet.mooncaps += amount;
        }
    }
}
