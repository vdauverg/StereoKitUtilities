using System;
using System.Collections.Generic;
using StereoKit;

namespace StereoKitUtilities
{
    class Spawner
    {
        public Model _model;
        public  List<Model> _spawnees = new List<Model>();
        public double _spawnTime;
        double  _timer;
        Matrix _transform;

        public Spawner(Matrix transform)
        {
            _transform = transform;
        }

        public Spawner(Matrix transform, Model model, double spawnTime)
        {
            _transform = transform;
            _model = model;
            _spawnTime = spawnTime;
        }

        public void SetTransform(Matrix transform)
        {
            _transform = transform;
        }

        public Matrix GetTransform()
        {
            return _transform;
        }
        
        public Model Spawn(Model model = null)
        {
            Model spawnee;

            if (model != null)
            {
                model.SetTransform(0, _transform);
                spawnee = Clone.Duplicate(ref model);
            }
            else
            {
                _model.SetTransform(0, _transform);
                spawnee = Clone.Duplicate(ref _model);
            }

            if (spawnee != null)
                _spawnees.Add(spawnee);

            return spawnee;
        }
        public void Despawn(Model model = null)
        {
            if (model != null)
                _spawnees.Remove(model);
            else
                _spawnees.RemoveAt(_spawnees.Count);
        }

        public void Update()
        {
            _timer += Time.Elapsed;
            if (_spawnTime > 0 && _timer >= _spawnTime)
            {
                _timer = 0;
                Spawn();
            }
            _spawnees.ForEach((spawn) => { spawn.Draw(Matrix.Identity); });
        }
    }
}
