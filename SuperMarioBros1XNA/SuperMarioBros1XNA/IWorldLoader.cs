using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarioBros1XNA
{
    interface IWorldLoader
    {
        World LoadWorld(String worldName);
    }
}
