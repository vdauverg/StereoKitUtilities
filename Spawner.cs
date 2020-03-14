using System;
using System.Collections.Generic;
using StereoKit;

namespace StereoKitUtilities
{
    class Spawner
    {
        public Model _model, _recentSpawn;
        public  List<Model> _spawnees = new List<Model>();
        public double _spawnTime;
        double  _timer;
        public bool _spawning = false;

        public Spawner()
        {
        }

        public Spawner(Model model, double spawnTime)
        {
            _model = model;
            _spawnTime = spawnTime;
        }

        public Model Spawn(Model model = null)
        {
            Model spawnee;

            if (model != null)
                spawnee = Clone.Duplicate(ref model);
            else
                spawnee = Clone.Duplicate(ref _model);
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
            _spawning = false;
            _timer += Time.Elapsed;
            if (_spawnTime > 0 && _timer >= _spawnTime)
            {
                _spawning = true;
                _timer = 0;
                _recentSpawn = Spawn();
            }
            _model.Draw(Matrix.Identity);
        }
    }
}
