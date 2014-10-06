using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace SuperMarioBros1XNA
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        World world;
        Level level;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.graphics.PreferredBackBufferWidth  = 320;
            this.graphics.PreferredBackBufferHeight = 220;
            this.graphics.ApplyChanges();
            
            Camera.Initialize(Vector2.Zero, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight, 212 * 16, 14 * 16);
            Camera.setObjectToFollow(player);

       }

        protected override void LoadContent()
        {

            TextureResource textureResource = new TextureResource(Content);
            textureResource.LoadTextures();
            ServiceLocator.setTextureService(textureResource);
            
            SoundPlayer.Instance.Initialize(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D tileMapTextureTemp = Content.Load<Texture2D>(@"Textures/TileMap");

            player = new Player(new Vector2(30, 176), Content);
            //player = new Player(new Vector2(3050, 22), Content);


            IWorldLoader worldLoader = new XMLWorldLoader();
            world = worldLoader.LoadWorld("world11");
            
            level = new Level(tileMapTextureTemp, player, Content);
            level.loadLevel("world11");
                      

            SimplePhysics.Init(level);
            Physics.Init(level);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);

            player.Update(gameTime);
            level.Update(gameTime);
            Camera.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            //level.Draw(spriteBatch);
            world.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}