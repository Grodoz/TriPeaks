﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TriPeaks.Test
{
    [TestClass]
    public class ValueConverterTests
    {

        private ColourToSuitConverter colourToSuit;
        private CardToNumberConverter cardToNumber;
        private SuitToColourConverter suitToColour;

        private Type stringType = typeof(string);
        private Type colourType = typeof(Color);

        [TestInitialize]
        public void Setup()
        {
            colourToSuit = new ColourToSuitConverter();
            cardToNumber = new CardToNumberConverter();
            suitToColour = new SuitToColourConverter();
        }

        [TestMethod]
        public void TestColourToSuitConverter()
        {
            foreach (var enumVal in EnumUtils.GetValues<CardColours>()) {
                string result = colourToSuit.Convert(enumVal, stringType, null, null) as string;
                Assert.IsTrue(result.Contains(enumVal.ToString().ToLower()), "The path didn't contain the name of the enum value.");
            }

        }
        
        [TestMethod]
        public void TestSuitToColourConverter()
        {
            var club = suitToColour.Convert(CardColours.Club, colourType, null, null);
            var diamond = suitToColour.Convert(CardColours.Diamond, colourType, null, null);
            var heart = suitToColour.Convert(CardColours.Heart, colourType, null, null);
            var spade = suitToColour.Convert(CardColours.Spade, colourType, null, null);

            Assert.AreEqual(Colors.Black, club);
            Assert.AreEqual(Colors.Red, diamond);
            Assert.AreEqual(Colors.Red, heart);
            Assert.AreEqual(Colors.Black, spade);
        }

        [TestMethod]
        public void TestCardToNumberConverter()
        {
            var expected = new[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            int i = 0;
            var enumValues = EnumUtils.GetValues<CardValues>().ToArray();
            foreach (var enumval in enumValues) {
                string result = cardToNumber.Convert(enumval, stringType, null, null) as string;
                Assert.AreEqual(expected[i++], result);
            }
        }

        [TestMethod]
        public void TestConvertersWithInvalidValues()
        {
            var ctsNull = colourToSuit.Convert(null, stringType, null, null);
            var ctsOOR = colourToSuit.Convert((CardColours)100, stringType, null, null); // OOR = Out of Range
            Assert.AreEqual(string.Empty, ctsNull);
            Assert.AreEqual(string.Empty, ctsOOR);

            var stcNull = suitToColour.Convert(null, colourType, null, null);
            var stcOOR = suitToColour.Convert((CardColours)100, colourType, null, null);
            Assert.AreEqual(Colors.Black, stcNull);
            Assert.AreEqual(Colors.Black, stcOOR);

            var ctnNull = cardToNumber.Convert(null, stringType, null, null);
            var ctnOOR = cardToNumber.Convert((CardValues)100, stringType, null, null);
            Assert.AreEqual(string.Empty, ctnNull);
            Assert.AreEqual(string.Empty, ctnOOR);

        }
    }

    internal static class EnumUtils
    {

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

    }
}
