using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestSmartTrashService.Model
{
    public class weight
    {
        public int id { get; set; }
        public string dato { get; set; }
        public string weightMeasure { get; set; }

        public weight(string dato, string weight)
        {
            this.dato = dato;
            this.weightMeasure = weight;
        }
        public weight() { }
    }
}
