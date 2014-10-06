using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarioBros1XNA
{
    public class ServiceLocator
    {
        private static TextureResource textureService;
        public static void setTextureService(TextureResource resource)
        {
            textureService = resource;
        }

        public static TextureResource TextureService()
        {
            return textureService;
        }

    }
}
