using System;
using NUnit.Framework;
using Sakura.Server;

namespace Script_Attribute_Spec
{
    [TestFixture]
    public class A_Script_Attribute
    {
        [Test]
        public void can_only_be_applied_to_classes()
        {
            var scriptAttributeAttributeUsage =
                (AttributeUsageAttribute?)Attribute.GetCustomAttribute(
                    typeof(ScriptAttribute),
                    typeof(AttributeUsageAttribute));
            Assert.That(
                scriptAttributeAttributeUsage!.ValidOn,
                Is.EqualTo(AttributeTargets.Class));
        }

        [Test]
        public void cannot_be_applied_multiple_times()
        {
            var scriptAttributeAttributeUsage =
            (AttributeUsageAttribute?)Attribute.GetCustomAttribute(
                typeof(ScriptAttribute),
                typeof(AttributeUsageAttribute));
            Assert.That(
                scriptAttributeAttributeUsage!.AllowMultiple,
                Is.False);
        }
    }
}
