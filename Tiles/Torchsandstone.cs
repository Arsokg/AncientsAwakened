using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Torchsandstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("Torchsandstone");   //put your CustomBlock name
            AddMapEntry(new Color(50, 40, 32));
            minPick = 65;
        }
    }
}