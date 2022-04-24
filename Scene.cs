using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML;
using SFML.System;
namespace SimplestGamePossible
{
    interface Scene:Drawable,IDisposable
    {
        public void Update();
        public void Enable();
    }
}
