string[] banks = File.ReadLines("input.txt").ToArray();
// string[] banks = {
//   "987654321111111",
//   "811111111111119",
//   "234234234234278",
//   "818181911112111"
// };

long totalSum = banks.Sum(x => BankAnalyzer.FindMaxJoltage12Batteries(x));
Console.WriteLine($"Total sum is: {totalSum}");
