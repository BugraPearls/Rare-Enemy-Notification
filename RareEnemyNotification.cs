using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RareEnemyNotification
{
	enum MessageType
    {
        FoundRareEnemyToClients
    }
	public class RareEnemyNotification : Mod
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();
            switch (msgType)
            {
                case MessageType.FoundRareEnemyToClients:
                    EnemySpawn.NotifEffect(reader.ReadInt32());
                    break;

            }
        }
            }
	public class EnemySpawn : GlobalNPC
	{
        public static void NotifEffect(int enemyType)
        {
            if (ModContent.GetInstance<ConfigOptions>().LifeformAnalyzer == false || (ModContent.GetInstance<ConfigOptions>().LifeformAnalyzer && Main.LocalPlayer.accCritterGuide)) //WHY IS LIFEFORM ANALYZER CRITTERGUIDE IN CODE???????????????????????
            {
                NPC npc = ContentSamples.NpcsByNetId[enemyType];
                Main.NewText(Language.GetTextValue("Mods.RareEnemyNotification.ChatText").Replace("<name>", npc.TypeName));
                CombatText.NewText(Main.LocalPlayer.getRect(), ModContent.GetInstance<ConfigOptions>().TextColor, Language.GetTextValue("Mods.RareEnemyNotification.PopupText").Replace("<name>", npc.TypeName), true);
                SoundEngine.PlaySound(SoundID.Item35 with { PitchRange = (-0.9f, -0.2f) });
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (npc.rarity > 0)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    NotifEffect(npc.type);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ModPacket packet = ModContent.GetInstance<RareEnemyNotification>().GetPacket();
                    packet.Write((byte)MessageType.FoundRareEnemyToClients);
                    packet.Write(npc.netID);
                    packet.Send();
                }
            }
        }
	}
}
