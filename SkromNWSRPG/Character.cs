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
            Init();
        }

        public void Init()
        {
            Stuff[GearSlot.Head] = null;
            Stuff[GearSlot.Back] = null;
            Stuff[GearSlot.Chest] = null;
            Stuff[GearSlot.Legs] = null;
            Stuff[GearSlot.Feet] = null;
            Stuff[GearSlot.Weapon] = null;
            Stuff[GearSlot.TwoHand] = null;
            Stuff[GearSlot.OffHand] = null;
        }

        public void Equip(Gear equipment)
        {
             
            switch(equipment.Slot)
            {
                case GearSlot.TwoHand:
                    Stuff[GearSlot.Weapon] = equipment;
                    Stuff[GearSlot.OffHand] = null;
                    break;

                case GearSlot.Weapon:
                    if(this.state == true)
                    {
                        Stuff[GearSlot.OffHand] = equipment;
                        this.state = false;
                    }
                    else
                    {
                        Stuff[GearSlot.Weapon] = equipment;
                        this.state = true;
                    }
                    Stuff[GearSlot.TwoHand] = null;
                    break;

                case GearSlot.OffHand:
                    Stuff[GearSlot.OffHand] = equipment;
                    Stuff[GearSlot.Weapon] = null;
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

        public int GetTotalDamage()
        {
            HandledItem arme1, arme2;
            int tot = 0;
            arme1 = (HandledItem)GetItemInSlot(GearSlot.Weapon);
            arme2 = (HandledItem)GetItemInSlot(GearSlot.OffHand);
            tot += arme1.Damage + arme2.Damage;

            return tot;
        }

        public int GetTotalDefence()
        {
            HandledItem arme1, arme2;
            Armor armor1, armor2, armor3, armor4, armor5;
            int tot = 0;
            arme1 = (HandledItem)GetItemInSlot(GearSlot.Weapon);
            arme2 = (HandledItem)GetItemInSlot(GearSlot.OffHand);

           
            armor1 = (Armor)GetItemInSlot(GearSlot.Head);
            armor2 = (Armor)GetItemInSlot(GearSlot.Back);
            armor3 = (Armor)GetItemInSlot(GearSlot.Chest);
            armor4 = (Armor)GetItemInSlot(GearSlot.Legs);
            armor5 = (Armor)GetItemInSlot(GearSlot.Feet);

            if (Stuff[GearSlot.Head] != null)
            {
                tot += armor1.Defence;
            }
            if (Stuff[GearSlot.Back] != null)
            {
                tot += armor2.Defence;
            }
            if (Stuff[GearSlot.Chest] != null)
            {
                tot += armor3.Defence;
            }
            if (Stuff[GearSlot.Legs] != null)
            {
                tot += armor4.Defence;
            }
            if (Stuff[GearSlot.Feet] != null)
            {
                tot += armor5.Defence;
            }

            tot += arme1.Defence + arme2.Defence;

            return tot;
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
