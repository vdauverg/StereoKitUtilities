using System;
using StereoKit;

using StereoKitUtilities;

namespace StereoKitUtilities
{
    public static class Clone
    {
        /// <summary>
        /// Creates and returns a duplicate of a referenced Model
		/// </summary>
        /// <param name="model">The Model to duplicate</param>
        public static Model Duplicate(ref Model model)
        {
            Model _clone = new Model();

            for (int i = 0; i < model.SubsetCount; i++)
                _clone.AddSubset(model.GetMesh(i), model.GetMaterial(i), model.GetTransform(i));

            return _clone;
        }
    }
}
