using System;
using System.Data.Linq;
using System.Collections.Generic;
using System.Text;

namespace FacebookMartialArtsStaff
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Enter the Test Case: ");
			new Staff().Calculate();
		}
	}

	class Staff {

		private int GetSumOfMass (string input)
		{
			int result = 0;
			char[] massEachSection;
			
			if (string.IsNullOrEmpty (input)) {
				return 0;
			}
			
			massEachSection = input.ToCharArray ();
			
			for (int i = 0; i < massEachSection.Length; i++) {
				char currentChar = massEachSection[i];
				if(Char.IsNumber(currentChar)) {
					result += (int)Char.GetNumericValue(currentChar);
				}
			}
			
			return result;
		}
		
		private int WeightedSumOfMasses (string mass)
		{
			int result = 0;
			if (String.IsNullOrEmpty (mass)) {
				return result;
			}

			char[] masses = mass.ToCharArray();
			
			for(int i = 0; i < masses.Length; i++) {
				int cMass = (int)Char.GetNumericValue(masses[i]) * (i+1);
				result += cMass;
			}
			
			return result;
		}
		
		private float WeightedByRegular (float weighted, float regular)
		{
			return weighted / regular;
		}
		
		private string Reverse (string input)
		{
			char[] inputArray = input.ToCharArray();
			Array.Reverse(inputArray);
			return new string(inputArray);
		}

		private bool isBalanced (string input)
		{
			int sum = this.GetSumOfMass (input);
			int weighted = this.WeightedSumOfMasses (input);
			float center_w = WeightedByRegular(weighted, sum);
			float center = (float)((input.Length + 1.0)/ 2.0);

			if (center == center_w) {
				return true;
			}
			return false;
		}

		private string Substring (int from, int to, string input)
		{
			StringBuilder builder = new StringBuilder ();
			for (int i = from; i < to; i++) {
				builder.Append(input[i]);
			}

			return builder.ToString();
		}

		public void Calculate ()
		{
			string stave = Console.ReadLine ();
			string[] cancelSpaces = stave.Split (' ');
			if (cancelSpaces.Length == 1) {
				stave = cancelSpaces [0];
			} else {
				stave = cancelSpaces.ToString ();
			}

			int max_lenght = stave.Length / 2;
			int i = max_lenght;
			for (i= max_lenght; i >= 1; --i) {
				int remain = stave.Length - i*2;


				for(int j = 0; j < remain; j++) {
						string left = this.Substring(j, i+j-1, stave);
					int rightRem = remain - j;

					for(int x = rightRem; x >= 0; x++) {
							int start = stave.Length-x-i;
							int finish = stave.Length-x-1;
							string right = this.Substring(start, finish, stave);

						string temp = left + right;
						string rightReversed = left + this.Reverse(right);
						string leftReversed = this.Reverse(left) + right;
					
						bool isBalanced = this.isBalanced(temp);
						bool isRightReversedBalanced = this.isBalanced(rightReversed);
						bool isLeftReversedBalanced = this.isBalanced(leftReversed);

						if(isBalanced || isRightReversedBalanced || isLeftReversedBalanced) {
							int firstIndex = j;
							int secondIndex = stave.Length - x;
							int thirdIndex = i;

							Console.WriteLine("{0} {1} {2}", firstIndex, secondIndex, thirdIndex);
							return;
						}
					}

				}
				
			}
		}
	}
}
