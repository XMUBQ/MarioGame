using FooBarHappyHour.Blocks;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Utility;
using FooBarHappyHour.Score;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Collision
{
    public static class PlayerBlockCollisionHandler
    {
        public static void HandleCollision(IPlayer player, IBlock block, CollisionSide side)
        {
            if (side == CollisionSide.Top && player.IsJumping)
            {
                player.Idle();
            }

            if (block is HiddenBlock)   // Special case handling for hidden blocks (only bottom collision allowed)
            {
                HandleHiddenBlockCollision(player, block as HiddenBlock, side);
            }
            else
            {
                if (!block.Collidable) return;

                switch (block)
                {
                    case BrickBlock brickBlock:
                        HandleBrickBlockCollision(player, brickBlock, side);
                        break;
                    case QuestionBlock questionBlock:
                        HandleQuestionBlockCollision(player, questionBlock, side);
                        break;
                    default:
                        PlayerBlockRepel(block, player, side);
                        break;
                }
            }
        }

        public static void HandleBrickBlockCollision(IPlayer player, BrickBlock brickBlock, CollisionSide side)
        {
            if (brickBlock.Breakable)
            {
                 HandleBreakableBrickBlockCollision(player, brickBlock, side);
            }
            else
            {
                HandleUnbreakableBrickBlockCollision(player, brickBlock, side);
            }
        }

        public static void HandleUnbreakableBrickBlockCollision(IPlayer player, BrickBlock brickBlock, CollisionSide side)
        {
            PlayerBlockRepel(brickBlock, player, side);

            if ((side == CollisionSide.Bottom) && !brickBlock.Depleted)
            {
                brickBlock.BeBumped();
                brickBlock.SpawnItem();
                if (brickBlock.Depleted) brickBlock.BecomeUsed();   // Become used after all contained items have been spawned
            }
        }

        public static void HandleBreakableBrickBlockCollision(IPlayer player, BrickBlock brickBlock, CollisionSide side)
        {
            PlayerBlockRepel(brickBlock, player, side);

            if ((side == CollisionSide.Bottom) && !brickBlock.Broken)
            {
                brickBlock.BeBumped();
                if (player.IsBig) brickBlock.Break();
            }
        }

        public static void HandleQuestionBlockCollision(IPlayer player, QuestionBlock questionBlock, CollisionSide side)
        {
            PlayerBlockRepel(questionBlock, player, side);

            if (!questionBlock.IsUsed && side == CollisionSide.Bottom) // If block is unused and mario bumps the bottom of the block
            {
                questionBlock.BeBumped();
                questionBlock.SpawnItem();
                questionBlock.BecomeUsed();
            }
        }

        public static void HandleHiddenBlockCollision(IPlayer player, HiddenBlock hiddenBlock, CollisionSide side)
        {
            if (hiddenBlock.IsUsed) // If block has been exposed act like a UsedBlock
            {
                PlayerBlockRepel(hiddenBlock, player, side);
            }
            else if (side == CollisionSide.Bottom && player.PlayerPhysics.IsMovingUp())   // If block is still hidden and mario bumps the bottom of the block
            {
                PlayerBlockRepel(hiddenBlock, player, side);
                hiddenBlock.BeBumped();
                hiddenBlock.SpawnItem();
                hiddenBlock.BecomeUsed();
            }
        }

        public static void PlayerBlockRepel(IGameObject block, IPlayer player, CollisionSide side)
        {
            Physics.GeneralPhysics.RepelObject(block, player, side);

            if (side == CollisionSide.Top)
            {
                player.ResetStompCombo();
                player.MovementState.ResetJump();
            } 
        }
    }
}
