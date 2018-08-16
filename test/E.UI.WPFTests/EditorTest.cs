using E.UI.Attributes;
using System;
using System.Windows;

namespace E.UI.WPFTests
{
    public enum Test
    {
        Test0,
        Test1,
        Test2,
        Test3
    }

    [UIClass]
    public class EditorTest
    {
        [UIRefresh(1000)]
        public int TestRefresh { get; set; }

        [UIIgnore]
        public string PROPERTY__SHOULD__BE__IGNORED { get; set; } = "You should not be seeing this :|";
        [UIIgnore]
        public string FIELD__SHOULD__BE__IGNORED = "You should not be seeing this :|";
        //[NoLabel]
        public int? TestIntNullable { get; set; }
        [UIDialog(typeof(EditorSettings))]
        public int TestInt { get; set; }
        [ReadOnly]
        public int ReadOnlyInt { get; set; }
        public string TestString { get; set; }
        public long? TestLongNullable { get; set; }
        public long TestLong { get; set; } = 5000000;
        public bool? TestBoolNullable { get; set; }
        public DateTime TestDateTime { get; set; }
        public DateTime? TestDateTimeNullable { get; set; }
        public bool TestBool { get; set; }
        public string FieldWithText = "hej";
        [NoLabel]
        public string FieldWithoutLabel = "noLabelField";

        public Rect? RectangleNullable { get; set; }
        public Rect Rectangle { get; set; } = new Rect(0, 0, 0, 0);

        public Test TestEnum { get; set; } = Test.Test2;
        public Test? TestEnumNullable { get; set; }
        [ReadOnly]
        public DayOfWeek DayOfWeek { get; set; } = DayOfWeek.Thursday;

        public decimal Decimal { get; set; } = 10.3m;
        public float Float { get; set; } = 10.5f;
        public float? FloatNullable { get; set; } = 10.5f;
        public double Double { get; set; } = 10.9d;

        public TestClass TestClass { get; set; } = new TestClass('m','e');
    }
}
