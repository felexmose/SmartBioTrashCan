using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestSmartTrashService.Model
{
    public class recipe1
    {
        public int id { get; set; }
        public string dato { get; set; }
        public string recipe { get; set; }
    
        
        public recipe1(int id, string dato, string recipe)
        {
            this.id = id;
            this.dato = dato;
            this.recipe = recipe;
           
        }

        public recipe1() { }
    }
}
