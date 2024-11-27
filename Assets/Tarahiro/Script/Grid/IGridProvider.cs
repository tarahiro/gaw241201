using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tarahiro.TGrid
{
    public interface IGridProvider
    {
        bool IsPositionable(Vector2Int position, int positionableIndex);

        /// <summary>
        /// �^�C���}�b�v���擾���܂��BOrder in Layer���Ⴂ���ɕ���ł��܂��B
        /// </summary>
        /// <param name="tileMapId"></param>
        /// <returns></returns>
        Tilemap GetTilemap(int tileMapId);

    }
}
