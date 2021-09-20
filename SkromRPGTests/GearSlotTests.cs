using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkromNWSRPG;

namespace SkromRPGTests
{
    [TestClass]
    public class GearSlotTests
    {
        [TestMethod]
        public void AvailableSlots()
        {
            string[] slots = new[]
            {
                "Head",
                "Back",
                "Chest",
                "Legs",
                "Feet",
                "Weapon",
                "OffHand",
                "TwoHand"
            };

            IEnumerable<string> values = Enum.GetValues<GearSlot>().Select(i => i.ToString());

            foreach (string slot in slots)
            {
                if (values.All(i => i != slot))
                    Assert.Fail($"{slot} n'existe pas dans GearSlot");
            }
        }
    }
}
