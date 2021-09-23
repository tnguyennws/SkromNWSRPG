using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkromNWSRPG
{
    /*
     * Cette classe représente un Personnage dans le Jeu
     *
     * Elle possède un Name
     * Elle possède une valeur de Life
     *
     * Cette Classe possède une Méthode Equip
     * Elle prend en paramètre un équipement et l'applique au slot correspondant du Character
     *
     * Le Character peut porter une arme dans les deux mains, à condition que ce soit un Weapon
     * Un objet à deux mains bloque l'emplacement OffHand et Weapon
     *
     * équiper un objet à une main 2 fois de suite dans Weapon l'équipe dans Weapon & OffHand
     *
     *
     * Cette Classe possède une Methode GetItemInSlot
     * Elle prend en paramètre un GearSlot
     * Elle renvoie l'objet équipé à l'emplacement passé en paramètre
     * Elle renvoie null si il n'y a rien à cet emplacement
     */
    public class Character
    {
        public string Name;
        public int Life;
        public Dictionary<GearSlot, Gear> Stuff = new();
        public bool state = false;


        public Character(string Name, int Life)
        {
            this.Name = Name;
            this.Life = Life;
        }

        public void Equip(Gear equipment)
        {
             
            switch(equipment.Slot)
            {
                case GearSlot.TwoHand:
                    Stuff[GearSlot.TwoHand] = equipment;
                    Stuff[GearSlot.Weapon] = null;
                    Stuff[GearSlot.OffHand] = null;
                    break;

                case GearSlot.Weapon:
                    if(state == true)
                    {
                        Stuff[GearSlot.OffHand] = equipment;
                        state = false;
                    }
                    else
                    {
                        Stuff[GearSlot.Weapon] = equipment;
                        state = true;
                    }
                    Stuff[GearSlot.TwoHand] = null;
                    break;

                case GearSlot.OffHand:
                    Stuff[GearSlot.OffHand] = equipment;
                    Stuff[GearSlot.TwoHand] = null;
                    break;

                case GearSlot.Head:
                    Stuff[GearSlot.Head] = equipment;
                    break;

                case GearSlot.Back:
                    Stuff[GearSlot.Back] = equipment;
                    break;

                case GearSlot.Chest:
                    Stuff[GearSlot.Chest] = equipment;
                    break;

                case GearSlot.Legs:
                    Stuff[GearSlot.Legs] = equipment;
                    break;

                case GearSlot.Feet:
                    Stuff[GearSlot.Feet] = equipment;
                    break;

                default:
                    Console.WriteLine("Erreur, ce n'est pas une arme ou armure");
                    break;
            }
        }

        public Gear GetItemInSlot(GearSlot slot)
        {
            if(Stuff[slot] != null)
            {
                return Stuff[slot];
            }
            else
            {
                return null;
            }
        }
    }
}
