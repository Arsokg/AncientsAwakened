using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class CatsEyeRifle : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cat's Eye Rifle");
            Tooltip.SetDefault(@"Fires Shadow bolts
Doesn't require ammo
'QUICK HIDE THE LOLI STASH'
-Liz");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override void SetDefaults()
        {
            
            item.damage = 530;
            item.noMelee = true;
            item.ranged = true; 
            item.width = 72; 
            item.height = 22;
            item.useTime = 30; 
            item.useAnimation = 30; 
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("CatsEye");
            item.knockBack = 12;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 9; 
            item.UseSound = new LegacySoundStyle(2, 40, Terraria.Audio.SoundType.Sound);
            item.autoReuse = true; 
            item.shootSpeed = 20f;
            item.crit = 0;
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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(121, 21, 214);
                }
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}