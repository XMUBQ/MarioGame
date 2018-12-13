using FooBarHappyHour.MetaStates;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Camera
{
    public static class MyCamera
    {
        public static Matrix GameWorldTransform()
        {
            Matrix transform;
            int worldWidth;
            if (SuperMarioBros.Instance.GameStateManager.InPrimaryWorld)
            {
                worldWidth = SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Width;
            }
            else
            {
                worldWidth = SuperMarioBros.Instance.GameStateManager.HiddenWorld.Width;
            }
            if (worldWidth * Utils.Instance.CommonObjectSize > SuperMarioBros.Instance.GraphicsDevice.Viewport.Width)
            {
                transform = DoesNotFitInWindow();
            }
            else
            {
                transform = FitsInWindow();
            }
            return transform;
        }

        private static Matrix DoesNotFitInWindow()
        {
            Matrix transform;
            if (SuperMarioBros.Instance.GameStateManager.Player.MovementState.Location.X <= SuperMarioBros.Instance.GraphicsDevice.Viewport.Width / 2)
            {
                transform = Matrix.CreateTranslation(new Vector3(-SuperMarioBros.Instance.GraphicsDevice.Viewport.Width * 0.5f, (int)(SuperMarioBros.Instance.GraphicsDevice.Viewport.Height * 0.5f - Utils.Instance.ViewHeight), 0)) * Matrix.CreateTranslation(new Vector3((int)(SuperMarioBros.Instance.GraphicsDevice.Viewport.Width * 0.5f), (int)(SuperMarioBros.Instance.GraphicsDevice.Viewport.Height * 0.5f), 0));
            }
            else if (SuperMarioBros.Instance.GameStateManager.Player.MovementState.Location.X >= SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Width * Utils.Instance.CommonObjectSize - SuperMarioBros.Instance.GraphicsDevice.Viewport.Width * 0.5f)
            {
                transform = Matrix.CreateTranslation(new Vector3((int)(-SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Width * Utils.Instance.CommonObjectSize + SuperMarioBros.Instance.GraphicsDevice.Viewport.Width * 0.5f), (int)SuperMarioBros.Instance.GraphicsDevice.Viewport.Height * 0.5f - Utils.Instance.ViewHeight, 0)) * Matrix.CreateTranslation(new Vector3((int)(SuperMarioBros.Instance.GraphicsDevice.Viewport.Width * 0.5f), (int)(SuperMarioBros.Instance.GraphicsDevice.Viewport.Height * 0.5f), 0));
            }
            else
            {
                transform = Matrix.CreateTranslation(new Vector3((int)-SuperMarioBros.Instance.GameStateManager.Player.MovementState.Location.X, SuperMarioBros.Instance.GraphicsDevice.Viewport.Height / 2 - Utils.Instance.ViewHeight, 0)) * Matrix.CreateTranslation(new Vector3(SuperMarioBros.Instance.GraphicsDevice.Viewport.Width / 2, SuperMarioBros.Instance.GraphicsDevice.Viewport.Height / 2, 0));
            }
            return transform;
        }

        private static Matrix FitsInWindow()
        {
            return Matrix.CreateTranslation(new Vector3(-SuperMarioBros.Instance.GraphicsDevice.Viewport.Width * 0.5f, (int)(SuperMarioBros.Instance.GraphicsDevice.Viewport.Height * 0.5f - Utils.Instance.ViewHeight), 0)) * Matrix.CreateTranslation(new Vector3((int)(SuperMarioBros.Instance.GraphicsDevice.Viewport.Width * 0.5f), (int)(SuperMarioBros.Instance.GraphicsDevice.Viewport.Height * 0.5f), 0));
        }
    }
}
