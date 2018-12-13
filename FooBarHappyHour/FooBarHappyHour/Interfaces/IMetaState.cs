using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.MetaStates
{
    public interface IMetaState
    {
        void Up(PlayerSelection playerSelection);
        void Down(PlayerSelection playerSelection);
        void Left(PlayerSelection playerSelection);
        void Right(PlayerSelection playerSelection);
        void Ability(PlayerSelection playerSelection);
        void Jump(PlayerSelection playerSelection);
        void SwitchLeft();
        void SwitchRight();
        void Quit();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
    }
}
