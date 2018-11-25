﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class CrucibleScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crucible Scale");
            Tooltip.SetDefault("The fury of the draconian sun eminates from this scale");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 4));
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(180, 41, 32);
                }
            }
        }


        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.maxStack = 999;
            item.value = 100000;
            item.rare = 11;
        }

        // The following 2 methods are purely to show off these 2 hooks. Don't use them in your own code.
        

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.OrangeRed.ToVector3() * 0.55f * Main.essScale);
        }
    }
}