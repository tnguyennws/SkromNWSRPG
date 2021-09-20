using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkromNWSRPG;

namespace SkromRPGTests
{
    [TestClass]
    public class CharacterTests
    {
        [TestMethod]
        public void CheckCharacter()
        {
            Character skrom = Utils.ValidateInstantiation<Character>(new (string, object)[]
            {
                ("Name", "Skrom"),
                ("Life", 100)
            });
        }

        [TestMethod]
        public void CheckHandledItems()
        {
            GearSlot TwoHand = Enum.GetValues<GearSlot>().FirstOrDefault(i => i.ToString() == "TwoHand");
            GearSlot Weapon = Enum.GetValues<GearSlot>().FirstOrDefault(i => i.ToString() == "Weapon");
            GearSlot OffHand = Enum.GetValues<GearSlot>().FirstOrDefault(i => i.ToString() == "OffHand");
            GearSlot Chest = Enum.GetValues<GearSlot>().FirstOrDefault(i => i.ToString() == "ChestHand");

            Character skrom = Utils.ValidateInstantiation<Character>(new (string, object)[]
            {
                ("Name", "Skrom"),
                ("Life", 100)
            });

            HandledItem aluneth = Utils.ValidateInstantiation<HandledItem>(new (string, object)[]
            {
                ("Name", "Aluneth"),
                ("Slot", TwoHand),
                ("Damage", 42),
                ("Defence", 10)
            });
            HandledItem aWarglave = Utils.ValidateInstantiation<HandledItem>(new (string, object)[]
            {
                ("Name", "Azzinoth Warglaive"),
                ("Slot", Weapon),
                ("Damage", 42),
                ("Defence", 10)
            });
            HandledItem nScale = Utils.ValidateInstantiation<HandledItem>(new (string, object)[]
            {
                ("Name", "Neltharion's Scale"),
                ("Slot", OffHand),
                ("Damage", 10),
                ("Defence", 42)
            });
            Armor tArmor = Utils.ValidateInstantiation<Armor>(new (string, object)[]
            {
                ("Name", "Thrall's Armor"),
                ("Slot", Chest),
                ("Defence", 42)
            });

            Equip(skrom, aWarglave);
            CheckGear(skrom, Weapon, aWarglave);
            CheckGear(skrom, OffHand, (HandledItem)null);

            Equip(skrom, aWarglave);
            CheckGear(skrom, Weapon, aWarglave);
            CheckGear(skrom, OffHand, aWarglave);

            Equip(skrom, aluneth);
            CheckGear(skrom, Weapon, aluneth);
            CheckGear(skrom, OffHand, (HandledItem)null);

            Equip(skrom, nScale);
            CheckGear(skrom, Weapon, (HandledItem)null);
            CheckGear(skrom, OffHand, nScale);

            Equip(skrom, aWarglave);
            CheckGear(skrom, Weapon, aWarglave);
            CheckGear(skrom, OffHand, nScale);

            Equip(skrom, tArmor);
            CheckGear(skrom, Chest, tArmor);

            Assert.AreEqual(52, GetDamage(skrom));
            Assert.AreEqual(94, GetDefence(skrom));
        }

        private int GetDamage(Character c)
        {
            return (Utils.Invoke<Character, int>(c, "GetTotalDamage", null));
        }

        private int GetDefence(Character c)
        {
            return (Utils.Invoke<Character, int>(c, "GetTotalDefence", null));
        }

        private void Equip(Character character, HandledItem item)
        {
            Utils.Invoke<Character>(character, "Equip", new object[]
            {
                item
            });
        }

        private void Equip(Character character, Armor item)
        {
            Utils.Invoke<Character>(character, "Equip", new object[]
            {
                item
            });
        }

        private void CheckGear(Character character, GearSlot slot, HandledItem g)
        {
            Utils.CheckTrue(() => Utils.Invoke<Character, HandledItem>(character, "GetItemInSlot", new object[]
            {
                slot
            }) == g, "La méthode ne renvoie pas le bon objet");
        }

        private void CheckGear(Character character, GearSlot slot, Armor g)
        {
            Utils.CheckTrue(() => Utils.Invoke<Character, Armor>(character, "GetItemInSlot", new object[]
            {
                slot
            }) == g, "La méthode ne renvoie pas le bon objet");
        }
    }
}
