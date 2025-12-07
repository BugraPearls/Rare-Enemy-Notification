using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader.Config;

namespace RareEnemyNotification
{
    public class ConfigOptions : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [LabelKey("$Mods.RareEnemyNotification.TextColor")]
        [TooltipKey("$Mods.RareEnemyNotification.TextColorTip")]
        [DefaultValue(typeof(Color), "84, 252, 252, 255"), ColorNoAlpha]
        public Color TextColor { get; set; }
    }
}
