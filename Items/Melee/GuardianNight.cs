using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class GuardianNight : ModItem
    {
        
        public override void SetDefaults()
        {

            item.damage = 174;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 78;              //Sword width
            item.height = 78;             //Sword height
            item.useTime = 26;          //how fast 
            item.useAnimation = 26;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4;      //Sword knockback
            item.value = 20;        
            item.rare = 7;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;
            
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guardian of the Depths");
            Tooltip.SetDefault("");
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

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "NightKeeper", 1);
			recipe.AddIngredient(null, "DeepAbyssium", 10);
			recipe.AddIngredient(ItemID.Ectoplasm, 15);//you need 1 DirtBlock
            recipe.AddTile(TileID.MythrilAnvil);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
