using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkromNWSRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SkromRPGTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void IsAbstract()
        {
            Assert.IsTrue(typeof(Item).IsAbstract, "La classe Item n'est pas Abstraite");
        }

        [TestMethod]
        public void CheckFields()
        {
            Utils.CheckFields<Item>(new []
            {
                ("Name", "string")
            });
        }

        [TestMethod]
        public void CheckGear()
        {
            Utils.CheckFields<Gear>(new []
            {
                ("Slot", "GearSlot")
            });

            if (!typeof(Gear).IsAssignableTo(typeof(Item)))
                Assert.Fail("La Classe Gear n'hérite pas de Item");
            if (!typeof(Gear).IsAbstract)
                Assert.Fail("La Classe Gear n'est pas Abstraite");
        }

        [TestMethod]
        public void CheckArmor()
        {
            Utils.CheckFields<Armor>(new []
            {
                ("Defence", "int")
            });

            Type type = typeof(Armor);
            GearSlot[] slots = Enum.GetValues<GearSlot>();

            if (!type.IsAssignableTo(typeof(Gear)))
                Assert.Fail("La Classe Armor n'hérite pas de Gear");

            Utils.ValidateInstantiation<Armor>(new object[]
            {
                "Armure",
                slots.FirstOrDefault(i => i.ToString() == "Head"),
                42
            });

            Utils.RefuseInstantiation<Armor>("Il doit être impossible d'instancier une Armor dans le mauvais GearSlot", new object[]
                {
                    "Armure",
                    slots.FirstOrDefault(i => i.ToString() == "Weapon"),
                    42
                },
                new object[]
                {
                    "Armure",
                    slots.FirstOrDefault(i => i.ToString() == "OffHand"),
                    42
                }
            );

            object obj = Activator.CreateInstance(type, new object[]
            {
                "Armure",
                slots.FirstOrDefault(i => i.ToString() == "Head"),
                42
            });

            Utils.CheckValues(obj, new (string, object)[]
            {
                ("Name", "Armure"),
                ("Slot", slots.FirstOrDefault(i => i.ToString() == "Head")),
                ("Defence", 42)
            });
        }

        [TestMethod]
        public void CheckHandledItem()
        {
            if (!typeof(HandledItem).IsAssignableTo(typeof(Gear)))
                Assert.Fail("La Classe Weapon n'hérite pas de Gear");
            GearSlot[] slots = Enum.GetValues<GearSlot>();

            Utils.CheckFields<HandledItem>(new []
            {
                ("Damage", "int"),
                ("Defence", "int")
            });

            Utils.ValidateInstantiation<HandledItem>(new (string,  object)[]
            {
                ("Name", "Épee"),
                ("Slot", slots.FirstOrDefault(i => i.ToString() == "Weapon")),
                ("Damage", 42),
                ("Defence", 84)
            });

            string[] slotsNotAvailables = new string[]
            {
                "Head",
                "Back",
                "Chest",
                "Legs",
                "Feets"
            };

            foreach (string notAvailable in slotsNotAvailables)
            {
                Utils.RefuseInstantiation<HandledItem>("Il doit être impossible d'instancier un HandledItem dans le mauvais GearSlot", new object[]
                {
                    new object[]
                    {
                        "Masse",
                        slots.FirstOrDefault(i => i.ToString() == notAvailable),
                        42,
                        0
                    }
                });
            }
        }

        [TestMethod]
        public void CheckConsumable()
        {
            if (!typeof(Consumable).IsAssignableTo(typeof(Item)))
                Assert.Fail("La Classe Consumable n'hérite pas de Item");
            
            Consumable pot = Utils.ValidateInstantiation<Consumable>(new (string, object)[]
            {
                ("Name", "Potion"),
                ("Description", "Rends 20 HP"),
                ("Effect", new Action<Character>(TwentyHP))
            });

            Character c = Utils.ValidateInstantiation<Character>(new (string, object)[]
            {
                ("Name", "Skrom"),
                ("Life", 100)
            });

            Use(pot, c);
            Assert.AreEqual(120, GetLife(c));
        }

        private int GetLife(Character c)
        {
            return ((int)c.GetType().GetField("Life").GetValue(c));
        }

        private void Use(Consumable consumable, Character c)
        {
            Utils.Invoke<Character>(consumable, "Use", new object[]
            {
                c
            });
        }

        private static void TwentyHP(Character c)
        {
            int l = (int)c.GetType().GetField("Life").GetValue(c);

            c.GetType().GetField("Life").SetValue(c, l + 20);
        }
    }
}
