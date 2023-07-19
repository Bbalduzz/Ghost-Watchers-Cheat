using Donteco;
using UnityEngine;

namespace Ghost_Watchers_Internal.src
{
    internal class Actions
    {
        // GHOST ACTIONS
        public static GhostAI ghost;
        public static void Appear()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null) { ghost.Data.Visibility.SetVisibleForTime(3f); }
        }
        public static void RandomAction()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null) { ghost.Actions.Random(); }
        }

        public static void DoAttack()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.ResetCooldowns();
                ghost.DoCooldownAttack();
            }
        }

        public static void DoFastAttack()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.ResetCooldowns();
                ghost.DoCooldownFastAttack();
            }
        }

        public static void DoHunt()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.ResetCooldowns();
                ghost.DoCooldownHunt();
            }
        }

        public static void DoCapture()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.ResetCooldowns();
                ghost.DoCooldownCapture();
            }
        }

        public static void Teleport(Vector3 destination)
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.Movement.Teleport(destination);
            }
        }

        public static void MakeNoise()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            ghost.Audio.PlayInteractBreath();
            ghost.Audio.PlayWarningRunAttack();
        }

        public static PlayerWallet wallet;
        public static void GiveMoneyAndExp(int money, int exp)
        {
            wallet = UnityEngine.GameObject.FindObjectOfType<PlayerWallet>();
            if (wallet != null)
            {
                wallet.AddMoneyAndExp(money, exp);
            }
        }

        public static void UnlockAllAchievements()
        {
            foreach (Donteco.AchievementType a in Enum.GetValues(typeof(Donteco.AchievementType)))
            {
                Donteco.AchievementsManager.IncrementAchievementValue(a);
            }
        }
    }
}
