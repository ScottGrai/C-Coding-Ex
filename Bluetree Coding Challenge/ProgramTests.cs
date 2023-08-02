using NUnit.Framework;

namespace Bluetree_Coding_Challenge.Tests
{ 
    [TestFixture]
    public class ProgramTests
    {
        private StringWriter? consoleOutput;
        readonly ItemScanner ItemScanner = new();

        [SetUp]
        public void Setup()
        {
            consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
        }

        [TearDown]
        public void Cleanup()
        {
            consoleOutput?.Dispose();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        }

        private int[] GetScannedItemsArray(int aCount, int bCount, int cCount, int dCount, int otherCount)
        {
            int[] scannedItems = new int[5];
            scannedItems[0] = aCount;
            scannedItems[1] = bCount;
            scannedItems[2] = cCount;
            scannedItems[3] = dCount;
            scannedItems[4] = otherCount;
            return scannedItems;
        }

        [Test]
        public void TestCalculateTotals()
        {
            int[] scannedItems1 = GetScannedItemsArray(2, 3, 1, 4, 0);
            int expectedTotal1 = 255;
            int actualTotal1 = ItemScanner.CalculateTotal(scannedItems1);
            Assert.AreEqual(expectedTotal1, actualTotal1);

            int[] scannedItems2 = GetScannedItemsArray(1, 2, 0, 1, 0);
            int expectedTotal2 = 110;
            int actualTotal2 = ItemScanner.CalculateTotal(scannedItems2);
            Assert.AreEqual(expectedTotal2, actualTotal2);

            int[] scannedItems3 = GetScannedItemsArray(0, 0, 0, 0, 0);
            int expectedTotal3 = 0;
            int actualTotal3 = ItemScanner.CalculateTotal(scannedItems3);
            Assert.AreEqual(expectedTotal3, actualTotal3);

            int[] scannedItems4 = GetScannedItemsArray(6, 1, 2, 3, 0);
            int expcetedTotal4 = 375;
            int actualTotal4 = ItemScanner.CalculateTotal(scannedItems4);
            Assert.AreEqual(expcetedTotal4, actualTotal4);

            int[] scannedItems5 = GetScannedItemsArray(2, 4, 1, 1, 2);
            int expectedTotal5 = 225;
            int actualTotal5 = ItemScanner.CalculateTotal(scannedItems5);
            Assert.AreEqual(expectedTotal5, actualTotal5);
        }

        [Test]
        public void TestItemScannerWithValidInputs()
        {
            var inputReader = new StringReader("A\nB\nC\nD\nstop\n");
            var outputWriter = new StringWriter();

            Console.SetIn(inputReader);
            Console.SetOut(outputWriter);

            ItemScanner.Run();

            string actualOutput = outputWriter.ToString();
            StringAssert.Contains("Item scanner running. Type \"stop\" to stop the program", actualOutput);
            StringAssert.Contains("Scan an item: ", actualOutput);
            StringAssert.Contains("Current total is: ", actualOutput);
            StringAssert.Contains("Final total is: ", actualOutput);

            inputReader.Dispose();
            outputWriter.Dispose();
        }

        [Test]
        public void TestItemScannerWithInvalidInputs()
        {
            var inputReader = new StringReader("X\nstop\n");
            var outputWriter = new StringWriter();

            Console.SetIn(inputReader);
            Console.SetOut(outputWriter);

            ItemScanner.Run();

            string actualOutput = outputWriter.ToString();
            StringAssert.Contains("Invalid SKU", actualOutput);
        }
    }
}
