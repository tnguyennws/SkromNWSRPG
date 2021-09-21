using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkromNWSRPG
{
    /*
     * Cette classe représente un Objet dans notre RPG
     * Il porte un Name
     * C'est une classe Abstraite
     */
    public abstract class Item
    {
        public string Name;

        public Item(string name)
        {
            Name = name;
        }
    }
}
