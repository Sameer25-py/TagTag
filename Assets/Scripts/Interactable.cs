using UnityEngine;
using UnityEngine.Tilemaps;

namespace TagTag
{
    public abstract class Interactable
    {
        private Vector3Int _index;
        public abstract void ApplyInteraction(Brain brain);
        public abstract void RemoveInteractable(Brain brain);

        public virtual Vector3Int SpawnInteractable(Tilemap tilemap, InteractableData data)
        {
            Vector3Int randomIndex = Grid.GetRandomValidIndex();
            if (tilemap.HasTile(randomIndex))
            {
                    SpawnInteractable(tilemap, data);
            }
            else
            {
                tilemap.SetTile(randomIndex, data.Sprite);
                Matrix4x4 scaleMatrix = Matrix4x4.Scale(data.scale);
                tilemap.SetTransformMatrix(randomIndex, scaleMatrix);
                _index = randomIndex;
            }

            return _index;
        }

        public virtual void DestroyInteractable(Tilemap tilemap)
        {
            if (tilemap.HasTile(_index))
            {
                tilemap.SetTile(_index, null);
            }
        }
    }
}