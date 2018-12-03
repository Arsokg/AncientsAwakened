using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.NPCs.Bosses.Orthrus
{
    [AutoloadBossHead]
    public class OrthrusHead1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orthrus X");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.lifeMax = 22000;
            npc.width = 36;
            npc.height = 32;
            npc.npcSlots = 0;
            npc.dontCountMe = true;
            npc.noTileCollide = false;
            npc.boss = false;
            npc.noGravity = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public Orthrus Body = null;
        public bool leftHead = false;
        public int damage = 0;

        public int distFromBodyX = 50; //how far from the body to centeralize the movement points. (X coord)
        public int distFromBodyY = 70; //how far from the body to centeralize the movement points. (Y coord)
        public int movementVariance = 60; //how far from the center point to move.

        public override void AI()
        {
            
            npc.realLife = (int)npc.ai[0];
            if (Body == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == mod.NPCType("Orthrus"))
                {
                    Body = (Orthrus)npcBody.modNPC;
                }
            }
            if (!Body.npc.active)
            {
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                return;
            }
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
                //attackDelay = 180;
            }
            else
            {
                damage = npc.damage / 2;
            }
            int num429 = 1;
            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
            {
                num429 = -1;
            }
            npc.TargetClosest();
            Player targetPlayer = Main.player[npc.target];
            Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
            float num433 = 6f;
            PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
            PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += npc.velocity.Y * 0.5f;
            PlayerPosX += npc.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;
            if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null

            if (Main.netMode != 1)
            {
                npc.ai[1]++;
                int aiTimerFire = (npc.whoAmI % 3 == 0 ? 50 : npc.whoAmI % 2 == 0 ? 150 : 100); //aiTimerFire is different per head by using whoAmI (which is usually different) 
                if (leftHead) aiTimerFire += 30;
                if (targetPlayer != null && npc.ai[1] == aiTimerFire)
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        Vector2 dir = Vector2.Normalize(targetPlayer.Center - npc.Center);
                        dir *= 5f;
                        Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX, PlayerPosY, mod.ProjectileType("OrthrusBreath"), (int)(damage * 1.3f), 0f, Main.myPlayer);
                    }
                }
                else
                if (npc.ai[1] >= 200) //pick random spot to move head to
                {
                    npc.ai[1] = 0;
                    npc.ai[2] = Main.rand.Next(-movementVariance, movementVariance);
                    npc.ai[3] = Main.rand.Next(-movementVariance, movementVariance);
                    npc.netUpdate = true;
                }
            }
            npc.rotation = 1.57f;
            Vector2 nextTarget = Body.npc.Center + new Vector2(leftHead ? -distFromBodyX : distFromBodyX, -distFromBodyY) + new Vector2(npc.ai[2], npc.ai[3]);
            if (Vector2.Distance(nextTarget, npc.Center) < 40f)
            {
                npc.velocity *= 0.9f;
                if (Math.Abs(npc.velocity.X) < 0.05f) npc.velocity.X = 0f;
                if (Math.Abs(npc.velocity.Y) < 0.05f) npc.velocity.Y = 0f;
            }
            else
            {
                npc.velocity = Vector2.Normalize(nextTarget - npc.Center);
                npc.velocity *= 5f;
            }
            npc.position += (Body.npc.oldPos[0] - Body.npc.position);
            npc.spriteDirection = -1;
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (Body != null)
            {
                Body.DrawHead(sb, leftHead ? "NPCs/Bosses/Orthrus/OrthrusHead1" : "NPCs/Bosses/Orthrus/OrthrusHead2", leftHead ? "NPCs/Bosses/Orthrus/OrthrusHead1_Glow" : "NPCs/Bosses/Orthrus/OrthrusHead2_Glow", npc, lightColor);
            }
            return true;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

    }
}
