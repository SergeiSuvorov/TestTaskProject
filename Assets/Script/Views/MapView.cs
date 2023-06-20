using UnityEngine;
using UnityEngine.Tilemaps;

namespace View
{
    public class MapView : MonoBehaviour 
    {
        [SerializeField] private Tilemap _tilemap;
        public Vector3 MapSize => _tilemap.size;
        public Vector3 CellSize => _tilemap.cellSize;

    }
}