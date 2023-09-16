using System;
using NUnit.Framework;
using Sakura.Server;

namespace Tick_Attribute_Spec
{
    [TestFixture]
    public class A_tick_attribute
    {
        [Test]
        public void can_only_be_applied_to_methods()
        {
            var tickAttributeAttributeUsage =
                (AttributeUsageAttribute?)Attribute.GetCustomAttribute(
                    typeof(TickAttribute),
                    typeof(AttributeUsageAttribute));
            Assert.That(
                tickAttributeAttributeUsage!.ValidOn,
                Is.EqualTo(AttributeTargets.Method));
        }
    }
}
