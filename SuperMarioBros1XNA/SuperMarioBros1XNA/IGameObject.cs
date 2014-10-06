using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{
    public interface IGameObject
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

        void setVelocity(Vector2 newVel);
        void setPosition(Vector2 newPosition);

        Vector2 getVelocity();
        Vector2 getCameraPosition();
        Vector2 getPosition();
                
        int getWidth();
        int getHeight();
        bool isOnGround();
        void setOnGround(bool grounded);
        Rectangle getRectangleBounds();
    }
     
}
