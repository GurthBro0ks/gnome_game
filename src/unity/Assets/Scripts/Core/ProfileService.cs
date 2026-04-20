using System;
using GnomeGame.Data;
using GnomeGame.Economy;

namespace GnomeGame.Core
{
    public class ProfileService
    {
        private SaveManager saveManager;

        public event Action ProfileChanged;

        public PlayerProfileData Profile => saveManager != null ? saveManager.Profile : null;

        public void Initialize(SaveManager activeSaveManager)
        {
            saveManager = activeSaveManager;
            RaiseProfileChanged();
        }

        public void GatherMooncaps(int amount)
        {
            if (saveManager == null)
            {
                return;
            }

            saveManager.MutateProfile(
                profile => MooncapEconomy.Gather(profile, amount),
                "gathered mooncaps");
            RaiseProfileChanged();
        }

        public void ForceSave()
        {
            if (saveManager == null)
            {
                return;
            }

            saveManager.SaveProfile("manual force save");
            RaiseProfileChanged();
        }

        public void ReloadSave()
        {
            if (saveManager == null)
            {
                return;
            }

            saveManager.ReloadProfile();
            RaiseProfileChanged();
        }

        private void RaiseProfileChanged()
        {
            var handler = ProfileChanged;
            if (handler != null)
            {
                handler.Invoke();
            }
        }
    }
}
