using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkromNWSRPG
{
    /*
     * Cette classe représente un équippement dans notre RPG
     * Il possède un emplacement d'équipement
     * C'est une classe Abstraite
     * Elle hérite de la classe Item
     */
    public abstract class Gear : Item
    {
        public Gear(string Name) : base(Name)
        {

        }

        public GearSlot Slot = new GearSlot();

        
    }
}
