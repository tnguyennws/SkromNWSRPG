using System;

namespace SkromNWSRPG
{
    /*
     * Cette classe représente une arme / objet à tenir dans la main dans notre RPG
     * Elle possède une valeur de Damage
     * Elle possède une valeur de Defence
     * Elle hérite de la classe Gear
     *
     * Son constructeur prend 4 arguments:
     * - Un Name
     * - Un GearSlot
     * - Une valeur de Damage
     * - Une valeur de Defence
     *
     * Un tel objet doit forcement être:
     * - Weapon
     * - TwoHand
     * - OffHand
     *
     * Elle lance une exception si ce n'est pas le cas
     */
    public class HandledItem : Gear
    {
        public int Damage;
        public int Defence;

        public HandledItem(string Name, GearSlot Slot, int Damage, int Defence) : base(Name, Slot)
        {
            this.Damage = Damage;
            this.Defence = Defence;

           if(Slot != GearSlot.Weapon && Slot != GearSlot.OffHand && Slot != GearSlot.TwoHand)
            {
                throw new Exception("Select Weapon, TwoHand or OffHand");
            }
        }

    }
}
