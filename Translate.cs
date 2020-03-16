using System;
using System.Collections.Generic;
using StereoKit;

namespace StereoKitUtilities
{
    public static class Move
    {
        public static void Translate(Model model, Vec3 dir, float speed)
        {
            dir *= speed;
            model.SetTransform(0, model.GetTransform(0) * Matrix.T(dir));
        }
    }
}
