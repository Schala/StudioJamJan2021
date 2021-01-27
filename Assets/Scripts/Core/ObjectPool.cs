/*
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street - Fifth Floor, Boston, MA 02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace StudioJamJan2021
{
    [Serializable]
    public class ObjectPoolItem
    {
        public int amount;
        public GameObject item = null;
        public bool expandable;
    }

    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] List<ObjectPoolItem> items = null;
        List<GameObject> pool = null;

        private void Awake() => pool = new List<GameObject>();

        private void Start()
        {
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < items[i].amount; j++)
                {
                    var obj = Instantiate(items[i].item);
                    obj.name = $"{obj.name} {j + 1}";
                    obj.SetActive(false);
                    pool.Add(obj);
                }
            }
        }

        public GameObject Get(string tag)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy && pool[i].CompareTag(tag))
                    return pool[i];
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].item.CompareTag(tag))
                {
                    if (items[i].expandable)
                    {
                        var obj = Instantiate(items[i].item);
                        obj.SetActive(false);
                        pool.Add(obj);
                        return obj;
                    }
                }
            }

            return null;
        }
    }
}
