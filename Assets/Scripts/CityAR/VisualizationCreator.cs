using DefaultNamespace;
using UnityEngine;

namespace CityAR
{
    public class VisualizationCreator : MonoBehaviour
    {
        private DataObject _dataObject;
        private GameObject _platform;
        private Data _data;
        public Material material;

        private void Start()
        {
            _platform = GameObject.Find("Platform");
            _data = _platform.GetComponent<Data>();
            _dataObject = _data.ParseData();
            BuildCity(_dataObject);
        }

        private void BuildCity(DataObject p)
        {
            if (p.project.files.Count > 0)
            {
                p.project.w = 1;
                p.project.h = 1;
                p.project.deepth = 1;
                BuildDistrict(p.project, false);
            }
        }

        /*
         * entry: Single entry from the data set. This can be either a folder or a single file.
         * splitHorizontal: Specifies whether the subsequent children should be split horizontally or vertically along the parent
         */
        private void BuildDistrict(Entry entry, bool splitHorizontal)
        {
            if (entry.type.Equals("File"))
            {
                //TODO if entry is from type File, create building
            }
            else
            {
                float x = entry.x;
                float z = entry.z;

                float dirLocs = entry.numberOfLines;
                entry.color = GetColorForDepth(entry.deepth);

                BuildDistrictBlock(entry, false);

                foreach (Entry subEntry in entry.files) {
                    subEntry.x = x;
                    subEntry.z = z;
                    
                    if (subEntry.type.Equals("Dir"))
                    {
                        float ratio = subEntry.numberOfLines / dirLocs;
                        subEntry.deepth = entry.deepth + 1;

                        if (splitHorizontal) {
                            subEntry.w = ratio * entry.w; // split along horizontal axis
                            subEntry.h = entry.h;
                            x += subEntry.w;
                        } else {
                            subEntry.w = entry.w;
                            subEntry.h = ratio * entry.h; // split along vertical axis
                            z += subEntry.h;
                        }
                    }
                    else
                    {
                        subEntry.parentEntry = entry;
                    }
                    BuildDistrict(subEntry, !splitHorizontal);
                }

                if (!splitHorizontal)
                {
                    entry.x = x;
                    entry.z = z;
                    if (ContainsDirs(entry))
                    {
                        entry.h = 1f - z;
                    }
                    entry.deepth += 1;
                    BuildDistrictBlock(entry, true);
                }
                else
                {
                    entry.x = -x;
                    entry.z = z;
                    if (ContainsDirs(entry))
                    {
                        entry.w = 1f - x;
                    }
                    entry.deepth += 1;
                    BuildDistrictBlock(entry, true);
                }
            }
        }

        /*
         * entry: Single entry from the data set. This can be either a folder or a single file.
         * isBase: If true, the entry has no further subfolders. Buildings must be placed on top of the entry
         */
        private void BuildDistrictBlock(Entry entry, bool isBase)
        {
            if (entry == null)
            {
                return;
            }
            
            var w = entry.w - 0.005f; // w -> x coordinate
            var h = entry.h - 0.005f; // h -> z coordinate
            var x = entry.x;
            var z = entry.z;
            var depth = entry.deepth;
            
            if (w * h > 0 && !isBase)
            {
                var parent = new GameObject();
                parent.name = entry.name;
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<MeshRenderer>().material = material;
                cube.transform.parent = parent.transform;
                parent.transform.parent = gameObject.transform;
                cube.transform.localPosition = new Vector3(-0.5f, 0.5f, 0.5f);
                parent.transform.localScale = new Vector3(w, 0f, h);
                parent.transform.position = new Vector3(x, 0.03f * depth, z);
            }
        }

        private bool ContainsDirs(Entry entry)
        {
            foreach (Entry e in entry.files)
            {
                if (e.type.Equals("Dir"))
                {
                    return true;
                }
            }

            return false;
        }
        
        private Color GetColorForDepth(int depth)
        {
            //TODO generate a color for a given depth
            return Color.green;
        }
    }
}