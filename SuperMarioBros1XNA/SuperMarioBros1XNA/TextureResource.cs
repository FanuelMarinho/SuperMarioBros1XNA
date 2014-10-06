using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SuperMarioBros1XNA
{
    public class TextureResource
    {
        private ContentManager content;
        private Dictionary<String, Texture2D> textures;

        public TextureResource(ContentManager content)
        {
            this.content = content;
        }

        public void loadTexture(string textureName, string alias)
        {
            Texture2D tempTexture = content.Load<Texture2D>(@"Textures/" + textureName);
            textures.Add(alias, tempTexture);
        }

        public Texture2D getTexture(string alias)
        {
            return textures[alias];
        }

        public void LoadTextures()
        {
            textures = new Dictionary<String, Texture2D>();
            textures.Add("goomba_run", content.Load<Texture2D>(@"Textures/goomba_run"));
            textures.Add("goomba_die", content.Load<Texture2D>(@"Textures/goomba_die"));
            textures.Add("koopa_run", content.Load<Texture2D>(@"Textures/koopa_run"));
            textures.Add("koopa_shell_in", content.Load<Texture2D>(@"Textures/koopa_shell_in"));
            textures.Add("koopa_shell_out", content.Load<Texture2D>(@"Textures/koopa_shell_out"));
            textures.Add("mario_idle", content.Load<Texture2D>(@"Textures/mario_idle"));
            textures.Add("mario_die",  content.Load<Texture2D>(@"Textures/mario_die"));
            textures.Add("mario_jump", content.Load<Texture2D>(@"Textures/mario_jump"));
            textures.Add("mario_run2", content.Load<Texture2D>(@"Textures/mario_run2"));
            textures.Add("TileMap", content.Load<Texture2D>(@"Textures/TileMap"));
        }
    }
}
