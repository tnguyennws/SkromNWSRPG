using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkromNWSRPG
{
    /*
     * Cette classe représente un objet que l'on peut utiliser dans notre RPG
     * Elle possède une Description
     * Elle possède un Effect
     *      Cet Effect est de type Action<Character>
     * Elle hérite de la classe Item
     *
     * Son constructeur prend 3 arguments:
     * - Un Name
     * - Une Description
     * - Un Effect
     *
     * Cette classe possède la Méthode Use
     * Elle prend un Character et applique l'Effect sur lui
     */
    public class Consumable : Item
    {
        public string Description;
        public Action<Character> Effect;

        public Consumable(string Name, string Description, Action<Character> Effect) : base(Name)
        {
            this.Description = Description;
            this.Effect = Effect;
        }

        public void Use(object Character)
        {
            
        }

    }
}
