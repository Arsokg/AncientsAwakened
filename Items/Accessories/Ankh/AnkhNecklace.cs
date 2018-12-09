using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Ankh
{
    [AutoloadEquip(EquipType.Neck)]
    public class AnkhNecklace : ModItem
	{
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AnkhShield);
            item.shieldSlot = -1;
            AutoDefaults();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().ammo20percentdown = true;
            player.meleeSpeed -= 0.07f;
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ankh Necklace");
            Tooltip.SetDefault(@"Grants immunity to knockback and fire blocks
Grants immunity to most debuffs
20% chance not to consume ammo");
        }
	}
}
