using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.Buffs
{
    public class BurningAsh_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Burning Ash");
			// Description.SetDefault("Ash is melting your skin");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
		{
            if (player.GetModPlayer<ZAAPlayer>().ZoneInferno && !Main.dayTime && !AAWorld.downedAkuma && Main.LocalPlayer.position.Y < Main.worldSurface * 16)
            {
                //player.buffTime[buffIndex] = 5;
                player.lifeRegenTime = 1;
                player.lifeRegen -= 7;
            }
            
		}
	}
}
