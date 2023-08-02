namespace Bluetree_Coding_Challenge
{
    class ItemScanner
    {
        static void Main()
        {
            var itemScanner = new ItemScanner();

            itemScanner.Run();
        }

        public void Run()
        {
            bool appOn = true;
            Console.WriteLine("Item scanner running. Type \"stop\" to stop the program");
            int[] scannedItems = new int[4];
            do
            {
                Console.WriteLine("Scan an item: ");
                string itemSku = Console.ReadLine();
                string lowerCaseSku = itemSku;
                // SKUs are usually case sensitive so the user will have to put in case sensitive skus, but the stop command does not need to be
                if (lowerCaseSku != null) { lowerCaseSku = lowerCaseSku.ToLower(); }

                if (itemSku == "A")
                {
                    scannedItems[0]++;
                }
                else if (itemSku == "B")
                {
                    scannedItems[1]++;
                }
                else if (itemSku == "C")
                {
                    scannedItems[2]++;
                }
                else if (itemSku == "D")
                {
                    scannedItems[3]++;
                }
                else if (lowerCaseSku == "stop")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid SKU");
                    continue;
                }

                int total = CalculateTotal(scannedItems);

                Console.WriteLine($"Current total is: {total}");
            } while (appOn);

            int finalTotal = CalculateTotal(scannedItems);
            Console.WriteLine($"Final total is: {finalTotal}");
        }

        internal static int CalculateTotal(int[] scannedItems)
        {
            int total = 0;

            total += CalculateTotalCost(scannedItems[0], "A");
            total += CalculateTotalCost(scannedItems[1], "B");
            total += CalculateTotalCost(scannedItems[2], "C");
            total += CalculateTotalCost(scannedItems[3], "D");

            return total;
        }

        static int CalculateTotalCost(int amount, string sku)
        {
            int divideBy = 1;
            int unitPrice;
            int salePrice = 0;

            if (sku == "A")
            {
                divideBy = 3;
                unitPrice = 50;
                salePrice = 130;
            }
            else if (sku == "B")
            {
                divideBy = 2;
                unitPrice = 30;
                salePrice = 45;
            }
            else if (sku == "C")
            {
                unitPrice = 20;
            }
            else if (sku == "D")
            {
                unitPrice = 15;
            }
            else
            {
                throw new Exception("Invalid SKU");
            }

            int result;

            if (salePrice != 0)
            {
                int divideAndRoundDown = (int)Math.Floor((double)amount / divideBy);
                int saleItemsValue = divideAndRoundDown *= salePrice;
                int remainder = amount % divideBy;
                if (remainder != 0)
                {
                    int nonSaleItems = remainder * unitPrice;
                    result = saleItemsValue + nonSaleItems;
                }
                else
                {
                    result = saleItemsValue;
                }
            }
            else
            {
                result = amount * unitPrice;
            }

            return result;
        }
    }
}
