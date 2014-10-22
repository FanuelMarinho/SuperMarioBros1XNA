using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Depois eu coloco em ingles pra ficar chique
/*
    Essa classe abstrata(poderia ser uma interface...WTV. 
 * Representa todos os objetos no cenario que iteragem entre si
 * Como inimigos e o player.  Tiles que geram moedas e itens ficam 
 * em outra class, ja que eles têm um comportamento e atributos diferentes dos
 * personagens do jogo
 
 */

namespace SuperMarioBros1XNA
{
    public abstract class GameObject
    {
        protected Vector2 position = Vector2.Zero;
        protected Vector2 velocity = Vector2.Zero;
        public bool onGround = false;
        public bool dead = false;
                
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
      
        #region Getters and Setters
        public float VelocityX
        {
            set { this.velocity.X = value; }
            get { return this.velocity.X; }
        }

        public float VelocityY
        {
            set { this.velocity.Y = value; }
            get { return this.velocity.Y; } 
        }

        public Vector2 Velocity 
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }

        public Vector2 CameraPosition 
        {
            get { return Camera.TransformToCameraPosition(this.position); }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public abstract void onHit(GameObject gameObject);
        public abstract int Width{ get; }
        public abstract int Height{ get; }

        public abstract Rectangle DrawHitBox{ get; }
        public abstract Rectangle HitBox{ get; }            
        
        #endregion


    }
}
